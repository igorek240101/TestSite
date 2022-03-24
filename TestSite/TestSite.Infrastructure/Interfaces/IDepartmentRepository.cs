using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestSite.Infrastructure.Interfaces
{
    public interface IDepartmentRepository
    {
        public Task<string> GetDepartmentAsync(int id);

        public Task<bool> IsValidNameAsync(string name);
    }
}
