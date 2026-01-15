
using PortfolioPoker.Application.Interfaces;
using PortfolioPoker.Domain.Interfaces;

namespace PortfolioPoker.Application.Services
{
    public class AnimationCoordinator : IAnimationCoordinator
    {
        private TaskCompletionSource<bool>? _tcs;

        // Event fired so UI components can react to game events
        public event Action<IReadOnlyList<IGameEvent>>? OnAnimate;

        // Trigger animations for a set of events
        public void TriggerEvents(IReadOnlyList<IGameEvent> events)
        {
            if (events.Count == 0) return;

            _tcs = new TaskCompletionSource<bool>();

            // Notify components to run animations (optional: update UI immediately)
            OnAnimate?.Invoke(events);

            // For now, we'll just simulate animation via Task.Delay
            SimulateAnimationDelay(events.Count);
        }

        private async void SimulateAnimationDelay(int eventCount)
        {
            // Simple animation: 300ms per event
            await Task.Delay(eventCount * 300);

            // Mark the animation as complete
            _tcs?.SetResult(true);
        }

        // Await this to ensure sequence waits for animations
        public Task WaitForAnimationsAsync()
        {
            return _tcs?.Task ?? Task.CompletedTask;
        }
    }
}