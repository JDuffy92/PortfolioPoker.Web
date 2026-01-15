using System.Collections.Generic;
using System.Linq;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;

namespace PortfolioPoker.Domain.Events
{
    public sealed class CardsDrawnEvent : IGameEvent
    {
        public IReadOnlyList<Card> Cards { get; }

        public CardsDrawnEvent(IEnumerable<Card> cards)
        {
            Cards = cards.ToList();
        }

        public string Description => $"Drew {Cards.Count} cards";
    }
}