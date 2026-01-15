using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;

namespace PortfolioPoker.Tests.EditMode.Domain.Models
{
    
    public partial class DeckTestsGeneric
    {
        private Deck _deck;

        
        public DeckTestsGeneric()
        {
            _deck = new Deck(
                new List<Card>()
            );
        }

        [Fact]
        public void AddCard_ShouldAddCardToDeck()
        {
            var card = new Card(Suit.Hearts, Rank.Two);
            _deck.AddCard(card);
            Assert.Contains(card, _deck.Cards);
        }
    }
}