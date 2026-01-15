using System.Collections.Generic;
using PortfolioPoker.Application.Interfaces;
using PortfolioPoker.Application.DTOs;
using PortfolioPoker.Domain.Models;

namespace PortfolioPoker.Application.Services
{
    public class RoundSnapshotService : IRoundSnapshotService
    {
        private readonly Stack<RoundSnapshot> _history = new();

        public bool CanUndo => _history.Count > 0;

        public void Save(Round round)
        {
            //TODO: Check if we need to clone anything inside RoundSnapshot (should we clone the round itself?)
            _history.Push(new RoundSnapshot(round));
        }

        public void RestoreLast(Round round)
        {
            if (!CanUndo)
                return;

            var snapshot = _history.Pop();
            snapshot.Restore(round);
        }

        public void Clear()
        {
            _history.Clear();
        }
    }
}
