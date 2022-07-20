using Microsoft.AspNetCore.Mvc;
using BackEnd.Data;

namespace BackEnd.Controllers
{
    [Route("api/logs")]
    [ApiController]

    // TODO where is this used?
    public class LogController : ControllerBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // POST: api/logs
        /// <summary>Legs the specified message.</summary>
        /// <param name="message">The message.</param>
        /// <returns>Result</returns>
        [HttpPost]
        public IActionResult Post([FromBody] LogMessage message)
        {
            var msg = $"MESSAGE: {message.Message} - " +
                      $"FILE: {message.FileName} - " +
                      $"LEVEL: {message.Level} - " +
                      $"LINENUMBER: {message.LineNumber} - " +
                      $"TIMESTAMP: {message.Timestamp:F}";
            log.Info(msg);
            log.Error(msg);
            return Ok();
        }


    }


}