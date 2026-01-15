using System.Collections.Generic;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.Domain.Services
{
    public class ScoringService: IScoringService
    {
        public int Multiplier;
        public int Chips;

        public int ScoreCards(IEnumerable<Joker> jokers, HandEvaluationResult handEvaluation)
        {
            //First determine base points for the hand evaluation
            Multiplier = CalculateMultiplierForHandEvaluation(handEvaluation);
            Chips = CalculateChipsForHandEvaluation(handEvaluation);

            //Next consider any jokers that may modify scoring

            //Finally add the points of the cards played (Individually)

            return Chips * Multiplier;
        }

        public ScoringServiceResult PreviewScoreCards(IEnumerable<Joker> jokers, HandEvaluationResult handEvaluation)
        {
            int handTypeChips = CalculateChipsForHandEvaluation(handEvaluation);
            int handTypeMultiplier = CalculateMultiplierForHandEvaluation(handEvaluation);

            return new ScoringServiceResult(handTypeChips, handTypeMultiplier, 0, ScoreCards(jokers, handEvaluation));
        }

        public int CalculateMultiplierForHandEvaluation(HandEvaluationResult handEvaluation)
        {
            int points = handEvaluation.HandType switch
            {
                HandType.HighCard => 1,
                HandType.Pair => 2,
                HandType.TwoPair => 2,
                HandType.ThreeOfAKind => 3,
                HandType.Straight => 4,
                HandType.Flush => 4,
                HandType.FullHouse => 4,
                HandType.FourOfAKind => 7,
                HandType.StraightFlush => 8,
                HandType.RoyalFlush => 8,
                _ => 1
            };

            return points;
        }

        public int CalculateChipsForHandEvaluation(HandEvaluationResult handEvaluation)
        {
            int points = handEvaluation.HandType switch
            {
                HandType.HighCard => 5,
                HandType.Pair => 10,
                HandType.TwoPair => 20,
                HandType.ThreeOfAKind => 30,
                HandType.Straight => 30,
                HandType.Flush => 40,
                HandType.FullHouse => 40,
                HandType.FourOfAKind => 60,
                HandType.StraightFlush => 100,
                HandType.RoyalFlush => 100,
                _ => 1
            };

            return points;
        }
    }
}