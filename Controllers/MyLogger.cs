using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Fluent;

namespace Push.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MyLogger : ControllerBase
    {


        public MyLogger()
        {
            
        }
        [HttpGet("nlog")]
        public IActionResult GetNLog(CancellationToken ct)
        {

            var logger = LogManager.GetCurrentClassLogger();

            logger.Info()
                .Message("Log Erro Claudio")
                .Property("Erropayload", "Teste Dart")
                .Write();

            return Ok();
        }
    }
}
