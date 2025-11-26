using System.Collections.Generic;
using System.Linq;
using AutoServiceApp.Domain.Statistics;
using Data.Interfaces;

namespace AutoServiceApp.Services
{
    public class StatisticsService
    {
        private readonly IRepairRequestRepository _repo;
        public StatisticsService(IRepairRequestRepository repo) => _repo = repo;

        public List<StatusStatisticItem> GetRequestsByStatus(RepairRequestsFilter filter)
        {
            var items = _repo.GetAll(filter ?? RepairRequestsFilter.Empty);
            return items
                .GroupBy(x => x.Status)
                .Select(g => new StatusStatisticItem { Status = g.Key, Count = g.Count() })
                .OrderBy(s => s.Status)
                .ToList();
        }

        public List<MonthStatisticItem> GetRequestsByMonth(RepairRequestsFilter filter)
        {
            var items = _repo.GetAll(filter ?? RepairRequestsFilter.Empty);
            return items
                .GroupBy(x => new { x.CreatedDate.Year, x.CreatedDate.Month })
                .Select(g => new MonthStatisticItem { Year = g.Key.Year, Month = g.Key.Month, Count = g.Count() })
                .OrderBy(m => m.Year).ThenBy(m => m.Month)
                .ToList();
        }

        public List<BrandStatisticItem> GetRequestsByBrand(RepairRequestsFilter filter)
        {
            var items = _repo.GetAll(filter ?? RepairRequestsFilter.Empty);
            return items
                .Where(x => !string.IsNullOrWhiteSpace(x.CarBrand))
                .GroupBy(x => x.CarBrand!.Trim())
                .Select(g => new BrandStatisticItem { BrandName = g.Key, Count = g.Count() })
                .OrderByDescending(b => b.Count)
                .ToList();
        }
    }
}
