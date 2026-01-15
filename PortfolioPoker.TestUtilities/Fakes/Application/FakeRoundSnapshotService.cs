using PortfolioPoker.Application.Interfaces;
using PortfolioPoker.Domain.Models;

namespace PortfolioPoker.TestUtilities.Fakes
{
    public class FakeRoundSnapshotService : IRoundSnapshotService
    {
        public bool SaveCalled { get; private set; }
        public bool RestoreCalled { get; private set; }

        public bool CanUndo => false;

        public void Save(Round round)
        {
            SaveCalled = true;
        }

        public void RestoreLast(Round round)
        {
            RestoreCalled = true;
        }

        public void Clear() { }
    }
}
