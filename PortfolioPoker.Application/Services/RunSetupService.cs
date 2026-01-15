using System.Collections.Generic;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.Enums;
using System;
using PortfolioPoker.Domain.ValueObjects;
using PortfolioPoker.Application.Interfaces;

namespace PortfolioPoker.Application.Services
{
    public class RunSetupService : IRunSetupService
    {
        //Create New Run
        //Select Starting Deck
        //Intialise Run with Rounds
        private readonly IRoundDescriptorFactory _roundDescriptorFactory;
        private readonly IRoundRewardService _roundRewardService;


        public RunSetupService(
            IRoundDescriptorFactory roundDescriptorFactory,
            IRoundRewardService roundRewardService
        )
        {
            _roundDescriptorFactory = roundDescriptorFactory;
            _roundRewardService = roundRewardService;
        }

        public Run CreateRun(
            DeckType deckType,
            int? seedOverride = null
        )
        {
            //If no seed provided, generate one
            int seed = seedOverride ?? GenerateSeed();

            var config = new RunConfig(
                seed: seed,
                deckType: deckType,
                startingMoney: new Money(0),
                totalRounds: 5,
                scoreIncrementPerRound: 20
            );

            var deck = Deck.GenerateDeckForDeckType(deckType);

            var rounds = _roundDescriptorFactory.CreateRounds(config);

            return new Run(
                roundRewardService: _roundRewardService,
                config: config,
                jokers: new List<Joker>(),
                startingDeck: deck,
                money: config.StartingMoney,
                rounds: rounds
            );
        }

        private static int GenerateSeed()
        {
            return new Random().Next(int.MinValue, int.MaxValue);
        }
    }
}
