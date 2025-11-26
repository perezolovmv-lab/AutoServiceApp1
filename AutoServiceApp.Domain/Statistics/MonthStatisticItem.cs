using System;
using System.Globalization;

namespace AutoServiceApp.Domain.Statistics
{
    public record MonthStatisticItem
    {
        public required int Year { get; init; }
        public required int Month { get; init; }
        public required int Count { get; init; }

        public string GetMonthName()
        {
            var date = new DateTime(Year, Month, 1);
            return date.ToString("MMM yyyy", CultureInfo.CurrentUICulture);
        }
    }
}
