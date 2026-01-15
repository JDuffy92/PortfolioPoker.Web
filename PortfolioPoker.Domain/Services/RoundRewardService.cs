using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.Domain.Services
{
    public class RoundRewardService : IRoundRewardService
    {
        public Money CalculateReward(Run run, Round round)
        {
            //TODO: Implement reward calculation logic here
            return new Money(5); // Placeholder
        }
    }
}