using System.Collections.Generic;
using PortfolioPoker.Domain;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.TestUtilities.Fakes
{
    public class FakeScoringService : IScoringService
    {
        private readonly int _pointsToReturn;

        public FakeScoringService(int pointsToReturn)
        {
            _pointsToReturn = pointsToReturn;
        }

        public int ScoreCards(IEnumerable<Joker> jokers, HandEvaluationResult handEvaluation)
        {
            return _pointsToReturn;
        }

        public ScoringServiceResult PreviewScoreCards(IEnumerable<Joker> jokers, HandEvaluationResult handEvaluation)
        {
            int handTypeChips = _pointsToReturn;
            int handTypeMultiplier = 1;

            return new ScoringServiceResult(handTypeChips, handTypeMultiplier, 0, _pointsToReturn);
        }
    }
}