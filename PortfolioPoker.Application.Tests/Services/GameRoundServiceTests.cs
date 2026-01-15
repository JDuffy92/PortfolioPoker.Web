using Xunit;
using PortfolioPoker.Application.Services;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.Services;
using PortfolioPoker.Domain.Validation;
using PortfolioPoker.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;
using PortfolioPoker.TestUtilities.Fakes;
using PortfolioPoker.Domain.ValueObjects;

namespace PortfolioPoker.Application.Tests.Services
{
    
    public class GameRoundServiceTests
    {
        private FakeHandEvaluator _fakeHandEvaluator;
        private FakeScoringService _fakeScoringService;

        private FakeRoundActionValidator _fakeRoundActionValidator;
        private FakeRoundStateEvaluator _fakeRoundStateEvaluator;
        private FakeRoundPhaseTransitionService _fakeRoundPhaseTransitionService;
        private FakeRoundSnapshotService _fakeRoundSnapshotService;

        private GameRoundService _service;
        private Round _round;

        public GameRoundServiceTests()
        {
            _fakeHandEvaluator = new FakeHandEvaluator(HandType.Pair);
            _fakeScoringService = new FakeScoringService(10);

            _fakeRoundActionValidator = new FakeRoundActionValidator(ValidationResult.Success());
            _fakeRoundStateEvaluator = new FakeRoundStateEvaluator(RoundStatus.Active);
            _fakeRoundPhaseTransitionService = new FakeRoundPhaseTransitionService(true);
            _fakeRoundSnapshotService = new FakeRoundSnapshotService();

            _service = new GameRoundService(
                handEvaluator: _fakeHandEvaluator,
                roundActionValidator: _fakeRoundActionValidator,
                roundStateEvaluator: _fakeRoundStateEvaluator,
                roundPhaseTransitionService: _fakeRoundPhaseTransitionService,
                roundSnapshotService: _fakeRoundSnapshotService,
                scoringService: _fakeScoringService
            );

            var deck = Deck.GenerateDeckForDeckType(DeckType.RedDeck);

            _round = new Round(
                handEvaluator: _fakeHandEvaluator,
                scoringService: _fakeScoringService,
                pointTarget: 100,
                handsAvailable: 5,
                discardsAvailable: 5,
                jokers: new List<Joker>(),
                deck: deck
            );
        }

        [Fact]
        public void DrawCards_DrawsCardsIntoHand()
        {
            // Act
            var drawnCards = _service.DrawCards(_round, 3);

            // Assert
            Assert.Equal(3, _round.Hand.Cards.Count);
            Assert.Equal(49, _round.Deck.Cards.Count);
        }

        [Fact]
        public void DrawCards_ResultsInSelectPhase()
        {
            // Act
            var drawnCards = _service.DrawCards(_round, 3);

            // Assert
            Assert.Equal(RoundPhase.SelectPhase, _round.Phase);
        }

        [Fact]
        public void PlayHand_PlaysCardsFromHandAndUpdatesPoints()
        {
            // Arrange
            _round.DrawCards(2);

            // -- Make sure we're in PlayPhase --
            _service.UpdatePhase(_round, RoundPhase.SelectPhase);
            _service.UpdatePhase(_round, RoundPhase.PlayPhase);

            var cardsToPlay = _round.Hand.Cards.ToList();

            // Act
            var result = _service.PlayHand(_round, cardsToPlay);

            // Assert
            Assert.True(result.Success);
            Assert.Empty(_round.Hand.Cards);
            Assert.Equal(2, _round.DiscardPile.Cards.Count);
            Assert.True(_round.CurrentPoints > 0);
        }

        [Fact]
        public void PlayHand_ResultsInDrawPhaseIfScoreLimitIsNotReached()
        {
            // Arrange
            _round.DrawCards(2);

            // Act
            var playedHand = _service.PlayHand(_round, _round.Hand.Cards.ToList());

            // Assert
            Assert.Equal(RoundPhase.DrawPhase, _round.Phase);
        }

