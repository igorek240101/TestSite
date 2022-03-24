using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSite.Api.Entites;

namespace TestSite.Api.Interfacies
{
    interface IWorkerService
    {
        public Task<int> PagesCountAsync(int count);
        public Task<Worker[]> GetWorkersAsync(int pageNum, int count);
        public Task DeleteWorkerAsync(int id);
        public Task NewWorkerAsync(Worker worker);
        public Task UpdateWorkerAsync(int id, Worker worker);
    }
}
