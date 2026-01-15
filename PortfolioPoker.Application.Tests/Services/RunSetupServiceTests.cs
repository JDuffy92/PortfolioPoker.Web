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
using PortfolioPoker.TestUtilities.Fakes;

namespace PortfolioPoker.Application.Tests.Services
{
    
    public class RunSetupServiceTests
    {
        private IRoundDescriptorFactory _roundDescriptorFactory;
        private IRoundRewardService _roundRewardService;

        private RunSetupService _service;

        public RunSetupServiceTests()
        {
            _roundDescriptorFactory = new FakeRoundDescriptorFactory();

            _roundRewardService = new FakeRoundRewardService(10);

            _service = new RunSetupService(
                roundDescriptorFactory: _roundDescriptorFactory,
                roundRewardService: _roundRewardService
            );
        }

        [Fact]
        public void CreateRun_WithSeedOverride_SetsSeedCorrectly()
        {
            // Arrange
            int seedOverride = 12345;

            // Act
            Run run = _service.CreateRun(DeckType.RedDeck, seedOverride);

            // Assert
            Assert.Equal(seedOverride, run.Config.Seed);
        }

        [Fact]
        public void CreateRun_WithoutSeedOverride_GeneratesSeed()
        {
            // Act
            Run run = _service.CreateRun(DeckType.RedDeck);

            // Assert
            Assert.IsType<int>(run.Config.Seed);
        }

        [Fact]
        public void CreateRun_SetsDeckTypeCorrectly()
        {
            // Act
            Run run = _service.CreateRun(DeckType.BlueDeck);

            // Assert
            Assert.Equal(DeckType.BlueDeck, run.Config.DeckType);
        }
    }
}