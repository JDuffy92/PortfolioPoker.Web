using System.Collections.Generic;
using System.Linq;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;

namespace PortfolioPoker.Domain.Events
{
    public sealed class CardsPlayedEvent : IGameEvent
    {
        public IReadOnlyList<Card> Cards { get; }

        public CardsPlayedEvent(IEnumerable<Card> cards)
        {
            Cards = cards.ToList();
        }

        public string Description => $"Played {Cards.Count} cards";
    }
}