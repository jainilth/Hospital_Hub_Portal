using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Hub_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentCancellationLogController : Controller
    {
        private readonly HospitalHubContext context;
        public AppointmentCancellationLogController(HospitalHubContext _context)
        {
            context = _context;
        }

        #region GetAllAppointmentCancellationLogs
        [HttpGet]
        public IActionResult GetAllAppointmentCancellationLogs()
        {
            var logs = context.HhAppointmentCancellationLogs.ToList();
            return Ok(logs);
        }
        #endregion

        #region GetAppointmentCancellationLogById
        [HttpGet("{id}")]
        public IActionResult GetAppointmentCancellationLogById(int id)
        {
            var log = context.HhAppointmentCancellationLogs.Find(id);
            if (log == null)
            {
                return NotFound();
            }
            return Ok(log);
        }
        #endregion

        #region AddAppointmentCancellationLog
        [HttpPost]
        public IActionResult AddAppointmentCancellationLog([FromBody] HhAppointmentCancellationLog hhAppointmentCancellationLog)
        {
            if (hhAppointmentCancellationLog == null)
            {
                return BadRequest("Cancellation log data is null");
            }
            hhAppointmentCancellationLog.CreatedDate = DateTime.Now;
            hhAppointmentCancellationLog.ModifiedDate = null;
            context.HhAppointmentCancellationLogs.Add(hhAppointmentCancellationLog);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetAppointmentCancellationLogById), new { id = hhAppointmentCancellationLog.LogId }, hhAppointmentCancellationLog);
        }
        #endregion

        #region UpdateAppointmentCancellationLog
        [HttpPut("{id}")]
        public IActionResult UpdateAppointmentCancellationLog(int id, [FromBody] HhAppointmentCancellationLog hhAppointmentCancellationLog)
        {
            if (hhAppointmentCancellationLog == null || hhAppointmentCancellationLog.LogId != id)
            {
                return BadRequest("Cancellation log data is null or ID mismatch");
            }
            var existingLog = context.HhAppointmentCancellationLogs.Find(id);
            if (existingLog == null)
            {
                return NotFound();
            }
            existingLog.AppointmentId = hhAppointmentCancellationLog.AppointmentId;
            existingLog.CanceledBy = hhAppointmentCancellationLog.CanceledBy;
            existingLog.CancellationReason = hhAppointmentCancellationLog.CancellationReason;
            existingLog.CancellationDate = hhAppointmentCancellationLog.CancellationDate;
            existingLog.UserId = hhAppointmentCancellationLog.UserId;
            existingLog.ModifiedDate = DateTime.Now;
            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region DeleteAppointmentCancellationLog
        [HttpDelete("{id}")]
        public IActionResult DeleteAppointmentCancellationLog(int id)
        {
            var log = context.HhAppointmentCancellationLogs.Find(id);
            if (log == null)
            {
                return NotFound();
            }
            context.HhAppointmentCancellationLogs.Remove(log);
            context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
