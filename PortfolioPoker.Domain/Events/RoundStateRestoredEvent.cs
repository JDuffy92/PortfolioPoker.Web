using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;

namespace PortfolioPoker.Domain.Events
{
    public class RoundStateRestoredEvent : IGameEvent
    {
        public Round PreviousState { get; }
        public RoundStateRestoredEvent(Round previous) => PreviousState = previous;

        public string Description => $"Round State Restored: {PreviousState}";
    }

}