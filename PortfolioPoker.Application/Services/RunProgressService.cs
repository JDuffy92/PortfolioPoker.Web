using System.Collections.Generic;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.Enums;
using System;
using PortfolioPoker.Application.Interfaces;

namespace PortfolioPoker.Application.Services
{
    public class RunProgressService : IRunProgressService
    {
        private readonly IRoundSetupService _roundSetupService;
        private readonly IRoundRewardService _rewardService;

        public RunProgressService(
            IRoundSetupService roundSetupService,
            IRoundRewardService rewardService)
        {
            _roundSetupService = roundSetupService;
            _rewardService = rewardService;
        }

        public bool CanStartNextRound(Run run)
        {
            return run.HasNextRound;
        }

        public Round StartNextRound(Run run)
        {
            if (!run.HasNextRound)
                throw new InvalidOperationException("Run has no more rounds.");

            var rng = run.Config.CreateRandom(run.CurrentRoundIndex);

            return _roundSetupService.CreateRound(run, rng);
        }

        public void CompleteRound(
            Run run,
            Round round)
        {
            if (round.Phase != RoundPhase.RoundEnd)
                throw new InvalidOperationException("Round is not complete");

            //Log our status to the console for debugging
            Console.WriteLine($"Completing round with status: {round.Status}");

            if (round.Status == RoundStatus.Success)
            {
                //Log to the console for debugging
                Console.WriteLine("Calculating reward for successful round...");

                var reward = _rewardService.CalculateReward(run, round);

                //Log the reward to the console for debugging
                Console.WriteLine($"Reward calculated: {reward.Amount} money.");

                run.AddMoney(reward);

                //Log the new total money to the console for debugging
                Console.WriteLine($"New total money: {run.Money.Amount}.");
            }

            run.CompleteCurrentRound();
        }

        public bool IsRunComplete(Run run)
        {
            return !run.HasNextRound;
        }
    }
}
