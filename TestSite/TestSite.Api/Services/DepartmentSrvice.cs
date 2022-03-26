using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSite.Api.Interfacies;
using TestSite.Infrastructure.Interfaces;

namespace TestSite.Api.Services
{
    public class DepartmentSrvice : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentSrvice(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<string[]> GetDepartmentsAsync()
        {
            return (await _departmentRepository.GetDepartmentsAsync()).ToList().ConvertAll(t => t.Name).ToArray();
        }
    }
}
