namespace AutoServiceApp.Domain.Statistics
{
    public record BrandStatisticItem
    {
        public required string BrandName { get; init; }
        public required int Count { get; init; }
    }
}
