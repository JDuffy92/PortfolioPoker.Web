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
    
    public class RoundStateEvaluatorTests
    {
        private RoundStateEvaluator _roundStateEvaluator;

        private Round _round;

        
        public RoundStateEvaluatorTests()
        {
            _roundStateEvaluator = new RoundStateEvaluator();

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
        public void Evaluate_ReturnsActive_WhenRoundIsOngoing()
        {
            // Arrange
            _round.CurrentPoints = 50;
            _round.HandsPlayed = 2;

            // Act
            var result = _roundStateEvaluator.Evaluate(_round);

            // Assert
            Assert.Equal(RoundStatus.Active, result);
        }

        [Fact]
        public void Evaluate_ReturnsSuccess_WhenPointTargetIsMet()
        {
            // Arrange
            _round.CurrentPoints = 100;
            _round.HandsPlayed = 2;

            // Act
            var result = _roundStateEvaluator.Evaluate(_round);

            // Assert
            Assert.Equal(RoundStatus.Success, result);
        }

        [Fact]
        public void Evaluate_ReturnsFailure_WhenNoHandsLeft()
        {
            // Arrange
            _round.CurrentPoints = 50;
            _round.HandsPlayed = 5;

            // Act
            var result = _roundStateEvaluator.Evaluate(_round);

            // Assert
            Assert.Equal(RoundStatus.Failure, result);
        }
    }
}