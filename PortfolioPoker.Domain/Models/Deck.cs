using System;
using System.Collections.Generic;
using System.Linq;
using PortfolioPoker.Domain.Enums;

namespace PortfolioPoker.Domain.Models
{
    public class Deck
    {
        public List<Card> Cards { get; private set; }

        public Deck(List<Card> cards)
        {
            Cards = cards;
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }


        //Generate Deck based on DeckType
        public static Deck GenerateDeckForDeckType(DeckType deckType)
        {
            Deck deck = deckType switch
            {
                DeckType.RedDeck => CreateRedDeck(),
                DeckType.BlueDeck => CreateBlueDeck(),
                _ => throw new ArgumentException("Invalid Deck Type"),
            };

            return deck;
        }

        //Red Deck (Starter Deck)
        private static Deck CreateRedDeck()
        {
            //Loop over each suit and rank to create a standard 52-card deck
           var cards = new List<Card>();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card(suit, rank));
                }
            }

            return new Deck(cards);
        }

        //Blue Deck (Contains 52 Random Cards)
        private static Deck CreateBlueDeck()
        {
            var cards = new List<Card>();

            for (int i = 0; i < 52; i++)
            {
                var suit = (Suit)(i % 4);
                var rank = (Rank)(i % 13);
                cards.Add(new Card(suit, rank));
            }

            return new Deck(cards);
        }
  
        public Deck Clone()
        {
            // Cards are value-like, safe to copy references
            return new Deck(Cards.ToList());
        }

        public Deck CloneAndShuffle(Random rng)
        {
            if (rng == null)
                throw new ArgumentNullException(nameof(rng));

            var clonedCards = Cards.ToList();

            ShuffleInPlace(clonedCards, rng);

            return new Deck(clonedCards);
        }

        // Fisher–Yates shuffle (deterministic with RNG)
        private static void ShuffleInPlace(List<Card> cards, Random rng)
        {
            for (int i = cards.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                (cards[i], cards[j]) = (cards[j], cards[i]);
            }
        }


        public static int GetHandsForDeckType(DeckType deckType)
        {
            int result = deckType switch
            {
                DeckType.RedDeck => 5,
                DeckType.BlueDeck => 4,
                _ => throw new ArgumentException("Invalid Deck Type"),
            };

            return result;
        }

        public static int GetDiscardsForDeckType(DeckType deckType)
        {
            int result = deckType switch
            {
                DeckType.RedDeck => 3,
                DeckType.BlueDeck => 3,
                _ => throw new ArgumentException("Invalid Deck Type"),
            };

            return result;
        }
    }
}