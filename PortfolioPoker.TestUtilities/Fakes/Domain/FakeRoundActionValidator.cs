using System.Collections.Generic;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.Validation;

namespace PortfolioPoker.TestUtilities.Fakes
{
    public class FakeRoundActionValidator : IRoundActionValidator
    {
        private readonly ValidationResult _validationResult;

        public FakeRoundActionValidator(ValidationResult validationResult)
        {
            _validationResult = validationResult;
        }
    
        public ValidationResult CanPerform(Round round, GameAction action, IReadOnlyCollection<Card> cards)
        {
            return _validationResult;
        }

        public ValidationResult CanPlayHand(Round round, IEnumerable<Card> cards)
        {
            return _validationResult;
        }

        public ValidationResult CanDiscard(Round round, IEnumerable<Card> cards)
        {
            return _validationResult;
        }
    }
}