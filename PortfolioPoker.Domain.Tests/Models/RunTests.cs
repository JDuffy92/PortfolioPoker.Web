using System.Collections.Generic;
using Xunit;
using PortfolioPoker.Domain;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.ValueObjects;
using PortfolioPoker.TestUtilities.Fakes;

namespace PortfolioPoker.Tests.EditMode.Domain.Models
{
    
    public class RunTests
    {
        private FakeRoundRewardService _fakeRoundRewardService;
        private RunConfig _config;
        private IEnumerable<Joker> _jokers;
        private Deck _deck;
        private Money _money;
        private List<RoundDescriptor> _rounds;
        private Run _run;

        
        public RunTests()
        {
            _fakeRoundRewardService = new FakeRoundRewardService(500);

            _config = new RunConfig(
                seed: 42,
                deckType: DeckType.RedDeck,
                startingMoney: new Money(1000),
                totalRounds: 3,
                scoreIncrementPerRound: 100
            );

            _jokers = new List<Joker> { };
            _deck = Deck.GenerateDeckForDeckType(_config.DeckType);
            _money = _config.StartingMoney;
            _rounds = new List<RoundDescriptor> { new RoundDescriptor(0, 100), new RoundDescriptor(1, 200), new RoundDescriptor(2, 300) };


            _run = new Run(_fakeRoundRewardService, _config, _jokers, _deck, _money, _rounds);
        }

        [Fact]
        public void CalculateAndApplyRewardsForRound_AddsCorrectRewardToMoney()
        {
            // Arrange
            var round = new Round(
                handEvaluator: new FakeHandEvaluator(HandType.Pair),
                scoringService: new FakeScoringService(10),
                pointTarget: 100,
                handsAvailable: 5,
                discardsAvailable: 2,
                jokers: _run.Jokers,
                deck: Deck.GenerateDeckForDeckType(DeckType.RedDeck)
            );

            var expectedReward = new Money(500);

            var initialMoney = _run.Money;

            // Act
            _run.CalculateAndApplyRewardsForRound(round);

            // Assert
            var expectedMoney = initialMoney.Add(expectedReward);
            Assert.Equal(expectedMoney.Amount, _run.Money.Amount);
        }

        [Fact]
        public void AddMoney_IncreasesMoneyBySpecifiedAmount()
        {
            // Arrange
            var amountToAdd = new Money(250);
            var initialMoney = _run.Money;

            // Act
            _run.AddMoney(amountToAdd);

            // Assert
            var expectedMoney = initialMoney.Add(amountToAdd);
            Assert.Equal(expectedMoney.Amount, _run.Money.Amount);
        }

        [Fact]
        public void SpendMoney_DecreasesMoneyBySpecifiedAmount()
        {
            // Arrange
            var amountToSpend = new Money(150);
            var initialMoney = _run.Money;

            // Act
            _run.SpendMoney(amountToSpend);

            // Assert
            var expectedMoney = initialMoney.Subtract(amountToSpend);
            Assert.Equal(expectedMoney.Amount, _run.Money.Amount);
        }

        [Fact]
        public void AddJoker_AddsJokerToRun()
        {
            //Arrange
            var initialJokerCount = _run.Jokers.Count;
            Joker jokerToAdd = new Joker(JokerType.Joker);

            // Act
            _run.AddJoker(jokerToAdd);

            // Assert
            Assert.Equal(initialJokerCount + 1, _run.Jokers.Count);
            Assert.Contains(jokerToAdd, _run.Jokers);
        }

        [Fact]
        public void AddJoker_ReducesMoneyByJokerCost()
        {
            // Arrange
            var joker = new Joker(JokerType.Joker);
            var initialMoney = _run.Money;
            var expectedMoney = initialMoney.Subtract(joker.Cost());

            // Act
            _run.AddJoker(joker);

            // Assert
            Assert.Equal(expectedMoney.Amount, _run.Money.Amount);
        }

        [Fact]
        public void RemoveJoker_RemovesJokerFromRun()
        {
            //Arrange
            Joker jokerToRemove = new Joker(JokerType.Joker);
            _run.Jokers.Add(jokerToRemove);
            var initialJokerCount = _run.Jokers.Count;

            // Act
            _run.RemoveJoker(jokerToRemove);

            // Assert
            Assert.Equal(initialJokerCount - 1, _run.Jokers.Count);
            Assert.DoesNotContain(jokerToRemove, _run.Jokers);
        }


        [Fact]
        public void AddCardToDeck_AddsCardToDeck()
        {
            // Arrange
            var cardToAdd = new Card(Suit.Hearts, Rank.Ace);
            var initialDeckCount = _run.Deck.Cards.Count;

            // Act
            _run.AddCardToDeck(cardToAdd);

            // Assert
            Assert.Equal(initialDeckCount + 1, _run.Deck.Cards.Count);
            Assert.Contains(cardToAdd, _run.Deck.Cards);
        }

        [Fact]
        public void AddCardToDeck_ReducesMoneyByCardCost()
        {
            // Arrange
            var card = new Card(Suit.Spades, Rank.King);
            var initialMoney = _run.Money;
            var expectedMoney = initialMoney.Subtract(card.Cost());

            // Act
            _run.AddCardToDeck(card);

            // Assert
            Assert.Equal(expectedMoney.Amount, _run.Money.Amount);
        }
    }
}