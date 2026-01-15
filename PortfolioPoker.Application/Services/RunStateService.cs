using PortfolioPoker.Application.Interfaces;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.Application.Services
{
    public class RunStateService : IRunStateService
    {
        public Run? CurrentRun { get; private set; }
        public Round? CurrentRound { get; set; }

        public IEnumerable<Card> SelectedCards { get; set; } = Enumerable.Empty<Card>();

        public event Action? OnChange;
        public event Action<IReadOnlyList<IGameEvent>>? OnGameEvents;

        public void SetCurrentRun(Run run)
        {
            CurrentRun = run;
            NotifyStateChanged();
        }

        public void StartRound(Round round)
        {
            CurrentRound = round;
            NotifyStateChanged();
        }

        public void ClearRound()
        {
            CurrentRound = null;
            NotifyStateChanged();
        }

        public void Clear()
        {
            CurrentRun = null;
            CurrentRound = null;
            SelectedCards = Enumerable.Empty<Card>();
            NotifyStateChanged();
        }

        public void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }

        public void ApplyResult<T>(PerformActionResult<T> result) where T : class
        {
            if (!result.Success)
                return;

            // Notify animations FIRST
            if (result.Events.Count > 0)
                OnGameEvents?.Invoke(result.Events);

            // Notify UI state AFTER mutation
            OnChange?.Invoke();
        }

        public void SelectCard(Card card)
        {
            if (!SelectedCards.Contains(card))
            {
                SelectedCards = SelectedCards.Append(card);
                NotifyStateChanged();
            }
        }

        public void DeselectCard(Card card)
        {
            if (SelectedCards.Contains(card))
            {
                SelectedCards = SelectedCards.Where(c => c != card);
                NotifyStateChanged();
            }
        }

        public void DeselectAllCards()
        {
            SelectedCards = Enumerable.Empty<Card>();
            NotifyStateChanged();
        }

        
        public void SortHandByRank()
        {
            var sortedCards = CurrentRound?.Hand.SortByRank(); // Get sorted cards

            if (sortedCards != null)
            {
                CurrentRound?.Hand.Cards.Clear(); // Clear existing cards
                CurrentRound?.Hand.Cards.AddRange(sortedCards); // Add sorted cards back
                NotifyStateChanged();
            }
        }

        public void SortHandBySuit()
        {
            var sortedCards = CurrentRound?.Hand.SortBySuit();

            if (sortedCards != null)
            {
                CurrentRound?.Hand.Cards.Clear(); // Clear existing cards
                CurrentRound?.Hand.Cards.AddRange(sortedCards); // Add sorted cards back
                NotifyStateChanged();
            }
        }
    }
}
