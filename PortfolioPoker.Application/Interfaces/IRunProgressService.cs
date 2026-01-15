using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;

namespace PortfolioPoker.Application.Interfaces
{
    public interface IRunProgressService
    {
        bool CanStartNextRound(Run run);

        Round StartNextRound(Run run);

        void CompleteRound(
             Run run,
             Round round);
    }
}