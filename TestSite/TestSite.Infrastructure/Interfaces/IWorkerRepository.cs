using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestSite.Infrastructure.Entities;

namespace TestSite.Infrastructure.Interfaces
{
    public interface IWorkerRepository
    {
        public Task<int> WorkersCountAsync(Filter filter);

        public Task<Worker[]> GetWorkersAsync(int pageNum, int count, Filter filter, Sort sort);

        public Task<Worker> GetWorkerAsync(int id);

        public Task DeleteWorkerAsync(int id);

        public Task NewWorkerAsync(Worker worker);

        public Task UpdateWorkerAsync(int id, string name, int wage, int departamentId, DateTime birthDate, DateTime startWorkDate);

        public struct Filter
        {
            public int? MinWage { get; set; }

            public int? MaxWage { get; set; }

            public DateTime? MinBirth { get; set; }

            public DateTime? MaxBirth { get; set; }

            public DateTime? MinStartWork { get; set; }

            public DateTime? MaxStartWork { get; set; }

            public int[] Departament { get; set; }
        }

        public struct Sort
        {
            public bool? isSort { get; set; }

            public Key sortKey { get; set; }

            public enum Key
            {
                Name,
                Wage,
                Birth,
                StartWork,
                Department
            }
        }
    }
}
