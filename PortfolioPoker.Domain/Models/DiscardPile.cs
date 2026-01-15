using System.Collections.Generic;

namespace PortfolioPoker.Domain.Models
{
    public class DiscardPile
    {
        public List<Card> Cards { get; private set; }

        public DiscardPile()
        {
            Cards = new List<Card>();
        }

        public void AddCards(List<Card> cards)
        {
            Cards.AddRange(cards);
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }
    }
}