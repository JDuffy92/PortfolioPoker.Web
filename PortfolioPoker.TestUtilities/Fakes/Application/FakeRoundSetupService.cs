using System;
using System.Collections.Generic;
using PortfolioPoker.Application.Interfaces;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.TestUtilities.Fakes
{
    public class FakeRoundSetupService : IRoundSetupService
    {
        private IHandEvaluator _handEvaluator;
        private IScoringService _scoringService;

        public FakeRoundSetupService(
            IHandEvaluator handEvaluator,
            IScoringService scoringService
        )
        {
            _handEvaluator = handEvaluator;
            _scoringService = scoringService;
        }

        public Round CreateRound(
            Run run,
            Random rng
        )
        {
            var round = new Round(
                handEvaluator: _handEvaluator,
                scoringService: _scoringService,
                pointTarget: 10,
                handsAvailable: 5,
                discardsAvailable: 3,
                jokers: new List<Joker>(),
                deck: Deck.GenerateDeckForDeckType(DeckType.RedDeck)
            );

            return round;
        }
    }
}
