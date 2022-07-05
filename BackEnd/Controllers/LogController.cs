using Microsoft.AspNetCore.Mvc;
using BackEnd.Data;

namespace BackEnd.Controllers
{
    [Route("api/logs")]
    [ApiController]
    
    public class LogController : ControllerBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // POST: api/logs
        [HttpPost]
        public IActionResult Post([FromBody] AngLog dto)
        {
            var msg = $"MESSAGE: {dto.Message} - " +
                      $"FILE: {dto.FileName} - " +
                      $"LEVEL: {dto.Level} - " +
                      $"LINENUMBER: {dto.LineNumber} - " +
                      $"TIMESTAMP: {dto.Timestamp:F}";
            log.Info(msg);
            log.Error(msg);
            return Ok();
        }


    }


}
