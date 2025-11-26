using Data.Interfaces;
using AutoServiceApp.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Data.InMemory;
public class RepairRequestRepository : IRepairRequestRepository
{
 private List<RepairRequest> _list=new();
 private int _id=1;
 public int Add(RepairRequest r){ r.Id=_id++; _list.Add(r); return r.Id;}

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public List<RepairRequest> GetAll()=>_list.ToList();

    public List<RepairRequest> GetAll(RepairRequestsFilter filter)
    {
        throw new NotImplementedException();
    }

    public RepairRequest GetById(int id)
    {
        throw new NotImplementedException();
    }

    public bool Update(RepairRequest request)
    {
        throw new NotImplementedException();
    }
}
