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
using PortfolioPoker.TestUtilities.Fakes;

namespace PortfolioPoker.Application.Tests.Services
{
    
    public class RunProgressServiceTests
    {
        private IRoundSetupService _roundSetupService;
        private IRoundRewardService _roundRewardService;

        private List<RoundDescriptor> _roundDescriptors;
        private Run _run;

        private RunProgressService _service;

        public RunProgressServiceTests()
        {
            //
            IHandEvaluator handEvaluator = new FakeHandEvaluator(HandType.Flush);
            IScoringService scoringService = new FakeScoringService(10);

            _roundSetupService = new FakeRoundSetupService(
                handEvaluator: handEvaluator,
                scoringService: scoringService
                );

            _roundRewardService = new FakeRoundRewardService(10);

            _service = new RunProgressService(
                roundSetupService: _roundSetupService,
                rewardService: _roundRewardService
            );

            //Generate some round descriptors
            _roundDescriptors = new List<RoundDescriptor>
            {
                new RoundDescriptor(1, pointTarget: 10),
                new RoundDescriptor(2, pointTarget: 20),
                new RoundDescriptor(3, pointTarget: 30)
            };

            _run = new Run(
                roundRewardService: _roundRewardService,
                jokers: new List<Joker>(),
                startingDeck: Deck.GenerateDeckForDeckType(DeckType.RedDeck),
                money: new Money(0),
                config: new RunConfig(
                    seed: 0,
                    deckType: DeckType.RedDeck,
                    startingMoney: new Money(0),
                    totalRounds: 3,
                    scoreIncrementPerRound: 10
                ),
                rounds: _roundDescriptors
            );
        }

        [Fact]
        public void CanStartNextRound_ReturnsTrue_WhenRunHasNextRound()
        {
            // Act
            bool canStart = _service.CanStartNextRound(_run);

            // Assert
            Assert.True(canStart);
        }

        [Fact]
        public void StartNextRound_ReturnsNewRound_WhenRunHasNextRound()
        {
            // Act
            Round round = _service.StartNextRound(_run);

            // Assert is of type Round
            Assert.IsType<Round>(round);
        }

        [Fact]
        public void IsRunComplete_ReturnsFalse_WhenRunIsNotComplete()
        {
            // Act
            bool isComplete = _service.IsRunComplete(_run);

            // Assert
            Assert.False(isComplete);
        }

        [Fact]
        public void IsRunComplete_ReturnsTrue_WhenRunIsComplete()
        {
            // Arrange - progress through all rounds
            while (_service.CanStartNextRound(_run))
            {
                _service.StartNextRound(_run);
                _run.CompleteCurrentRound();
            }

            // Act
            bool isComplete = _service.IsRunComplete(_run);

            // Assert
            Assert.True(isComplete);
        }
    }
}