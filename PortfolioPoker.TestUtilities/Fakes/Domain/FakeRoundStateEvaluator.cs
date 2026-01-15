using System.Collections.Generic;
using PortfolioPoker.Domain;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.Validation;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.TestUtilities.Fakes
{
    public class FakeRoundStateEvaluator : IRoundStateEvaluator
    {
        private readonly RoundStatus _roundStatusToReturn;

        public FakeRoundStateEvaluator(RoundStatus roundStatusToReturn)
        {
            _roundStatusToReturn = roundStatusToReturn;
        }

        public RoundStatus Evaluate(Round round)
        {
            return _roundStatusToReturn;
        }
    }
}