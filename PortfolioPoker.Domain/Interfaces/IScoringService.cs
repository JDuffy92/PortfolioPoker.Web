using System.Collections.Generic;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.Domain.Interfaces
{
    public interface IScoringService
    {
        int ScoreCards(IEnumerable<Joker> jokers, HandEvaluationResult handEvaluation);

        ScoringServiceResult PreviewScoreCards(IEnumerable<Joker> jokers, HandEvaluationResult handEvaluation);
    }
}