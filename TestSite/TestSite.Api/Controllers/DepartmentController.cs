using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSite.Api.Interfacies;

namespace TestSite.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IWorkerService workerService, IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        [Route("GetDepartments")]
        public async Task<ActionResult<string[]>> GetDepartmentsAsync()
        {
            try
            {
                return Ok(await _departmentService.GetDepartmentsAsync());
            }
            catch (ArgumentException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch
            {
                return BadRequest("Ошибка подключения к БД");
            }
        }
    }
}
