using PortfolioPoker.Domain.Interfaces;

namespace PortfolioPoker.Application.Interfaces
{
    public interface IAnimationCoordinator
    {
        void TriggerEvents(IReadOnlyList<IGameEvent> events);

        Task WaitForAnimationsAsync();

        event Action<IReadOnlyList<IGameEvent>>? OnAnimate;
    }
}