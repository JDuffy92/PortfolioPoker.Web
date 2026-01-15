using Xunit;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.Services;
using PortfolioPoker.Domain.ValueObjects;
using System.Collections.Generic;

namespace PortfolioPoker.Domain.Tests.Services
{
    
    public class ScoringServiceTests
    {
        private ScoringService _scoringService;

        
        public ScoringServiceTests()
        {
            _scoringService = new ScoringService();
        }

        [Theory]
        [InlineData(HandType.HighCard, 5, 1, 5)]
        [InlineData(HandType.Pair, 10, 2, 20)]
        [InlineData(HandType.TwoPair, 20, 2, 40)]
        [InlineData(HandType.ThreeOfAKind, 30, 3, 90)]
        [InlineData(HandType.Straight, 30, 4, 120)]
        [InlineData(HandType.Flush, 40, 4, 160)]
        [InlineData(HandType.FullHouse, 40, 4, 160)]
        [InlineData(HandType.FourOfAKind, 60, 7, 420)]
        [InlineData(HandType.StraightFlush, 100, 8, 800)]
        [InlineData(HandType.RoyalFlush, 100, 8, 800)]
        public void ScoreCards_ReturnsExpectedPoints(HandType handType, int expectedChips, int expectedMultiplier, int expectedTotal)
        {
            // Arrange
            var handEvaluation = new HandEvaluationResult(handType, new List<Card>());

            // Act
            var points = _scoringService.ScoreCards(new List<Joker>(), handEvaluation);

            // Assert
            Assert.Equal(expectedChips, _scoringService.Chips);
            Assert.Equal(expectedMultiplier, _scoringService.Multiplier);
            Assert.Equal(expectedTotal, points);
        }

        [Theory]
        [InlineData(HandType.StraightFlush, 8)]
        [InlineData(HandType.FullHouse, 4)]
        [InlineData(HandType.Flush, 4)]
        [InlineData(HandType.ThreeOfAKind, 3)]
        [InlineData(HandType.TwoPair, 2)]
        [InlineData(HandType.Pair, 2)]
        [InlineData(HandType.HighCard, 1)]
        public void CalculateMultiplierForHandEvaluation_ReturnsCorrectMultiplier(HandType handType, int expectedMultiplier)
        {
            var handEvaluation = new HandEvaluationResult(handType, new List<Card>());
            var multiplier = _scoringService.CalculateMultiplierForHandEvaluation(handEvaluation);

            Assert.Equal(expectedMultiplier, multiplier);
        }

        [Theory]
        [InlineData(HandType.StraightFlush, 100)]
        [InlineData(HandType.FullHouse, 40)]
        [InlineData(HandType.Flush, 40)]
        [InlineData(HandType.ThreeOfAKind, 30)]
        [InlineData(HandType.TwoPair, 20)]
        [InlineData(HandType.Pair, 10)]
        [InlineData(HandType.HighCard, 5)]
        public void CalculateChipsForHandEvaluation_ReturnsCorrectChips(HandType handType, int expectedChips)
        {
            var handEvaluation = new HandEvaluationResult(handType, new List<Card>());
            var chips = _scoringService.CalculateChipsForHandEvaluation(handEvaluation);

            Assert.Equal(expectedChips, chips);
        }
    }
}
