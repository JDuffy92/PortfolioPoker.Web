using System.Collections.Generic;
using PortfolioPoker.Application.Interfaces;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.Enums;
using System;

namespace PortfolioPoker.Application.Services
{
    public class RoundSetupService : IRoundSetupService
    {
        private readonly IHandEvaluator _handEvaluator;
        private readonly IScoringService _scoringService;

        public RoundSetupService(
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
            //Get current RoundDescriptor
            var roundDescriptor = run.GetCurrentRoundDescriptor();

            //TODO: Check if we need to clone the Jokers/Deck etc to not affect the Run's state
            var deck = run.Deck.CloneAndShuffle(rng);
            var jokers = new List<Joker>(run.Jokers);

            return new Round(
                _handEvaluator,
                _scoringService,
                pointTarget: roundDescriptor.PointTarget,
                handsAvailable: run.Hands,
                discardsAvailable: run.Discards,
                jokers: jokers,
                deck: deck
            );
        }
    }
}
