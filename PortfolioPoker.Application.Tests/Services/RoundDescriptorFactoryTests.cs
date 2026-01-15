using Xunit;
using PortfolioPoker.Application.Services;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.Services;
using PortfolioPoker.Domain.Validation;
using PortfolioPoker.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.Application.Tests.Services
{
    
    public class RoundDescriptorFactoryTests
    {
        private IRoundDescriptorFactory _roundDescriptorFactory;

        public RoundDescriptorFactoryTests()
        {
            _roundDescriptorFactory = new RoundDescriptorFactory();
        }

        [Fact]
        public void CreateRounds_GeneratesCorrectNumberOfRounds()
        {
            // Arrange
            var config = new RunConfig(
                seed: 0,
                deckType: DeckType.RedDeck,
                startingMoney: new Money(0),
                totalRounds: 5,
                scoreIncrementPerRound: 10
            );

            // Act
            var rounds = _roundDescriptorFactory.CreateRounds(config);

            // Assert
            Assert.Equal(5, rounds.Count);
        }
    }
}