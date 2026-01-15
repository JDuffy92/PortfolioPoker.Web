using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.Application.Interfaces
{
    public interface IRunStateService
    {
        Run? CurrentRun { get; }
        Round? CurrentRound { get; set; }
        event Action? OnChange;
        event Action<IReadOnlyList<IGameEvent>> OnGameEvents;

        IEnumerable<Card> SelectedCards { get; set; }


        void SetCurrentRun(Run run);
        void StartRound(Round round);

        void ClearRound();

        void Clear();

        void NotifyStateChanged();

        void ApplyResult<T>(PerformActionResult<T> result) where T : class;

        void SelectCard(Card card);
        void DeselectCard(Card card);

        void DeselectAllCards();

        void SetSortMode(HandSortMode mode);

        IReadOnlyList<Card> GetSortedHandCards();
    }
}
