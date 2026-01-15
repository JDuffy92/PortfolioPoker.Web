using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;

namespace PortfolioPoker.Domain.Services
{
    public class RoundStateEvaluator : IRoundStateEvaluator
    {
        public RoundStatus Evaluate(Round round)
        {
            if (round.CurrentPoints >= round.PointTarget)
                return RoundStatus.Success;

            if (round.HandsPlayed >= round.HandsAvailable)
                return RoundStatus.Failure;

            return RoundStatus.Active;
        }
    }

}