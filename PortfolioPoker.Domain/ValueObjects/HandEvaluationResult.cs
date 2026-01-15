using System.Collections.Generic;
using System.Linq;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;

namespace PortfolioPoker.Domain.ValueObjects
{
    public class HandEvaluationResult
    {
        public HandType HandType { get; }
        public IReadOnlyList<Card> Cards { get; }

        public HandEvaluationResult(
            HandType handType,
            IEnumerable<Card> cards)
        {
            HandType = handType;
            Cards = cards.ToList().AsReadOnly();
        }
    }
}
