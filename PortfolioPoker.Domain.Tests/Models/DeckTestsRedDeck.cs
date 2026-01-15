using System;
using System.Linq;
using Xunit;
using PortfolioPoker.Domain;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;

namespace PortfolioPoker.Tests.EditMode.Domain.Models
{
    
    public partial class DeckTestsRedDeck
    {
        private Deck _deck;

        public DeckTestsRedDeck()
        {
            _deck = Deck.GenerateDeckForDeckType(DeckType.RedDeck);
        }

        [Fact]
        public void RedDeck_Has52Cards()
        {
            Assert.Equal(52, _deck.Cards.Count);
        }

        [Fact]
        public void RedDeck_HasOneOfEachSuit()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                Assert.Equal(13, _deck.Cards.Count(c => c.Suit == suit));
            }
        }

        [Fact]
        public void RedDeck_HasOneOfEachRank()
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                Assert.Equal(4, _deck.Cards.Count(c => c.Rank == rank));
            }
        }

        [Fact]
        public void RedDeck_HasNoDuplicateCards()
        {
            Assert.Equal(_deck.Cards.Count, _deck.Cards.Distinct().Count());
        }
    }
}