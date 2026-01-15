using System.Collections.Generic;
using System.Linq;
using PortfolioPoker.Domain.Enums;

namespace PortfolioPoker.Domain.Rules
{
    public static class RoundPhaseActionRules
    {
        private static readonly Dictionary<RoundPhase, GameAction[]> AllowedActions =
            new()
            {
                {
                    RoundPhase.SelectPhase,
                    new[] { GameAction.PlayHand, GameAction.Discard }
                },
            };

        public static bool IsActionAllowed(RoundPhase phase, GameAction action)
            => AllowedActions.TryGetValue(phase, out var actions)
               && actions.Contains(action);
    }

}