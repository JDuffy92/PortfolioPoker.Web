using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;

namespace PortfolioPoker.Domain.Services
{
    public class RoundPhaseTransitionService : IRoundPhaseTransitionService
    {
        public  bool CanTransition(RoundPhase from, RoundPhase to)
        {
            //If the phases are the same, no transition needed
            if (from == to)
                return true;

            return from switch
            {
                RoundPhase.StartPhase => to == RoundPhase.DrawPhase,          // From start to drawing cards
                RoundPhase.DrawPhase => to == RoundPhase.SelectPhase,           // After drawing, select cards
                RoundPhase.SelectPhase => to == RoundPhase.PlayPhase || to == RoundPhase.DiscardPhase,      // Can either play or discard
                RoundPhase.PlayPhase => to == RoundPhase.DrawPhase || to == RoundPhase.RoundEnd,          // Draw replacement cards, or round end if failure
                RoundPhase.DiscardPhase => to == RoundPhase.DrawPhase,          // Draw replacement cards
                                                                                // RoundPhase.RoundEnd => to == RoundPhase.DrawPhase,             // Next round starts here --  Should go back to Menu or Setup phase instead
                _ => false
            };
        }
    }

}