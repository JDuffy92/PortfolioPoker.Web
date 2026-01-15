using System.Collections.Generic;
using PortfolioPoker.Domain;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.TestUtilities.Fakes
{
    public class FakeRoundPhaseTransitionService : IRoundPhaseTransitionService
    {
        private readonly bool _canTransition;

        public FakeRoundPhaseTransitionService(bool canTransition)
        {
            _canTransition = canTransition;
        }
    
        public bool CanTransition(RoundPhase from, RoundPhase to)
        {
            return _canTransition;
        }
    }
}