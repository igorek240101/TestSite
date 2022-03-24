using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestSite.Infrastructure.Entities;

namespace TestSite.Infrastructure.Interfaces
{
    public interface IWorkerRepository
    {
        public Task<int> PagesCountAsync(int count);
        public Task<Worker[]> GetWorkersAsync(int pageNum, int count);
        public Task DeleteWorkerAsync(int id);
        public Task NewWorkerAsync(Worker worker);
        public Task UpdateWorkerAsync(int id, Worker worker);
    }
}
