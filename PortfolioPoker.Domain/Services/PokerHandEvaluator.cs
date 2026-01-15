using System.Collections.Generic;
using System.Linq;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.Domain.Services
{
    public class PokerHandEvaluator : IHandEvaluator
    {
        public HandEvaluationResult Evaluate(IEnumerable<Card> cards)
        {
            var cardList = cards.ToList();

            if (IsRoyalFlush(cardList))
                return new HandEvaluationResult(HandType.RoyalFlush, cardList);

            if (IsStraightFlush(cardList))
                return new HandEvaluationResult(HandType.StraightFlush, cardList);

            if (IsFourOfAKind(cardList))
                return new HandEvaluationResult(HandType.FourOfAKind, cardList);

            if (IsFullHouse(cardList))
                return new HandEvaluationResult(HandType.FullHouse, cardList);

            if (IsFlush(cardList))
                return new HandEvaluationResult(HandType.Flush, cardList);

            if (IsStraight(cardList))
                return new HandEvaluationResult(HandType.Straight, cardList);

            if (IsThreeOfAKind(cardList))
                return new HandEvaluationResult(HandType.ThreeOfAKind, cardList);

            if (IsTwoPair(cardList))
                return new HandEvaluationResult(HandType.TwoPair, cardList);

            if (IsPair(cardList))
                return new HandEvaluationResult(HandType.Pair, cardList);

            return new HandEvaluationResult(HandType.HighCard, cardList);
        }

        private bool HasCardsOfSameRank(List<Card> cards, int count)
        {
            var rankGroups = cards.GroupBy(card => card.Rank);
            return rankGroups.Any(group => group.Count() == count);
        }

        private bool HasCardsInSequence(List<Card> cards, int sequenceLength)
        {
            var distinctRanks = cards.Select(card => card.Rank).Distinct().OrderBy(rank => rank).ToList();
            int consecutiveCount = 1;

            for (int i = 1; i < distinctRanks.Count; i++)
            {
                if ((int)distinctRanks[i] == (int)distinctRanks[i - 1] + 1)
                {
                    consecutiveCount++;
                    if (consecutiveCount >= sequenceLength)
                        return true;
                }
                else
                {
                    consecutiveCount = 1;
                }
            }

            return false;
        }

        private bool HasCardsOfSameSuit(List<Card> cards, int count)
        {
            var suitGroups = cards.GroupBy(card => card.Suit);
            return suitGroups.Any(group => group.Count() >= count);
        }


        private bool IsRoyalFlush(List<Card> cards)
        {
            var requiredRanks = new HashSet<Rank>
            {
                Rank.Ten,
                Rank.Jack,
                Rank.Queen,
                Rank.King,
                Rank.Ace
            };

            var suitGroups = cards.GroupBy(card => card.Suit);
            foreach (var group in suitGroups)
            {
                var groupRanks = new HashSet<Rank>(group.Select(card => card.Rank));
                if (requiredRanks.IsSubsetOf(groupRanks))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsStraightFlush(List<Card> cards)
        {
            var suitGroups = cards.GroupBy(card => card.Suit);
            foreach (var group in suitGroups)
            {
                if (HasCardsInSequence(group.ToList(), 5))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsFourOfAKind(List<Card> cards)
        {
            var rankGroups = cards.GroupBy(card => card.Rank);
            return rankGroups.Any(group => group.Count() == 4);
        }

        private bool IsFullHouse(List<Card> cards)
        {
            var rankGroups = cards.GroupBy(card => card.Rank);
            bool hasThreeOfAKind = rankGroups.Any(group => group.Count() == 3);
            bool hasPair = rankGroups.Any(group => group.Count() == 2);
            return hasThreeOfAKind && hasPair;
        }

        private bool IsFlush(List<Card> cards)
        {
            return HasCardsOfSameSuit(cards, 5);
        }

        private bool IsStraight(List<Card> cards)
        {
            return HasCardsInSequence(cards, 5);
        }

        private bool IsThreeOfAKind(List<Card> cards)
        {
            return HasCardsOfSameRank(cards, 3);
        }

        private bool IsTwoPair(List<Card> cards)
        {
            var rankGroups = cards.GroupBy(card => card.Rank);
            return rankGroups.Count(group => group.Count() == 2) >= 2;
        }

        private bool IsPair(List<Card> cards)
        {
            return HasCardsOfSameRank(cards, 2);
        }
    }
}