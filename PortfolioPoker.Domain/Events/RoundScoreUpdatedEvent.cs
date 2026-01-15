using PortfolioPoker.Domain.Interfaces;

namespace PortfolioPoker.Domain.Events
{
    public sealed class ScoreUpdatedEvent : IGameEvent
    {
        public int PreviousScore { get; }
        public int NewScore { get; }

        public ScoreUpdatedEvent(int previous, int current)
        {
            PreviousScore = previous;
            NewScore = current;
        }

        public string Description => $"Score updated: {PreviousScore} → {NewScore}";
    }

}