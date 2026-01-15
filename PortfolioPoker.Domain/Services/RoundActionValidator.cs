using System.Collections.Generic;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.Rules;
using PortfolioPoker.Domain.Validation;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.Domain.Services
{
    public class RoundActionValidator : IRoundActionValidator
    {
        public ValidationResult CanPerform(Round round, GameAction action, IReadOnlyCollection<Card> cards)
        {
            if (!RoundPhaseActionRules.IsActionAllowed(round.Phase, action))
                return ValidationResult.Fail("Action not allowed in this phase");

            return action switch
            {
                GameAction.PlayHand => CanPlayHand(round, cards),
                GameAction.Discard => CanDiscard(round, cards),
                _ => ValidationResult.Success()
            };
        }

        public ValidationResult CanPlayHand(Round round, IEnumerable<Card> cards)
        {
            if (round.HandsPlayed >= round.HandsAvailable)
                return ValidationResult.Fail("No hands remaining");

            return ValidationResult.Success();
        }

        public ValidationResult CanDiscard(Round round, IEnumerable<Card> cards)
        {
            if (round.DiscardsMade >= round.DiscardsAvailable)
                return ValidationResult.Fail("No discards remaining");

            return ValidationResult.Success();
        }
    }

}