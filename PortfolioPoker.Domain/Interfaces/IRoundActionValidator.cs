using System.Collections.Generic;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.Validation;

namespace PortfolioPoker.Domain.Interfaces
{
    public interface IRoundActionValidator
    {
        ValidationResult CanPerform(Round round, GameAction action, IReadOnlyCollection<Card> cards);
        ValidationResult CanPlayHand(Round round, IEnumerable<Card> cards);
        ValidationResult CanDiscard(Round round, IEnumerable<Card> cards);
    }
}