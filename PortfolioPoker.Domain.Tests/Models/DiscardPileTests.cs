using System.Collections.Generic;
using Xunit;
using PortfolioPoker.Domain;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;

namespace PortfolioPoker.Tests.EditMode.Domain.Models
{
    
    public class DiscardPileTests
    {
        private DiscardPile _discardPile;
        
        public DiscardPileTests()
        {
            _discardPile = new DiscardPile();
        }
        
        [Fact]
        public void AddCards_ShouldAddCardToDiscardPile()
        {
            var cards = new List<Card> { new Card(Suit.Hearts, Rank.Two), new Card(Suit.Spades, Rank.Three) };
            _discardPile.AddCards(cards);
            Assert.Contains(cards[0], _discardPile.Cards);
            Assert.Contains(cards[1], _discardPile.Cards);
        }

        [Fact]
        public void AddCards_ContainsExpectedNumberOfCards()
        {
            var cards = new List<Card> { new Card(Suit.Hearts, Rank.Two), new Card(Suit.Spades, Rank.Three) };
            _discardPile.AddCards(cards);

            Assert.Equal(2, _discardPile.Cards.Count);
        }
    }
}