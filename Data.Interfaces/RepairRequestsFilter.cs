using AutoServiceApp.Domain.Enums;

namespace Data.Interfaces
{
    public class RepairRequestsFilter
    {
        public static RepairRequestsFilter Empty { get; set; }
        public string? SearchText { get; set; }

        // ÄÀÒÛ Â ÂÈÄÅ DateOnly? – ÊÀÊ Â StatisticsWindow
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public RequestStatus? Status { get; set; }
    }
}
