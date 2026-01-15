
using System;
using System.Collections.Generic;
using System.Linq;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.Domain.Models
{
    public class Run
    {
        private IRoundRewardService _roundRewardService;

        public RunConfig Config { get; set; }

        public List<Joker> Jokers { get; set; }
        public Deck Deck { get; set; }
        public Money Money { get; set; }

        public int Hands { get; set; }
        public int Discards { get; set; }
        public int CurrentRoundIndex { get; private set; }
        private readonly List<RoundDescriptor> _rounds;


        public Run(
            IRoundRewardService roundRewardService,
            RunConfig config,
            IEnumerable<Joker> jokers,
            Deck startingDeck,
            Money money,
            IEnumerable<RoundDescriptor> rounds
        )
        {
            _roundRewardService = roundRewardService;
            Config = config;
            Jokers = jokers.ToList();
            Deck = startingDeck;
            Money = money;
            Hands = Deck.GetHandsForDeckType(config.DeckType);
            Discards = Deck.GetDiscardsForDeckType(config.DeckType);

            CurrentRoundIndex = 0;
            _rounds = rounds.ToList();
        }


        #region Money Management
        public void CalculateAndApplyRewardsForRound(Round round)
        {
            var reward = _roundRewardService.CalculateReward(this, round);
            AddMoney(reward);
        }

        public void AddMoney(Money amount)
        {
            Money = Money.Add(amount);
        }

        public void SpendMoney(Money amount)
        {
            if (!CanSpendMoney(amount))
                throw new InvalidOperationException("Not enough money");

            Money = Money.Subtract(amount);
        }

        public bool CanSpendMoney(Money amount)
        {
            return Money.IsGreaterThanOrEqual(amount);
        }
        #endregion


        #region Joker Manipulation
        public void AddJoker(Joker joker)
        {
            SpendMoney(joker.Cost());
            Jokers.Add(joker);
        }

        public void RemoveJoker(Joker joker)
        {
            SpendMoney(joker.Cost());
            Jokers.Remove(joker);
        }
        #endregion


        #region Deck Manipulation
        public void AddCardToDeck(Card card)
        {
            SpendMoney(card.Cost());
            Deck.Cards.Add(card);
        }

        public void RemoveCardFromDeck(Card card)
        {
            SpendMoney(card.Cost());
            Deck.Cards.Remove(card);
        }

        #endregion


        #region  Round Management
        public bool HasNextRound => CurrentRoundIndex < _rounds.Count;

        public RoundDescriptor GetCurrentRoundDescriptor()
        {
            if (CurrentRoundIndex < 0 || CurrentRoundIndex >= _rounds.Count)
                throw new InvalidOperationException("No current round descriptor available.");

            return _rounds[CurrentRoundIndex];
        }

        public bool CompleteCurrentRound()
        {
            if (CurrentRoundIndex >= _rounds.Count)
                return false;

            CurrentRoundIndex++;
            return true;
        }

        public int TotalRounds => _rounds.Count;
        #endregion
    }
}