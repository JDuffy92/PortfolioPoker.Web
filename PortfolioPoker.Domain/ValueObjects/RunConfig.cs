using System;
using System.Collections.Generic;
using System.Linq;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;

namespace PortfolioPoker.Domain.ValueObjects
{
    /*
    * Configuration settings for a Run.
    */
    [Serializable]
    public sealed class RunConfig: IEquatable<RunConfig>
    {
        public int Seed { get; }
        public DeckType DeckType { get; }
        public Money StartingMoney { get; }
        
        public int TotalRounds; // Currently fixed to 5 rounds
        public int ScoreIncrementPerRound; // Score target increases by this amount each round

        public RunConfig(
            int seed,
            DeckType deckType,
            Money startingMoney,
            int totalRounds,
            int scoreIncrementPerRound
        )
        {
            Seed = seed;
            DeckType = deckType;
            StartingMoney = startingMoney;
            TotalRounds = totalRounds;
            ScoreIncrementPerRound = scoreIncrementPerRound;
        }

        #region  Seed Randomness
        public Random CreateRandom(int modifier)
        {
            return new Random(Seed + modifier);
        }
        #endregion

        #region Value Equality
#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
        public bool Equals(RunConfig other)
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
        {
            if (other is null) return false;

            return Seed == other.Seed
                && DeckType == other.DeckType
                && StartingMoney.Equals(other.StartingMoney)
                && TotalRounds == other.TotalRounds
                && ScoreIncrementPerRound == other.ScoreIncrementPerRound;
        }

#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        public override bool Equals(object obj)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        {
#pragma warning disable CS8604 // Possible null reference argument.
            return Equals(obj as RunConfig);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                Seed,
                DeckType,
                StartingMoney,
                TotalRounds,
                ScoreIncrementPerRound
            );
        }

        #endregion
    }
}
