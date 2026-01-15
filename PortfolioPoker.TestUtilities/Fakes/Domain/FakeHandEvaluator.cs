using System.Collections.Generic;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.TestUtilities.Fakes
{
    public class FakeHandEvaluator : IHandEvaluator
    {
        private readonly HandType _handTypeToReturn;

        public FakeHandEvaluator(HandType handTypeToReturn)
        {
            _handTypeToReturn = handTypeToReturn;
        }

        public HandEvaluationResult Evaluate(IEnumerable<Card> cards)
        {
            return new HandEvaluationResult(_handTypeToReturn, cards);
        }
    }
}
