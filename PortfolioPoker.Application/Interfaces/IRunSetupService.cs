using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;

namespace PortfolioPoker.Application.Interfaces{
    public interface IRunSetupService
    {
        Run CreateRun(
            DeckType deckType,
            int? seedOverride = null
        );
    }
}