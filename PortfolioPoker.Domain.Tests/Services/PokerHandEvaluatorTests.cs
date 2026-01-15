using Xunit;
using System.Collections.Generic;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.Services;

namespace PortfolioPoker.Domain.Tests.Services
{
    
    public class PokerHandEvaluatorTests
    {
        private PokerHandEvaluator _evaluator;

        
        public PokerHandEvaluatorTests()
        {
            _evaluator = new PokerHandEvaluator();
        }

        [Fact]
        public void Evaluate_RoyalFlush_ReturnsRoyalFlush()
        {
            var cards = new List<Card>
            {
                new Card(rank: Rank.Ace, suit: Suit.Hearts),
                new Card(rank: Rank.King, suit: Suit.Hearts),
                new Card(rank: Rank.Queen, suit: Suit.Hearts),
                new Card(rank: Rank.Jack, suit: Suit.Hearts),
                new Card(rank: Rank.Ten, suit: Suit.Hearts),
                new Card(rank: Rank.Two, suit: Suit.Diamonds),
                new Card(rank: Rank.Three, suit: Suit.Clubs)
            };

            var result = _evaluator.Evaluate(cards);
            Assert.Equal(HandType.RoyalFlush, result.HandType);
        }

        [Fact]
        public void Evaluate_StraightFlush_ReturnsStraightFlush()
        {
            var cards = new List<Card>
            {
                new Card(rank: Rank.Nine, suit: Suit.Hearts),
                new Card(rank: Rank.Eight, suit: Suit.Hearts),
                new Card(rank: Rank.Seven, suit: Suit.Hearts),
                new Card(rank: Rank.Six, suit: Suit.Hearts),
                new Card(rank: Rank.Five, suit: Suit.Hearts),
                new Card(rank: Rank.Two, suit: Suit.Diamonds),
                new Card(rank: Rank.Three, suit: Suit.Clubs)
            };

            var result = _evaluator.Evaluate(cards);
            Assert.Equal(HandType.StraightFlush, result.HandType);
        }

        [Fact]
        public void Evaluate_FourOfAKind_ReturnsFourOfAKind()
        {
            var cards = new List<Card>
            {
                new Card(rank: Rank.King, suit: Suit.Hearts),
                new Card(rank: Rank.King, suit: Suit.Diamonds),
                new Card(rank: Rank.King, suit: Suit.Clubs),
                new Card(rank: Rank.King, suit: Suit.Spades),
                new Card(rank: Rank.Two, suit: Suit.Hearts),
                new Card(rank: Rank.Three, suit: Suit.Diamonds),
                new Card(rank: Rank.Four, suit: Suit.Clubs)
            };

            var result = _evaluator.Evaluate(cards);
            Assert.Equal(HandType.FourOfAKind, result.HandType);
        }

        [Fact]
        public void Evaluate_FullHouse_ReturnsFullHouse()
        {
            var cards = new List<Card>
            {
                new Card(rank: Rank.King, suit: Suit.Hearts),
                new Card(rank: Rank.King, suit: Suit.Diamonds),
                new Card(rank: Rank.King, suit: Suit.Clubs),
                new Card(rank: Rank.Queen, suit: Suit.Hearts),
                new Card(rank: Rank.Queen, suit: Suit.Diamonds),
                new Card(rank: Rank.Two, suit: Suit.Clubs),
                new Card(rank: Rank.Three, suit: Suit.Hearts)
            };

            var result = _evaluator.Evaluate(cards);
            Assert.Equal(HandType.FullHouse, result.HandType);
        }

        [Fact]
        public void Evaluate_Flush_ReturnsFlush()
        {
            var cards = new List<Card>
            {
                new Card(rank: Rank.Ace, suit: Suit.Hearts),
                new Card(rank: Rank.King, suit: Suit.Hearts),
                new Card(rank: Rank.Queen, suit: Suit.Hearts),
                new Card(rank: Rank.Jack, suit: Suit.Hearts),
                new Card(rank: Rank.Nine, suit: Suit.Hearts),
                new Card(rank: Rank.Two, suit: Suit.Diamonds),
                new Card(rank: Rank.Three, suit: Suit.Clubs)
            };

            var result = _evaluator.Evaluate(cards);
            Assert.Equal(HandType.Flush, result.HandType);
        }

