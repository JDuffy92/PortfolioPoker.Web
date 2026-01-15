using System;
using System.Collections.Generic;
using System.Linq;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.Domain.Models
{
    /**
    * Descriptor for a round within a run, containing metadata such as round number and target score.
    * Used for configuring rounds in a run.
    */
    public sealed class RoundDescriptor
    {
        public int RoundNumber { get; }
        public int PointTarget { get; }

        public RoundDescriptor(int roundNumber, int pointTarget)
        {
            RoundNumber = roundNumber;
            PointTarget = pointTarget;
        }
    }
}