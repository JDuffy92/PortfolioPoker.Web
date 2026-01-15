
using System;
using System.Collections.Generic;
using System.Linq;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.Domain.Models
{
    public class Round
    {
        private readonly IHandEvaluator _handEvaluator;
        private readonly IScoringService _scoringService;

        public RoundPhase Phase { get; set; } = RoundPhase.StartPhase;

        public RoundStatus Status { get; set; } = RoundStatus.Active;

        public int HandsAvailable { get; }
        public int HandsPlayed { get; set; } = 0;

        public int DiscardsAvailable { get; }
        public int DiscardsMade { get; set; } = 0;


        public int PointTarget { get; private set; }
        public int CurrentPoints { get; set; } = 0;

        public List<Joker> Jokers { get; private set; }
        public Deck Deck { get; set; }
        public Hand Hand { get; private set; } = new Hand(new List<Card>());
        public DiscardPile DiscardPile { get; private set; } = new DiscardPile();

        public Round(
            IHandEvaluator handEvaluator,
            IScoringService scoringService,
            int pointTarget,
            int handsAvailable,
            int discardsAvailable,
            List<Joker> jokers,
            Deck deck
        )
        {
            _handEvaluator = handEvaluator;
            _scoringService = scoringService;
            PointTarget = pointTarget;
            HandsAvailable = handsAvailable;
            DiscardsAvailable = discardsAvailable;
            Jokers = jokers;
            Deck = deck;
        }

        public HandEvaluationResult PlayCards(IEnumerable<Card> cardsToPlay)
        {
            if (!CanPlayHand())
            {
                throw new InvalidOperationException("No hands remaining.");
            }

            var cardList = cardsToPlay.ToList();

            var evaluation = _handEvaluator.Evaluate(cardList);
            var points = _scoringService.ScoreCards(Jokers, evaluation);

            CurrentPoints += points;

            AddCardsToDiscardPile(cardList); // Add to discard pile but don't count as a discard action

            HandsPlayed++;

            return evaluation;
        }


        public IReadOnlyList<Card> DrawCards(int numberOfCards)
        {
            var drawn = new List<Card>();

            for (int i = 0; i < numberOfCards; i++)
            {
                if (Deck.Cards.Count == 0)
                    throw new InvalidOperationException("Deck is empty.");

                var card = DrawCard();
                drawn.Add(card);
            }

            return drawn;
        }

        private Card DrawCard()
        {
            if (Deck.Cards.Count == 0) throw new InvalidOperationException("Deck is empty");
            var card = Deck.Cards[0];
            Deck.Cards.RemoveAt(0);
            Hand.AddCard(card);
            return card;
        }

        public IReadOnlyList<Card> AddCardsToDiscardPile(IEnumerable<Card> cards)
        {
            var cardList = cards.ToList();

            Hand.DiscardCards(cardList, DiscardPile);

            return cardList;
        }

        public IReadOnlyList<Card> DiscardCards(IEnumerable<Card> cards)
        {
            if (!CanDiscard())
            {
                throw new InvalidOperationException("No discards remaining.");
            }

            var cardList = cards.ToList();

            Hand.DiscardCards(cardList, DiscardPile);
            DiscardsMade++;

            return cardList;
        }

        public bool CanPlayHand()
        {
            return HandsPlayed < HandsAvailable;
        }

        public bool CanDiscard()
        {
            return DiscardsMade < DiscardsAvailable;
        }

        public void End(RoundStatus status)
        {
            if (Phase == RoundPhase.RoundEnd)
                throw new InvalidOperationException("Round already ended");

            Phase = RoundPhase.RoundEnd;
            Status = status;
        }
    }
}