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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly TestSiteContext _testSiteContext;

        public DepartmentRepository(TestSiteContext testSiteContext)
        {
            _testSiteContext = testSiteContext;
        }

        public async Task AddDepartment(string name)
        {
            _testSiteContext.Departament.Add(new Departament() { Name = name });
            await _testSiteContext.SaveChangesAsync();
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

        public async Task<Departament[]> GetDepartmentsAsync()
        {
            return await _testSiteContext.Departament.ToArrayAsync();
        }

        public async Task<int> IdByNameAsync(string name)
        {
            Departament departament = await _testSiteContext.Departament.FirstOrDefaultAsync(t => t.Name == name);
            if (departament == null)
            {
                return -1;
            }
            else
            {
                return departament.Id;
            }
        }
    }
}
