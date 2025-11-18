using AutoServiceApp.Domain.Enums;

namespace AutoServiceApp.Domain.Statistics
{
    public record StatusStatisticItem
    {
        public required RequestStatus Status { get; init; }
        public required int Count { get; init; }
    }
}
