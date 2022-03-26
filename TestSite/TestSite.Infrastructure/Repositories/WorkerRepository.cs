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

        public async Task<int> PagesCountAsync(int count)
        {
            int workersCount = await _testSiteContext.Worker.CountAsync();
            int pagesCount = workersCount / count;
            if (workersCount % count != 0)
            {
                pagesCount++;
            }
            return pagesCount;
        }

        public async Task<Worker[]> GetWorkersAsync(int pageNum, int count)
        {
            return await _testSiteContext.Worker.Skip((pageNum - 1) * count).Take(count).ToArrayAsync();
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
