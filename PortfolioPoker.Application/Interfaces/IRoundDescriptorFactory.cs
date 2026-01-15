using System.Collections.Generic;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.Application.Interfaces
{
    public interface IRoundDescriptorFactory
    {
        IReadOnlyList<RoundDescriptor> CreateRounds(RunConfig config);
    }
}