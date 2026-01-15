using System.Collections.Generic;
using System.Linq;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;

namespace PortfolioPoker.Domain.ValueObjects
{
    public class ScoringServiceResult
    {
        public int HandTypeChips { get; }
        public int HandTypeMultiplier { get; }

        public int JokerChips { get; } // Placeholder for future joker chip calculations

        public int TotalScore { get; }

        public ScoringServiceResult(
            int handTypeChips,
            int handTypeMultiplier,
            int jokerChips,
            int totalScore)
        {
            HandTypeChips = handTypeChips;
            HandTypeMultiplier = handTypeMultiplier;
            JokerChips = jokerChips;
            TotalScore = totalScore;
        }
    }
}
