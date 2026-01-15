using System;
using System.Collections.Generic;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.Application.Interfaces
{
    public interface IRoundSetupService
    {
        Round CreateRound(
            Run run,
            Random rng
        );
    }
}