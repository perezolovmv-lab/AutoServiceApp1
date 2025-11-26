using System.Collections.Generic;
using AutoServiceApp.Domain.Entities;

namespace Data.Interfaces
{
    public interface IRepairRequestRepository
    {
        int Add(RepairRequest request);
        bool Update(RepairRequest request);
        bool Delete(int id);
        RepairRequest? GetById(int id);
        List<RepairRequest> GetAll(RepairRequestsFilter filter);
        object GetAll(object value);
    }
}
