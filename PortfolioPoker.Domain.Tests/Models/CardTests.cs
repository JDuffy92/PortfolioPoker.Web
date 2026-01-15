using System.Collections.Generic;
using Xunit;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;

namespace PortfolioPoker.Tests.EditMode.Domain.Models
{
    
    public class CardTests
    {
        private List<Card> _cards;

        
        public CardTests()
        {
            _cards = new List<Card>();
        }
        
        [Fact]
        public void AddCard_ShouldAddCardToHand()
        {
            var card = new Card(Suit.Hearts, Rank.Two);
            _cards.Add(card);

            Assert.Contains(card, _cards);
        }

        [Fact]
        public void AddCard_HasExpectedBasePoints()
        {
            var card = new Card(Suit.Hearts, Rank.Two);
            _cards.Add(card);

            //Calculate the base points for our card
            int basePoints = card.GetBaseChips();
            Assert.Equal(2, basePoints);
        }
    }
}