        [Fact]
        public void Evaluate_Straight_ReturnsStraight()
        {
            var cards = new List<Card>
            {
                new Card(rank: Rank.Nine, suit: Suit.Hearts),
                new Card(rank: Rank.Eight, suit: Suit.Diamonds),
                new Card(rank: Rank.Seven, suit: Suit.Clubs),
                new Card(rank: Rank.Six, suit: Suit.Hearts),
                new Card(rank: Rank.Five, suit: Suit.Diamonds),
                new Card(rank: Rank.Two, suit: Suit.Clubs),
                new Card(rank: Rank.Three, suit: Suit.Hearts)
            };

            var result = _evaluator.Evaluate(cards);
            Assert.Equal(HandType.Straight, result.HandType);
        }

        [Fact]
        public void Evaluate_ThreeOfAKind_ReturnsThreeOfAKind()
        {
            var cards = new List<Card>
            {
                new Card(rank: Rank.King, suit: Suit.Hearts),
                new Card(rank: Rank.King, suit: Suit.Diamonds),
                new Card(rank: Rank.King, suit: Suit.Clubs),
                new Card(rank: Rank.Queen, suit: Suit.Hearts),
                new Card(rank: Rank.Jack, suit: Suit.Diamonds),
                new Card(rank: Rank.Two, suit: Suit.Clubs),
                new Card(rank: Rank.Three, suit: Suit.Hearts)
            };

            var result = _evaluator.Evaluate(cards);
            Assert.Equal(HandType.ThreeOfAKind, result.HandType);
        }

        [Fact]
        public void Evaluate_TwoPair_ReturnsTwoPair()
        {
            var cards = new List<Card>
            {
                new Card(rank: Rank.King, suit: Suit.Hearts),
                new Card(rank: Rank.King, suit: Suit.Diamonds),
                new Card(rank: Rank.Queen, suit: Suit.Clubs),
                new Card(rank: Rank.Queen, suit: Suit.Hearts),
                new Card(rank: Rank.Jack, suit: Suit.Diamonds),
                new Card(rank: Rank.Two, suit: Suit.Clubs),
                new Card(rank: Rank.Three, suit: Suit.Hearts)
            };

            var result = _evaluator.Evaluate(cards);
            Assert.Equal(HandType.TwoPair, result.HandType);
        }

        [Fact]
        public void Evaluate_Pair_ReturnsPair()
        {
            var cards = new List<Card>
            {
                new Card(rank: Rank.King, suit: Suit.Hearts),
                new Card(rank: Rank.King, suit: Suit.Diamonds),
                new Card(rank: Rank.Queen, suit: Suit.Clubs),
                new Card(rank: Rank.Jack, suit: Suit.Hearts),
                new Card(rank: Rank.Nine, suit: Suit.Diamonds),
                new Card(rank: Rank.Two, suit: Suit.Clubs),
                new Card(rank: Rank.Three, suit: Suit.Hearts)
            };

            var result = _evaluator.Evaluate(cards);
            Assert.Equal(HandType.Pair, result.HandType);
        }

        [Fact]
        public void Evaluate_HighCard_ReturnsHighCard()
        {
            var cards = new List<Card>
            {
                new Card(rank: Rank.Ace, suit: Suit.Hearts),
                new Card(rank: Rank.King, suit: Suit.Diamonds),
                new Card(rank: Rank.Queen, suit: Suit.Clubs),
                new Card(rank: Rank.Jack, suit: Suit.Hearts),
                new Card(rank: Rank.Nine, suit: Suit.Diamonds),
                new Card(rank: Rank.Two, suit: Suit.Clubs),
                new Card(rank: Rank.Three, suit: Suit.Hearts)
            };

            var result = _evaluator.Evaluate(cards);
            Assert.Equal(HandType.HighCard, result.HandType);
        }
    }
}