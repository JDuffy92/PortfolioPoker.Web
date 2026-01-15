using Xunit;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.Services;
using PortfolioPoker.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using PortfolioPoker.TestUtilities.Fakes;

namespace PortfolioPoker.Domain.Tests.Services
{
    
    public class RoundActionValidatorTests
    {
        private RoundActionValidator _roundActionValidator;
        private Round _round;

        
        public RoundActionValidatorTests()
        {
            _roundActionValidator = new RoundActionValidator();

            var handEvaluator = new FakeHandEvaluator(HandType.Pair);
            var scoringService = new FakeScoringService(10);

            var deck = Deck.GenerateDeckForDeckType(DeckType.RedDeck);

            _round = new Round(
                handEvaluator: handEvaluator,
                scoringService: scoringService,
                pointTarget: 100,
                handsAvailable: 5,
                discardsAvailable: 5,
                jokers: new List<Joker>(),
                deck: deck
            );
        }

        [Fact]
        public void CanPerform_ReturnFailureFoDiscardInDrawPhase()
        {
            // Arrange
            var action = GameAction.Discard;
            var cards = new List<Card>();

            // Act
            var result = _roundActionValidator.CanPerform(_round, action, cards);

            // Assert
            Assert.False(result.IsValid);
        }


        [Fact]
        public void CanPerform_ReturnsSuccessForDiscardInSelectPhase()
        {
            // Arrange
            _round.Phase = RoundPhase.SelectPhase;
            var action = GameAction.Discard;
            var cards = new List<Card>();

            // Act
            var result = _roundActionValidator.CanPerform(_round, action, cards);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void CanPerform_ReturnFailureFoPlayHandInDrawPhase()
        {
            // Arrange
            var action = GameAction.PlayHand;
            var cards = new List<Card>();

            // Act
            var result = _roundActionValidator.CanPerform(_round, action, cards);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void CanPerform_ReturnsSuccessForPlayHandInSelectPhase()
        {
            // Arrange
            _round.Phase = RoundPhase.SelectPhase;
            var action = GameAction.PlayHand;
            var cards = new List<Card>();

            // Act
            var result = _roundActionValidator.CanPerform(_round, action, cards);

            // Assert
            Assert.True(result.IsValid);
        }

    }
}