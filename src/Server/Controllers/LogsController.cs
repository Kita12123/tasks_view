using System;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/logs")]
    public class LogsController : ControllerBase
    {
        private readonly ILogger<LogsController> _logger;
        public LogsController(ILogger<LogsController> logger) => _logger = logger;

        [HttpPost]
        public IActionResult Post([FromBody] JsonElement payload)
        {
            try
            {
                _logger.LogError("Client log: {Payload}", payload.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to record client log");
            }
            return NoContent();
        }
    }
}
