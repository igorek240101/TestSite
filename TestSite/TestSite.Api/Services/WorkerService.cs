using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSite.Api.Entites;
using TestSite.Api.Interfacies;
using TestSite.Infrastructure.Interfaces;

namespace TestSite.Api.Services
{
    public class WorkerService : IWorkerService
    {
        const int START_WORKING_AGE = 14;
        private readonly IWorkerRepository _workerRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public WorkerService(IWorkerRepository workerRepository, IDepartmentRepository departmentRepository)
        {
            _workerRepository = workerRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<int> PagesCountAsync(int count)
        {
            return await _workerRepository.PagesCountAsync(count);
        }

        public async Task<Worker[]> GetWorkersAsync(int pageNum, int count)
        {
            return (await _workerRepository.GetWorkersAsync(pageNum, count)).ToList().ConvertAll(t => new Worker() 
            {
                Id = t.Id,
                Name = t.Name,
                Departament = _departmentRepository.GetDepartmentAsync(t.DepartamentId).Result,
                BirthDate = t.BirthDate,
                StartWorkDate = t.StartWorkDate,
                Wage = t.Wage
            }).ToArray();
        }

        public async Task NewWorkerAsync(Worker worker)
        {
            if (worker.Name == null || 
                worker.StartWorkDate == null || 
                worker.BirthDate == null || 
                worker.Departament == null || 
                worker.Wage == null)
            {
                throw new ArgumentException("Все поля дожны быть заполнены");
            }
            if (!await _departmentRepository.IsValidNameAsync(worker.Departament))
            {
                throw new ArgumentException("Некорректное наименование отдела");
            }
            if (worker.StartWorkDate.Value.AddYears(-START_WORKING_AGE) < worker.BirthDate.Value)
            {
                throw new ArgumentException($"Нельзя начать работать раньше {START_WORKING_AGE} лет");
            }
            await _workerRepository.NewWorkerAsync(new Infrastructure.Entities.Worker()
            {

            });
        }

        public Task UpdateWorkerAsync(int id, Worker worker)
        {
            throw new NotImplementedException();
        }

        public Task DeleteWorkerAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
