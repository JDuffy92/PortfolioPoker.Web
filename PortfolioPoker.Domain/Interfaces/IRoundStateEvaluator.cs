using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;

namespace PortfolioPoker.Domain.Interfaces
{
    public interface IRoundStateEvaluator
    {
        RoundStatus Evaluate(Round round);
    }
}