using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using TestSite.Infrastructure.Context;
using TestSite.Infrastructure.Entities;
using TestSite.Infrastructure.Interfaces;
using System;

namespace TestSite.Infrastructure.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly TestSiteContext _testSiteContext;

        public WorkerRepository(TestSiteContext testSiteContext)
        {
            _testSiteContext = testSiteContext;
        }

        public void Dispose()
        {
            _testSiteContext.Dispose();
        }

        public async Task<int> WorkersCountAsync(IWorkerRepository.Filter filter)
        {
            IQueryable<Worker> queryable = _testSiteContext.Worker.Where(
                t => (filter.MaxWage.HasValue ? filter.MaxWage.Value >= t.Wage : true) &&
                (filter.MaxBirth.HasValue ? filter.MaxBirth.Value >= t.BirthDate : true) &&
                (filter.MaxStartWork.HasValue ? filter.MaxStartWork.Value >= t.StartWorkDate : true) &&
                (filter.MinStartWork.HasValue ? filter.MinStartWork.Value <= t.StartWorkDate : true) &&
                (filter.MinBirth.HasValue ? filter.MinBirth.Value <= t.BirthDate : true) &&
                (filter.MinWage.HasValue ? filter.MinWage.Value <= t.Wage : true) &&
                (filter.Departament != null ? filter.Departament.Contains(t.DepartamentId) : true));

            return await queryable.CountAsync();
        }

        public async Task<Worker[]> GetWorkersAsync(int pageNum, int count, IWorkerRepository.Filter filter, IWorkerRepository.Sort sort)
        {
            IQueryable<Worker> queryable = _testSiteContext.Worker.Where(
                t => (filter.MaxWage.HasValue ? filter.MaxWage.Value >= t.Wage : true) &&
                (filter.MaxBirth.HasValue ? filter.MaxBirth.Value >= t.BirthDate : true) &&
                (filter.MaxStartWork.HasValue ? filter.MaxStartWork.Value >= t.StartWorkDate : true) &&
                (filter.MinStartWork.HasValue ? filter.MinStartWork.Value <= t.StartWorkDate : true) &&
                (filter.MinBirth.HasValue ? filter.MinBirth.Value <= t.BirthDate : true) &&
                (filter.MinWage.HasValue ? filter.MinWage.Value <= t.Wage : true) &&
                (filter.Departament != null ? filter.Departament.Contains(t.DepartamentId) : true));

            if(sort.isSort.HasValue)
            {
                if(sort.isSort.Value)
                {
                    switch(sort.sortKey)
                    {
                        case IWorkerRepository.Sort.Key.Name:
                            queryable = queryable.OrderBy(t => t.Name); break;
                        case IWorkerRepository.Sort.Key.Wage:
                            queryable = queryable.OrderBy(t => t.Wage); break;
                        case IWorkerRepository.Sort.Key.Birth:
                            queryable = queryable.OrderBy(t => t.BirthDate); break;
                        case IWorkerRepository.Sort.Key.StartWork:
                            queryable = queryable.OrderBy(t => t.StartWorkDate); break;
                        case IWorkerRepository.Sort.Key.Department:
                            queryable = queryable.OrderBy(t => t.Departament); break;
                    }
                }
                else
                {
                    switch (sort.sortKey)
                    {
                        case IWorkerRepository.Sort.Key.Name:
                            queryable = queryable.OrderByDescending(t => t.Name); break;
                        case IWorkerRepository.Sort.Key.Wage:
                            queryable = queryable.OrderByDescending(t => t.Wage); break;
                        case IWorkerRepository.Sort.Key.Birth:
                            queryable = queryable.OrderByDescending(t => t.BirthDate); break;
                        case IWorkerRepository.Sort.Key.StartWork:
                            queryable = queryable.OrderByDescending(t => t.StartWorkDate); break;
                        case IWorkerRepository.Sort.Key.Department:
                            queryable = queryable.OrderByDescending(t => t.Departament); break;
                    }
                }
            }

            queryable = queryable.Skip((pageNum - 1) * count).Take(count);

            return await queryable.ToArrayAsync();
        }

        public async Task<Worker> GetWorkerAsync(int id)
        {
            return await _testSiteContext.Worker.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task NewWorkerAsync(Worker worker)
        {
            _testSiteContext.Worker.Add(worker);
            await _testSiteContext.SaveChangesAsync();
        }

        public async Task UpdateWorkerAsync(int id, string name, int wage, int departamentId, DateTime birthDate, DateTime startWorkDate)
        {
            Worker oldWorker = await _testSiteContext.Worker.FirstOrDefaultAsync(t => t.Id == id);
            if (oldWorker == null)
            {
                throw new ArgumentException("Пользователь с таким id не найден");
            }
            else
            {
                oldWorker.Name = name;
                oldWorker.Wage = wage;
                oldWorker.DepartamentId = departamentId;
                oldWorker.BirthDate = birthDate;
                oldWorker.StartWorkDate = startWorkDate;
                await _testSiteContext.SaveChangesAsync();
            }
        }

        public async Task DeleteWorkerAsync(int id)
        {
            Worker oldWorker = await _testSiteContext.Worker.FirstOrDefaultAsync(t => t.Id == id);
            if (oldWorker == null)
            {
                throw new ArgumentException("Пользователь с таким id не найден");
            }
            else
            {
                _testSiteContext.Worker.Remove(oldWorker);
                await _testSiteContext.SaveChangesAsync();
            }
        }
    }
}
