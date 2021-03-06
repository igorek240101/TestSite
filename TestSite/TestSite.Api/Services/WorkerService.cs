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

        public async Task<int> WorkersCountAsync(Filter filter)
        {
            return await _workerRepository.WorkersCountAsync(await ReFilter(filter));
        }

        public async Task<Worker[]> GetWorkersAsync(int pageNum, int count, Filter filter, IWorkerRepository.Sort sort)
        {
            return (await _workerRepository.GetWorkersAsync(pageNum, count, await ReFilter(filter), sort)).ToList().ConvertAll(t => new Worker() 
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
            int departamentId = await _departmentRepository.IdByNameAsync(worker.Departament);
            while (departamentId == -1)
            {
                await _departmentRepository.AddDepartment(worker.Departament);
                departamentId = await _departmentRepository.IdByNameAsync(worker.Departament);
            }
            if (worker.StartWorkDate.Value.AddYears(-START_WORKING_AGE) < worker.BirthDate.Value)
            {
                throw new ArgumentException($"Нельзя начать работать раньше {START_WORKING_AGE} лет");
            }
            await _workerRepository.NewWorkerAsync(new Infrastructure.Entities.Worker()
            {
                Name = worker.Name,
                Wage = worker.Wage.Value,
                BirthDate = worker.BirthDate.Value,
                StartWorkDate = worker.StartWorkDate.Value,
                DepartamentId = departamentId,
            });
        }

        public async Task UpdateWorkerAsync(Worker worker)
        {
            if (worker.Id == null)
            {
                throw new ArgumentException("Пустое значение id");
            }
            Infrastructure.Entities.Worker oldWorker = await _workerRepository.GetWorkerAsync(worker.Id.Value);
            if (oldWorker == null)
            {
                throw new ArgumentException("Пользователь с таким id не найден");
            }
            string name = worker.Name == null ? oldWorker.Name : worker.Name;
            int wage = worker.Wage == null ? oldWorker.Wage : worker.Wage.Value;
            DateTime birthDate = worker.BirthDate == null ? oldWorker.BirthDate : worker.BirthDate.Value;
            DateTime startWorkDate = worker.StartWorkDate == null ? oldWorker.StartWorkDate : worker.StartWorkDate.Value;
            int departamentId = worker.Departament == null ? oldWorker.DepartamentId : await _departmentRepository.IdByNameAsync(worker.Departament);
            if (departamentId == -1)
            {
                throw new ArgumentException("Некорректное наименование отдела");
            }
            if (startWorkDate.AddYears(-START_WORKING_AGE) < birthDate)
            {
                throw new ArgumentException($"Нельзя начать работать раньше {START_WORKING_AGE} лет");
            }
            await _workerRepository.UpdateWorkerAsync(oldWorker.Id, name, wage, departamentId, birthDate, startWorkDate);
        }

        public async Task DeleteWorkerAsync(int id)
        {
            await _workerRepository.DeleteWorkerAsync(id);
        }

        private async Task<IWorkerRepository.Filter> ReFilter(Filter filter)
        {
            int[] departmentId;
            if (filter.Departament == null)
            {
                departmentId = null;
            }
            else
            {
                departmentId = new int[filter.Departament.Length];
                for (int i = 0; i < departmentId.Length; i++)
                {
                    departmentId[i] = await _departmentRepository.IdByNameAsync(filter.Departament[i]);
                }
            }
            return new IWorkerRepository.Filter()
            {
                MinWage = filter.MinWage,
                MaxWage = filter.MaxWage,
                MinBirth = filter.MinBirth,
                MaxBirth = filter.MaxBirth,
                MinStartWork = filter.MinStartWork,
                MaxStartWork = filter.MaxStartWork,
                Departament = departmentId
            };
        }
    }
}
