using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.Domain.Interfaces
{
    public interface IRoundRewardService
    {
        Money CalculateReward(Run run, Round round);
    }
}