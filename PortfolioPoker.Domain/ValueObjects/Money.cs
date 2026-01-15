using System.Collections.Generic;
using System.Linq;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;

namespace PortfolioPoker.Domain.ValueObjects
{
    public class Money
    {
        public int Amount { get; }

        public Money(int amount)
        {
            Amount = amount;
        }

        public Money Add(Money other)
        {
            return new Money(this.Amount + other.Amount);
        }

        public Money Subtract(Money other)
        {
            //Ensure money does not go negative
            if (this.Amount - other.Amount < 0)
            {
                return new Money(0);
            }

            return new Money(this.Amount - other.Amount);
        }

        public bool IsGreaterThanOrEqual(Money other)
        {
            return this.Amount >= other.Amount;
        }
    }
}
