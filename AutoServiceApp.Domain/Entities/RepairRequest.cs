using AutoServiceApp.Domain.Enums;
using System;

namespace AutoServiceApp.Domain.Entities
{
    public class RepairRequest
    {
        public int Id { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string ProblemDescription { get; set; }
        public string ClientName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public RequestStatus Status { get; set; } = RequestStatus.New;
    }
}