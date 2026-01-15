public enum RoundPhase
{
    StartPhase,  // Initial setup phase
    DrawPhase,    // Initial or replacement draw for the current hand
    SelectPhase,  // Player selects cards to play or discard
    PlayPhase,    // Player plays selected cards
    DiscardPhase, // Player discards selected cards
    RoundEnd      // Round finished due to failure or success
}
