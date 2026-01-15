using System.Collections.Generic;
using PortfolioPoker.Application.Interfaces;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.TestUtilities.Fakes
{
    public class FakeRoundDescriptorFactory : IRoundDescriptorFactory
    {
        public IReadOnlyList<RoundDescriptor> CreateRounds(RunConfig config)
        {
            var rounds = new List<RoundDescriptor>();

            int startingScore = 10;

            for (int i = 0; i <= config.TotalRounds; i++)
            {
                rounds.Add(new RoundDescriptor(roundNumber: i, pointTarget: startingScore + (i * config.ScoreIncrementPerRound)));
            }

            return rounds.AsReadOnly();
        }
    }
}