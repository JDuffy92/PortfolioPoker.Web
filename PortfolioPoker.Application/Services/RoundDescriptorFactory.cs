using System.Collections.Generic;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.Enums;
using System;
using PortfolioPoker.Application.Interfaces;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.Application.Services
{
    public class RoundDescriptorFactory: IRoundDescriptorFactory
    {
        public IReadOnlyList<RoundDescriptor> CreateRounds(RunConfig config)
        {
            var rounds = new List<RoundDescriptor>();

            for (int i = 0; i < config.TotalRounds; i++)
            {
                rounds.Add(new RoundDescriptor(
                    roundNumber: i,
                    pointTarget: config.ScoreIncrementPerRound * (i + 1)
                ));
            }

            return rounds;
        }
        
    }
}