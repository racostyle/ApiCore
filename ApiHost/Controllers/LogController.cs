using ApiHost.Database;
using Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : Controller
    {
        private readonly SqlHandler _sqlHandler;

        public LogController(SqlHandler sqlHandler)
        {
            _sqlHandler = sqlHandler;
        }

        private async Task SafetyChecks()
        {
            await _sqlHandler.CreateLogsDatabaseIfDoesNotExist();
            await _sqlHandler.CreateLogsDatabaseIfDoesNotExist();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await SafetyChecks();
            var loaded = await _sqlHandler.Get();
            return Ok(loaded);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LogDTO log)
        {
            await SafetyChecks();

            var result = await _sqlHandler.Post(log);

            if (result)
                return Ok("Log saved successfully.");
            return BadRequest("Log was not saved in database");
        }
    }
}
