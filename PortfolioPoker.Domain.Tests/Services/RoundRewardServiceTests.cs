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
    
    public class RoundRewardServiceTests
    {
        private RoundRewardService _roundRewardService;

        private Round _round;

        
        public RoundRewardServiceTests()
        {
            _roundRewardService = new RoundRewardService();

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
    }
}