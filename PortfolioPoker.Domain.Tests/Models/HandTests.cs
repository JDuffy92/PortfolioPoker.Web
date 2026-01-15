using System.Collections.Generic;
using Xunit;
using PortfolioPoker.Domain;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;

namespace PortfolioPoker.Tests.EditMode.Domain.Models
{
    
    public class HandTests
    {
        private Hand _hand;

        
        public HandTests()
        {
            _hand = new Hand(new List<Card>());
        }
        
        [Fact]
        public void AddCard_ShouldAddCardToHand()
        {
            var card = new Card(Suit.Hearts, Rank.Two);
            _hand.AddCard(card);
            Assert.Contains(card, _hand.Cards);
        }

        [Fact]
        public void AddCard_ContainsExpectedNumberOfCards()
        {
            var card = new Card(Suit.Hearts, Rank.Two);
            _hand.AddCard(card);

            Assert.Single(_hand.Cards);
        }


        [Fact]
        public void DiscardCards_ShouldRemoveCardsFromHandAndAddToDiscardPile()
        {
            var card1 = new Card(Suit.Hearts, Rank.Two);
            var card2 = new Card(Suit.Spades, Rank.Three);
            _hand.AddCard(card1);
            _hand.AddCard(card2);

            var discardPile = new DiscardPile();
            _hand.DiscardCards(new List<Card> { card1, card2 }, discardPile);

            Assert.DoesNotContain(card1, _hand.Cards);
            Assert.DoesNotContain(card2, _hand.Cards);
            Assert.Contains(card1, discardPile.Cards);
            Assert.Contains(card2, discardPile.Cards);
        }

        [Fact]
        public void SortByRank_ShouldSortCardsInHandByRank()
        {
            var card1 = new Card(Suit.Hearts, Rank.Five);
            var card2 = new Card(Suit.Spades, Rank.Two);
            var card3 = new Card(Suit.Diamonds, Rank.Three);
            _hand.AddCard(card1);
            _hand.AddCard(card2);
            _hand.AddCard(card3);

            var sortedCards = _hand.SortByRank();

            Assert.Equal(Rank.Two, sortedCards[0].Rank);
            Assert.Equal(Rank.Three, sortedCards[1].Rank);
            Assert.Equal(Rank.Five, sortedCards[2].Rank);
        }

        [Fact]
        public void SortBySuit_ShouldSortCardsInHandBySuit()
        {
            var card1 = new Card(Suit.Spades, Rank.Five);
            var card2 = new Card(Suit.Hearts, Rank.Two);
            var card3 = new Card(Suit.Diamonds, Rank.Three);
            _hand.AddCard(card1);
            _hand.AddCard(card2);
            _hand.AddCard(card3);

            var sortedCards = _hand.SortBySuit();

            Assert.Equal(Suit.Diamonds, sortedCards[0].Suit);
            Assert.Equal(Suit.Hearts, sortedCards[1].Suit);
            Assert.Equal(Suit.Spades, sortedCards[2].Suit);
        }
    }
}