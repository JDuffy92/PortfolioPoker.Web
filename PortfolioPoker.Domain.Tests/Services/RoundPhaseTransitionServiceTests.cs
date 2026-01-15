using Xunit;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.Services;
using PortfolioPoker.Domain.ValueObjects;
using System.Collections.Generic;

namespace PortfolioPoker.Domain.Tests.Services
{
    
    public class RoundPhaseTransitionServiceTests
    {
        private RoundPhaseTransitionService _roundPhaseTransitionService;

        
        public RoundPhaseTransitionServiceTests()
        {
            _roundPhaseTransitionService = new RoundPhaseTransitionService();
        }

        [Theory]
        [InlineData(RoundPhase.DrawPhase, RoundPhase.SelectPhase, true)]
        [InlineData(RoundPhase.DrawPhase, RoundPhase.PlayPhase, false)]
        [InlineData(RoundPhase.DrawPhase, RoundPhase.DiscardPhase, false)]
        [InlineData(RoundPhase.DrawPhase, RoundPhase.RoundEnd, false)]
        [InlineData(RoundPhase.SelectPhase, RoundPhase.DrawPhase, false)]
        [InlineData(RoundPhase.SelectPhase, RoundPhase.PlayPhase, true)]
        [InlineData(RoundPhase.SelectPhase, RoundPhase.DiscardPhase, true)]
        [InlineData(RoundPhase.SelectPhase, RoundPhase.RoundEnd, false)]
        [InlineData(RoundPhase.PlayPhase, RoundPhase.DrawPhase, true)]
        [InlineData(RoundPhase.PlayPhase, RoundPhase.SelectPhase, false)]
        [InlineData(RoundPhase.PlayPhase, RoundPhase.DiscardPhase, false)]
        [InlineData(RoundPhase.PlayPhase, RoundPhase.RoundEnd, true)]
        [InlineData(RoundPhase.RoundEnd, RoundPhase.DrawPhase, false)]
        [InlineData(RoundPhase.RoundEnd, RoundPhase.SelectPhase, false)]
        [InlineData(RoundPhase.RoundEnd, RoundPhase.DiscardPhase, false)]
        [InlineData(RoundPhase.RoundEnd, RoundPhase.PlayPhase, false)]
        public void CanTransition_ReturnsExpectedResult(RoundPhase from, RoundPhase to, bool expected)
        {
            // Act
            var result = _roundPhaseTransitionService.CanTransition(from, to);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}