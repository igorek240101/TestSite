using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSite.Api.Interfacies;
using TestSite.Api.Entites;
using System.Globalization;
using System.IO;

namespace TestSiteApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService _workerService;

        public WorkerController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        [HttpPut]
        [Route("WorkersCount")]
        public async Task<ActionResult<int>> WorkersCountAsync(Filter filter)
        {
            try
            {
                return Ok(await _workerService.WorkersCountAsync(filter));
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

        [HttpPut]
        [Route("GetWorkers/{pageNum}/{count}")]
        public async Task<ActionResult<object>> GetWorkersAsync(int pageNum, int count, GetWorkerParams param)
        {
            try
            {
                return Ok(new { workers = (await _workerService.GetWorkersAsync(pageNum, count, param.Filter, param.Sort)).ToList().ConvertAll(
                    t => new { id = t.Id,
                               name = t.Name,
                               wage = t.Wage,
                               birthDate = t.BirthDate.Value.ToString("d", CultureInfo.GetCultureInfo("de-DE")),
                               startWorkDate = t.StartWorkDate.Value.ToString("d", CultureInfo.GetCultureInfo("de-DE")),
                               departament = t.Departament}) });
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

        [HttpDelete]
        [Route("DeleteWorker/{id}")]
        public async Task<ActionResult> DeleteWorkerAsync(int id)
        {
            try
            {
                await _workerService.DeleteWorkerAsync(id);
                return Ok();
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

        [HttpPost]
        [Route("NewWorkerAsync")]
        public async Task<ActionResult> NewWorkerAsync(Worker worker)
        {
            try
            {
                await _workerService.NewWorkerAsync(worker);
                return Ok();
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

        [HttpPut]
        [Route("UpdateWorker")]
        public async Task<ActionResult> UpdateWorkerAsync(Worker worker)
        {
            try
            {
                await _workerService.UpdateWorkerAsync(worker);
                return Ok();
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
