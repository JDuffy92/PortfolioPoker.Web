namespace PortfolioPoker.Domain.Interfaces
{
    public interface IRoundPhaseTransitionService
    {
        bool CanTransition(RoundPhase from, RoundPhase to);
    }
}