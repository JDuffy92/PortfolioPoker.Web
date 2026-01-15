using System;
using System.Collections.Generic;

namespace PortfolioPoker.Domain.Models
{
    public class Hand
    {
        public List<Card> Cards { get; private set; }

        public Hand(List<Card> cards)
        {
            Cards = cards;
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        public void DiscardCards(IEnumerable<Card> cards, DiscardPile discardPile)
        {
            foreach (var card in cards)
            {
                if (!Cards.Contains(card))
                    throw new InvalidOperationException("Card not in hand");

                Cards.Remove(card);
                discardPile.AddCard(card);
            }
        }
    }
}