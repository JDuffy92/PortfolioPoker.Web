using System.Collections.Generic;
using Xunit;
using PortfolioPoker.Domain;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;

namespace PortfolioPoker.Tests.EditMode.Domain.Models
{
    
    public class JokerTests
    {
        private Joker _joker;

        
        public JokerTests()
        {
            _joker = new Joker(JokerType.Joker);
        }
    }
}