using System.Collections.Generic;
using System.Linq;
using PortfolioPoker.Domain.Models;

namespace PortfolioPoker.Application.DTOs
{
    public class RoundSnapshot
    {
        public int HandsPlayed { get; }
        public int DiscardsMade { get; }
        public int CurrentPoints { get; }

        public List<Card> DeckCards { get; }
        public List<Card> HandCards { get; }
        public List<Card> DiscardPileCards { get; }

        public RoundSnapshot(Round round)
        {
            HandsPlayed = round.HandsPlayed;
            DiscardsMade = round.DiscardsMade;
            CurrentPoints = round.CurrentPoints;

            DeckCards = round.Deck.Cards.ToList();
            HandCards = round.Hand.Cards.ToList();
            DiscardPileCards = round.DiscardPile.Cards.ToList();
        }

        public void Restore(Round round)
        {
            round.Deck.Cards.Clear();
            round.Deck.Cards.AddRange(DeckCards);

            round.Hand.Cards.Clear();
            round.Hand.Cards.AddRange(HandCards);

            round.DiscardPile.Cards.Clear();
            round.DiscardPile.Cards.AddRange(DiscardPileCards);

            round.GetType()
                 .GetProperty(nameof(round.HandsPlayed))!
                 .SetValue(round, HandsPlayed);

            round.GetType()
                 .GetProperty(nameof(round.DiscardsMade))!
                 .SetValue(round, DiscardsMade);

            round.GetType()
                 .GetProperty(nameof(round.CurrentPoints))!
                 .SetValue(round, CurrentPoints);
        }
    }
}
