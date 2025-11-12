using System;
using AutoServiceApp.Domain.Enums;

namespace Data.Interfaces
{
    public record RepairRequestsFilter
    {
        public static RepairRequestsFilter Empty => new();

        public DateOnly? StartDate { get; init; }
        public DateOnly? EndDate { get; init; }

        public string? SearchText { get; init; }
        public RequestStatus? Status { get; init; }
    }
}
