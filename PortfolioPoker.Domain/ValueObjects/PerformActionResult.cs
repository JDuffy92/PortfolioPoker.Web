using System.Collections.Generic;
using System.Linq;
using PortfolioPoker.Domain.Enums;
using PortfolioPoker.Domain.Models;
using PortfolioPoker.Domain.Interfaces;
using System;

namespace PortfolioPoker.Domain.ValueObjects
{
    public class PerformActionResult<T>
    {
        public bool Success { get; }
        #nullable enable
        public string? FailureReason { get; }
        public T? Data { get; }

        public IReadOnlyList<IGameEvent> Events { get; }

        private PerformActionResult(
            bool success,
            T? data,
            string? failureReason,
            IReadOnlyList<IGameEvent> events)
        {
            Success = success;
            Data = data;
            FailureReason = failureReason;
            Events = events;
        }

        public static PerformActionResult<T> Fail(string reason)
            => new(false, default, reason, Array.Empty<IGameEvent>());

        public static PerformActionResult<T> Ok(T data, IEnumerable<IGameEvent> events)
            => new(true, data, null, events.ToList());
    }
}


