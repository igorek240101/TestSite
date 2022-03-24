using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using TestSite.Infrastructure.Context;
using TestSite.Infrastructure.Entities;
using TestSite.Infrastructure.Interfaces;

namespace TestSite.Infrastructure.Repositories
{
    class DepartmentRepository : IDepartmentRepository
    {
        private readonly TestSiteContext _testSiteContext;

        public DepartmentRepository(TestSiteContext testSiteContext)
        {
            _testSiteContext = testSiteContext;
        }

        public void Dispose()
        {
            _testSiteContext.Dispose();
        }

        public async Task<string> GetDepartmentAsync(int id)
        {
            Departament departament = await _testSiteContext.Departament.FirstOrDefaultAsync(t => t.Id == id);
            if (departament == null)
            {
                throw new ArgumentException("Отдел с таким id не найден");
            }
            else
            {
                return departament.Name;
            }
        }

        public async Task<bool> IsValidNameAsync(string name)
        {
            return await _testSiteContext.Departament.FirstOrDefaultAsync(t => t.Name == name) != null;
        }
    }
}
