using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.Domain.Models{
    public class Card
    {
        public Suit Suit { get; private set; }
        public Rank Rank { get; private set; }

        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public int GetBaseChips()
        {
            int points = this.Rank switch
            {
                Rank.Two => 2,
                Rank.Three => 3,
                Rank.Four => 4,
                Rank.Five => 5,
                Rank.Six => 6,
                Rank.Seven => 7,
                Rank.Eight => 8,
                Rank.Nine => 9,
                Rank.Ten => 10,
                Rank.Jack => 10,
                Rank.Queen => 10,
                Rank.King => 10,
                Rank.Ace => 11,
                _ => 0
            };

            return points;
        }

        public Money Cost()
        {
            //TODO: Implement more complex cost logic based on card properties
            return new Money(1); // Default cost for a card is 1 money unit
        }
    }
}