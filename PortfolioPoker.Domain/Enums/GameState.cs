namespace PortfolioPoker.Domain.Enums
{
    public enum GameState
    {
        MainMenu,       // Set up round, choose options
        RoundActive,    // A round is ongoing
        RoundEndMenu,   // Round ended (success/failure), show summary, rewards
        Settings,       // Options, save/load, etc.
        Exit
    }
}
