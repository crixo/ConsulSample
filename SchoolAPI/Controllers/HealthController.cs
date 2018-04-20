using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SchoolAPI.Controllers.API
{
    [Route("api/[controller]")]
    public class HealthController : Controller
    {
        private readonly ILogger<HealthController> _logger;

        private static int _counter;

        public HealthController(ILogger<HealthController> logger)
        {
            this._logger = logger;
        }

        [HttpGet("status")]        
        public IActionResult Status()
        {
            _counter++;

            if (_counter % 3 != 0)
            {
                _logger.LogInformation("status ok");
                return Ok();
            }

            _logger.LogError("status ko");
            return this.StatusCode(500, "Error");
        }
        
    }
}
