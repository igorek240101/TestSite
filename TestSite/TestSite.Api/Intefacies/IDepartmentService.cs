using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestSite.Api.Interfacies
{
    public interface IDepartmentService
    {
        public Task<string[]> GetDepartmentsAsync();
    }
}
