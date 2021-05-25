using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLog;
using NLog.Fluent;

namespace Push.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MyLogger : ControllerBase
    {            
        [HttpGet("nlog")]
        public IActionResult GetNLog(CancellationToken ct)
        {
            var logger = LogManager.GetCurrentClassLogger();

            logger.Error()
                .Message($"Meu log de erro ")
                .Property("Metodo", "xpto")
                .Property("Usuario", "Claudio")
                .Property("PropriedadeXpto", "Xpto")
                .Write();

            return Ok();
        }
    }
}
