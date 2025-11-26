using System;
using System.Collections.Generic;
using System.Linq;
using AutoServiceApp.Domain.Entities;
using AutoServiceApp.Domain.Enums;
using Data.Interfaces;

namespace Data.InMemory.Repositories
{
    public class RepairRequestRepository : IRepairRequestRepository
    {
        private readonly List<RepairRequest> _requests;
        private int _nextId = 1;

        public RepairRequestRepository()
        {
            _requests = new List<RepairRequest>();
            InitializeTestData();
            SeedRandomRequests(20);
        }

        private void InitializeTestData()
        {
            var now = DateTime.Now;

            var testRequest1 = new RepairRequest
            {
                CarBrand = "Toyota",
                CarModel = "Camry",
                ProblemDescription = "Замена масла и фильтров",
                ClientName = "Иванов Иван Иванович",
                PhoneNumber = "+7 (999) 123-45-67",
                Status = RequestStatus.Completed,
                CreatedDate = now.AddDays(-60)
            };
            Add(testRequest1);

            var testRequest2 = new RepairRequest
            {
                CarBrand = "Honda",
                CarModel = "Civic",
                ProblemDescription = "Диагностика двигателя",
                ClientName = "Петров Петр Петрович",
                PhoneNumber = "+7 (999) 987-65-43",
                Status = RequestStatus.InProgress,
                CreatedDate = now.AddDays(-30)
            };
            Add(testRequest2);

            var testRequest3 = new RepairRequest
            {
                CarBrand = "Lada",
                CarModel = "Vesta",
                ProblemDescription = "Замена тормозных колодок",
                ClientName = "Сидорова Анна Ивановна",
                PhoneNumber = "+7 (999) 555-35-35",
                Status = RequestStatus.New,
                CreatedDate = now.AddDays(-10)
            };
            Add(testRequest3);
        }

        public int Add(RepairRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            request.Id = _nextId++;

            if (request.CreatedDate == default)
                request.CreatedDate = DateTime.Now;

            if (request.Status == default(RequestStatus))
                request.Status = RequestStatus.New;

            _requests.Add(request);
            return request.Id;
        }

        public RepairRequest GetById(int id)
        {
            return _requests.FirstOrDefault(r => r.Id == id);
        }

        

        public bool Update(RepairRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var existing = GetById(request.Id);
            if (existing == null)
                return false;

            existing.CarBrand = request.CarBrand;
            existing.CarModel = request.CarModel;
            existing.ProblemDescription = request.ProblemDescription;
            existing.ClientName = request.ClientName;
            existing.PhoneNumber = request.PhoneNumber;
            existing.Status = request.Status;

            return true;
        }

        public bool Delete(int id)
        {
            var request = GetById(id);
            return request != null && _requests.Remove(request);
        }


        private void SeedRandomRequests(int count)
        {
            var brands = new[] { "Toyota", "BMW", "Audi", "Ford", "Kia", "Hyundai", "Volkswagen", "Nissan" };
            var models = new[] { "Corolla", "X5", "A4", "Focus", "Rio", "Elantra", "Golf", "Qashqai" };
            var names = new[] { "Иван Петров", "Сергей Смирнов", "Анна Волкова", "Мария Иванова", "Дмитрий Кузнецов", "Ольга Соколова" };
            var problems = new[] { "Диагностика двигателя", "Замена масла", "Ремонт подвески", "Тормозная система", "Шиномонтаж", "Электрика" };
            var rnd = new Random();

            for (int i = 0; i < count; i++)
            {
                var request = new RepairRequest
                {
                    CarBrand = brands[rnd.Next(brands.Length)],
                    CarModel = models[rnd.Next(models.Length)],
                    ProblemDescription = problems[rnd.Next(problems.Length)],
                    ClientName = names[rnd.Next(names.Length)],
                    PhoneNumber = $"+7 (9{rnd.Next(10, 99)}) {rnd.Next(100, 999)}-{rnd.Next(10, 99)}-{rnd.Next(10, 99)}",
                    Status = (RequestStatus)rnd.Next(0, 3),
                    CreatedDate = DateTime.Now.AddDays(-rnd.Next(0, 90))
                };
                Add(request);
            }
        }
        public List<RepairRequest> GetAll(RepairRequestsFilter filter)
        {
            var query = _requests.AsEnumerable();

            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.SearchText))
                {
                    var s = filter.SearchText.Trim().ToLowerInvariant();
                    query = query.Where(r =>
                        r.Id.ToString().Contains(s) ||
                        (r.ClientName ?? string.Empty).ToLower().Contains(s) ||
                        (r.CarBrand ?? string.Empty).ToLower().Contains(s) ||
                        (r.CarModel ?? string.Empty).ToLower().Contains(s) ||
                        (r.ProblemDescription ?? string.Empty).ToLower().Contains(s));
                }

                if (filter.Status.HasValue)
                    query = query.Where(r => r.Status == filter.Status.Value);

                if (filter.StartDate.HasValue)
                    query = query.Where(r => DateOnly.FromDateTime(r.CreatedDate) >= filter.StartDate.Value);

                if (filter.EndDate.HasValue)
                    query = query.Where(r => DateOnly.FromDateTime(r.CreatedDate) <= filter.EndDate.Value);
            }

            // ВАЖНО: сортировка по дате
            return query
                .OrderByDescending(r => r.CreatedDate)   // новые сверху
                .ThenBy(r => r.Id)                       // при равной дате по номеру
                .ToList();
        }

    }
}