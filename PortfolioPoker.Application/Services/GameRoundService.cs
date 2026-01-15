using System;
using System.Collections.Generic;
using System.Linq;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.ValueObjects;
using PortfolioPoker.Application.Interfaces;
using PortfolioPoker.Domain.Events;

namespace PortfolioPoker.Application.Services
{
    public class GameRoundService : IGameRoundService
    {
        private readonly IHandEvaluator _handEvaluator;
        private readonly IRoundActionValidator _roundActionValidator;
        private readonly IRoundStateEvaluator _roundStateEvaluator;
        private readonly IRoundPhaseTransitionService _roundPhaseTransitionService;
        private readonly IRoundSnapshotService _roundSnapshotService;
        private readonly IScoringService _scoringService;

        public GameRoundService(
            IHandEvaluator handEvaluator,
            IRoundActionValidator roundActionValidator,
            IRoundStateEvaluator roundStateEvaluator,
            IRoundPhaseTransitionService roundPhaseTransitionService,
            IRoundSnapshotService roundSnapshotService,
            IScoringService scoringService
            )
        {
            _handEvaluator = handEvaluator;
            _roundActionValidator = roundActionValidator;
            _roundStateEvaluator = roundStateEvaluator;
            _roundPhaseTransitionService = roundPhaseTransitionService;
            _roundSnapshotService = roundSnapshotService;
            _scoringService = scoringService;
        }

        public RoundPhase ChangePhase(Round round, RoundPhase targetPhase, List<IGameEvent> events)
        {
            if (round.Phase != targetPhase)
            {
                var newPhase = UpdatePhase(round, targetPhase);
                events.Add(new RoundPhaseChangedEvent(newPhase));
                return newPhase;
            }
            return round.Phase;
        }

        public PerformActionResult<IReadOnlyList<Card>> DrawCards(Round round, int count)
        {
            //Log the round phase to the console for debugging
            Console.WriteLine($"Current round phase: {round.Phase}");

            if (!_roundPhaseTransitionService.CanTransition(round.Phase, RoundPhase.DrawPhase))
                return PerformActionResult<IReadOnlyList<Card>>.Fail($"Cannot draw cards in {round.Phase.ToString()} phase");

            _roundSnapshotService.Save(round);
            var events = new List<IGameEvent>();

            ChangePhase(round, RoundPhase.DrawPhase, events);

            var drawnCards = round.DrawCards(count);
            events.Add(new CardsDrawnEvent(drawnCards));

            ChangePhase(round, RoundPhase.SelectPhase, events);

            return PerformActionResult<IReadOnlyList<Card>>.Ok(drawnCards, events);
        }

        public PerformActionResult<HandEvaluationResult> PlayHand(Round round, IEnumerable<Card> cards)
        {
            //Log the round phase to the console for debugging
            Console.WriteLine($"Current round phase: {round.Phase}");

            var cardList = cards.ToList();
            if (!_roundPhaseTransitionService.CanTransition(round.Phase, RoundPhase.PlayPhase))
                return PerformActionResult<HandEvaluationResult>.Fail($"Cannot play hand in {round.Phase.ToString()} phase");

            var validation = _roundActionValidator.CanPerform(round, GameAction.PlayHand, cardList);
            if (!validation.IsValid)
                return PerformActionResult<HandEvaluationResult>.Fail(validation.ErrorMessage);

            _roundSnapshotService.Save(round);

            //Log the round phase to the console for debugging
            Console.WriteLine($"Current round phase 2: {round.Phase}");

            var events = new List<IGameEvent>();
            ChangePhase(round, RoundPhase.PlayPhase, events);

            //Log the round phase to the console for debugging
            Console.WriteLine($"Current round phase 3: {round.Phase}");

            var handEvaluation = round.PlayCards(cardList);
            events.Add(new CardsPlayedEvent(cardList));
            events.Add(new ScoreUpdatedEvent(round.CurrentPoints - handEvaluation.Cards.Count, round.CurrentPoints));

            var roundStatus = _roundStateEvaluator.Evaluate(round);

            //Check if round has ended
            if(roundStatus != RoundStatus.Active)
            {
                //If the round isn't active anymore, end it and set its status
                round.End(roundStatus);

                //Change phase to RoundEnd
                ChangePhase(round, RoundPhase.RoundEnd, events);
            } else
            {
                //The round hasn't ended so we go back to DrawPhase for the next hand
                ChangePhase(round, RoundPhase.DrawPhase, events);
            }

            return PerformActionResult<HandEvaluationResult>.Ok(handEvaluation, events);
        }

        public PerformActionResult<IReadOnlyList<Card>> DiscardCards(Round round, IEnumerable<Card> cards)
        {
            var cardList = cards.ToList();
            var validation = _roundActionValidator.CanPerform(round, GameAction.Discard, cardList);
            if (!validation.IsValid)
                return PerformActionResult<IReadOnlyList<Card>>.Fail(validation.ErrorMessage);

            _roundSnapshotService.Save(round);

            var events = new List<IGameEvent>();
            ChangePhase(round, RoundPhase.DiscardPhase, events);

            var discarded = round.DiscardCards(cardList);
            events.Add(new CardsDiscardedEvent(discarded));

            ChangePhase(round, RoundPhase.DrawPhase, events);

            return PerformActionResult<IReadOnlyList<Card>>.Ok(discarded, events);
        }

        public HandEvaluationResult PreviewHandEvaluation(IEnumerable<Card> cards){
           return _handEvaluator.Evaluate(cards.ToList());
        }

        public ScoringServiceResult PreviewScoreCards(IEnumerable<Joker> jokers, HandEvaluationResult handEvaluation)
        {
            return _scoringService.PreviewScoreCards(jokers, handEvaluation);
        }

        public RoundPhase UpdatePhase(Round round, RoundPhase newPhase)
        {
            if (!_roundPhaseTransitionService.CanTransition(round.Phase, newPhase))
                throw new InvalidOperationException($"Cannot transition from {round.Phase} to {newPhase}");

            round.Phase = newPhase;
            return round.Phase;
        }

        public void ResetPhase(Round round) => round.Phase = RoundPhase.DrawPhase;
    }

}
