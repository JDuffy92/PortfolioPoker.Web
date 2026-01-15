using System.Collections.Generic;
using System.Linq;
using PortfolioPoker.Domain.Interfaces;

namespace PortfolioPoker.Domain.Events
{
    public sealed class RoundPhaseChangedEvent : IGameEvent
    {
        public RoundPhase NewPhase { get; }

        public RoundPhaseChangedEvent(RoundPhase newPhase)
        {
            NewPhase = newPhase;
        }

        public string Description => $"Round phase changed to {NewPhase}";
    }
}