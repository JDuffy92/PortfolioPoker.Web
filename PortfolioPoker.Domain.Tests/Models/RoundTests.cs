using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.Services;
using PortfolioPoker.TestUtilities.Fakes;

namespace PortfolioPoker.Tests.EditMode.Domain.Models
{
    
    public class RoundTests
    {
        private Round _round;

        private List<Joker> _jokers;
        private Deck _deck;

        
        public RoundTests()
        {
            _deck = Deck.GenerateDeckForDeckType(DeckType.RedDeck);

            //Set up our jokers
            _jokers = new List<Joker> { };

            var handEvaluator = new FakeHandEvaluator(HandType.HighCard);
            var scoringService = new FakeScoringService(pointsToReturn: 10);

            _round = new Round(
                handEvaluator: handEvaluator,
                scoringService: scoringService,
                pointTarget: 100,
                handsAvailable: 5,
                discardsAvailable: 5,
                jokers: new List<Joker>(),
                deck: _deck
            );

            //Draw initial hand of 3 cards
            _round.DrawCards(3);
        }

        [Fact]
        public void Round_HandShouldBeEmptyOnInitialization()
        {
            Assert.Equal(3, _round.Hand.Cards.Count);
        }

        [Fact]
        public void Round_DrawCards_ShouldAddCardsToHand()
        {
            _round.DrawCards(2);
            Assert.Equal(5, _round.Hand.Cards.Count);
        }

        [Fact]
        public void Round_DrawCardsShouldRemoveCardsFromDeck()
        {
            int initialDeckCount = _round.Deck.Cards.Count;
            _round.DrawCards(3);
            Assert.Equal(initialDeckCount - 3, _round.Deck.Cards.Count);
        }


        [Fact]
        public void Round_DiscardCards_ShouldMoveCardsFromHandToDiscardPile()
        {
            var cardsToDiscard = new List<Card>(_round.Hand.Cards);
            _round.DiscardCards(cardsToDiscard);
            Assert.Empty(_round.Hand.Cards);
            Assert.Equal(3, _round.DiscardPile.Cards.Count);
            Assert.Equal(1, _round.DiscardsMade);
        }

        [Fact]
        public void PlayCards_AddsPointsAndDiscardsCards()
        {
            var cardsToPlay = _round.Hand.Cards.ToList();

            // Act
            _round.PlayCards(cardsToPlay);

            // Assert
            Assert.Equal(10, _round.CurrentPoints);
            Assert.Empty(_round.Hand.Cards);
            Assert.Equal(3, _round.DiscardPile.Cards.Count);
            Assert.Equal(1, _round.HandsPlayed);
        }
    }
}