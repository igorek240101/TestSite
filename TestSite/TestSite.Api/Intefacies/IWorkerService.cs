using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSite.Api.Entites;
using TestSite.Infrastructure.Interfaces;

namespace TestSite.Api.Interfacies
{
    public interface IWorkerService
    {
        public Task<int> WorkersCountAsync(Filter filter);
        public Task<Worker[]> GetWorkersAsync(int pageNum, int count, Filter filter, IWorkerRepository.Sort sort);
        public Task DeleteWorkerAsync(int id);
        public Task NewWorkerAsync(Worker worker);
        public Task UpdateWorkerAsync(Worker worker);
    }
}