        [Fact]
        public void PlayHand_ResultsInRoundEndPhaseIfScoreLimitIsReached()
        {
            // Arrange
            // -- Recreate service with new evaluator
            _service = new GameRoundService(
                handEvaluator: _fakeHandEvaluator,
                roundActionValidator: _fakeRoundActionValidator,
                roundStateEvaluator: new FakeRoundStateEvaluator(RoundStatus.Success),
                roundPhaseTransitionService: _fakeRoundPhaseTransitionService,
                roundSnapshotService: _fakeRoundSnapshotService,
                scoringService: _fakeScoringService
            );

            _round.DrawCards(2);
            _round.CurrentPoints = 100; // Simulate reaching point target

            // Act
            var playedHand = _service.PlayHand(_round, _round.Hand.Cards.ToList());

            // Assert
            Assert.Equal(RoundPhase.RoundEnd, _round.Phase);
        }


        [Fact]
        public void PreviewHandEvaluation_ReturnsEvaluationWithoutMutatingRound()
        {
            // Arrange
            _round.DrawCards(3);
            var cards = _round.Hand.Cards.ToList();

            var pointsBefore = _round.CurrentPoints;
            var handSizeBefore = _round.Hand.Cards.Count;

            // Act
            var result = _service.PreviewHandEvaluation(cards);

            // Assert
            Assert.Equal(HandType.Pair, result.HandType);
            Assert.Equal(handSizeBefore, _round.Hand.Cards.Count);
            Assert.Equal(pointsBefore, _round.CurrentPoints);
            Assert.Empty(_round.DiscardPile.Cards);
        }

        [Fact]
        public void PreviewScoreCards_ReturnsScoringServiceResultWithoutMutatingRound()
        {
            // Arrange
            _round.DrawCards(3);
            var cards = _round.Hand.Cards.ToList();

            var pointsBefore = _round.CurrentPoints;
            var handSizeBefore = _round.Hand.Cards.Count;

            // Act
            var evaluationResult = _service.PreviewHandEvaluation(cards);

            var result = _service.PreviewScoreCards(_round.Jokers, evaluationResult);

            // Assert
            Assert.IsType<ScoringServiceResult>(result);
        }


        [Fact]
        public void PlayHand_ReturnsFailedResultWhenNoHandsAvailable()
        {
            // Recreate service with a validator that means no hands are available
            _fakeRoundActionValidator = new FakeRoundActionValidator(ValidationResult.Fail("No hands remaining"));

            //Recreate service with new validator
            _service = new GameRoundService(
                handEvaluator: _fakeHandEvaluator,
                roundActionValidator: _fakeRoundActionValidator,
                roundStateEvaluator: _fakeRoundStateEvaluator,
                roundPhaseTransitionService: _fakeRoundPhaseTransitionService,
                roundSnapshotService: _fakeRoundSnapshotService,
                scoringService: _fakeScoringService
            );

            // Arrange
            _round.DrawCards(2);
            var cardsToPlay = _round.Hand.Cards.ToList();

            // Play all available hands
            for (int i = 0; i < _round.HandsAvailable; i++)
            {
                _service.PlayHand(_round, cardsToPlay);
                _round.DrawCards(2); // Draw again for next play
                cardsToPlay = _round.Hand.Cards.ToList();
            }

            // Act & Assert
            var result = _service.PlayHand(_round, cardsToPlay);
            Assert.False(result.Success);
        }

        [Fact]
        public void DiscardCards_ReturnsFailedWhenNoDiscardsAvailable()
        {
            // Recreate service with a validator that means no hands are available
            _fakeRoundActionValidator = new FakeRoundActionValidator(ValidationResult.Fail("No hands remaining"));

            //Recreate service with new validator
            _service = new GameRoundService(
                handEvaluator: _fakeHandEvaluator,
                roundActionValidator: _fakeRoundActionValidator,
                roundStateEvaluator: _fakeRoundStateEvaluator,
                roundPhaseTransitionService: _fakeRoundPhaseTransitionService,
                roundSnapshotService: _fakeRoundSnapshotService,
                scoringService: _fakeScoringService
            );
            
            // Arrange
            _round.DrawCards(3);
            var cardsToDiscard = _round.Hand.Cards.ToList();

            // Use all available discards
            for (int i = 0; i < _round.DiscardsAvailable; i++)
            {
                _round.DiscardCards(cardsToDiscard);
                _round.DrawCards(3); // Draw again for next discard
                cardsToDiscard = _round.Hand.Cards.ToList();
            }

            // Act & Assert
            var result = _service.DiscardCards(_round, cardsToDiscard);
            Assert.False(result.Success);
        }
    }
}
