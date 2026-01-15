using System.Collections.Generic;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.Application.Interfaces
{
    public interface IGameRoundService
    {
        RoundPhase ChangePhase(Round round, RoundPhase targetPhase, List<IGameEvent> events);

        PerformActionResult<IReadOnlyList<Card>> DrawCards(Round round, int count);

        PerformActionResult<HandEvaluationResult> PlayHand(Round round, IEnumerable<Card> cards);

        PerformActionResult<IReadOnlyList<Card>> DiscardCards(Round round, IEnumerable<Card> cards);

        HandEvaluationResult PreviewHandEvaluation(IEnumerable<Card> cards);

        ScoringServiceResult PreviewScoreCards(IEnumerable<Joker> jokers, HandEvaluationResult handEvaluation);

        RoundPhase UpdatePhase(Round round, RoundPhase newPhase);

        void ResetPhase(Round round) => round.Phase = RoundPhase.DrawPhase;
    }
}