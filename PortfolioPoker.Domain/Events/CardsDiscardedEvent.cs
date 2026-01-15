using System.Collections.Generic;
using System.Linq;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;

namespace PortfolioPoker.Domain.Events
{
    public sealed class CardsDiscardedEvent : IGameEvent
    {
        public IReadOnlyList<Card> Cards { get; }

        public CardsDiscardedEvent(IEnumerable<Card> cards)
        {
            Cards = cards.ToList();
        }

        public string Description => $"Discarded {Cards.Count} cards";
    }
}