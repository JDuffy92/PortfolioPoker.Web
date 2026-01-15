using System.Collections.Generic;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.Domain.Models
{
    public class Joker
    {
        public JokerType Type { get; private set; }

        public Joker(JokerType type)
        {
            Type = type;
        }

        public Money Cost()
        {
            switch (Type)
            {
                case JokerType.Joker:
                    return new Money(3);
                case JokerType.LustyJoker:
                    return new Money(5);
                default:
                    return new Money(0);
            }
        }
    }
}