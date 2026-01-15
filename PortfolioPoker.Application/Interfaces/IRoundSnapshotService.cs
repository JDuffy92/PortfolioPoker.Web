using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;

namespace PortfolioPoker.Application.Interfaces{
    public interface IRoundSnapshotService
    {
        void Save(Round round);
        bool CanUndo { get; }
        void RestoreLast(Round round);
        void Clear();
    }
}