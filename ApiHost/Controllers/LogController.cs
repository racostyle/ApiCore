using ApiHost.Database;
using ApiHost.DTO;
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

        [HttpGet("status")]
        public IActionResult Status()
        {
            return Ok("LogController is working!");
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LogDTO log)
        {
           
            await _sqlHandler.CreateLogsDatabaseIfDoesNotExist();
            await _sqlHandler.CreateLogsDatabaseIfDoesNotExist();

            var result = await _sqlHandler.Post(log);

            if (result)
                return Ok("Log saved successfully.");
            return BadRequest("Log was not saved in database");
        }
    }
}
