using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSite.Api.Interfacies;
using TestSite.Api.Entites;

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

        [HttpGet]
        [Route("PagesCount/{count}")]
        public async Task<ActionResult<int>> PagesCountAsync(int count)
        {
            try
            {
                return Ok(await _workerService.PagesCountAsync(count));
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

        [HttpGet]
        [Route("GetWorkers/{pageNum}/{count}")]
        public async Task<ActionResult<Worker[]>> GetWorkersAsync(int pageNum, int count)
        {
            try
            {
                return Ok(await _workerService.GetWorkersAsync(pageNum, count));
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
        [Route("DeleteWorker")]
        public async Task<ActionResult<Worker[]>> DeleteWorkerAsync(int id)
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
        public async Task<ActionResult<Worker[]>> NewWorkerAsync(Worker worker)
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
        public async Task<ActionResult<Worker[]>> UpdateWorkerAsync(Worker worker)
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
