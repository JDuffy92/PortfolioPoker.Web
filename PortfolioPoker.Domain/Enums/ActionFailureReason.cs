namespace PortfolioPoker.Domain.Enums
{
    public enum ActionFailureReason
    {
        None,
        ActionNotAllowed,

        // Shared
        InvalidCards,
        DeckEmpty,

        // Play hand
        NoHandsAvailable,

        // Discard
        NoDiscardsAvailable,

        // Draw
        NotEnoughCardsInDeck
    }
}
