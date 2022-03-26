using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestSite.Infrastructure.Entities;

namespace TestSite.Infrastructure.Interfaces
{
    public interface IDepartmentRepository
    {
        public Task<string> GetDepartmentAsync(int id);

        public Task<int> IdByNameAsync(string name);

        public Task<Departament[]> GetDepartmentsAsync();
    }
}
