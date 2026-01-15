using System.Collections.Generic;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.Domain.Interfaces
{
    public interface IHandEvaluator
    {
        HandEvaluationResult Evaluate(IEnumerable<Card> cards);
    }
}
