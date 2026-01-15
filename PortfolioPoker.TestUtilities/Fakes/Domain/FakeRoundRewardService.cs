using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.TestUtilities.Fakes
{
    public class FakeRoundRewardService : IRoundRewardService
    {
        private int _rewardAmount;

        public FakeRoundRewardService(int rewardAmount)
        {
            _rewardAmount = rewardAmount;
        }

        public Money CalculateReward(Run run, Round round)
        {
            // Implement reward calculation logic here
            return new Money(_rewardAmount); // Placeholder
        }
    }
}