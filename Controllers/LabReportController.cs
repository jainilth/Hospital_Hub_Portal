using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Hub_Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabReportController : Controller
    {
        private readonly HospitalHubContext context;
        public LabReportController(HospitalHubContext _context)
        {
            context = _context;
        }

        #region GetAllLabReports
        [HttpGet]
        public IActionResult GetAllLabReports()
        {
            var reports = context.HhLabReports.ToList();
            return Ok(reports);
        }
        #endregion

        #region GetLabReportById
        [HttpGet("{id}")]
        public IActionResult GetLabReportById(int id)
        {
            var report = context.HhLabReports.Find(id);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }
        #endregion

        #region AddLabReport
        [HttpPost]
        public IActionResult AddLabReport([FromBody] HhLabReport hhLabReport)
        {
            if (hhLabReport == null)
            {
                return BadRequest("Lab report data is null");
            }
            hhLabReport.CreatedDate = DateTime.Now;
            hhLabReport.ModifiedDate = null;
            context.HhLabReports.Add(hhLabReport);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetLabReportById), new { id = hhLabReport.ReportId }, hhLabReport);
        }
        #endregion

        #region UpdateLabReport
        [HttpPut("{id}")]
        public IActionResult UpdateLabReport(int id, [FromBody] HhLabReport hhLabReport)
        {
            if (hhLabReport == null || hhLabReport.ReportId != id)
            {
                return BadRequest("Lab report data is null or ID mismatch");
            }
            var existingReport = context.HhLabReports.Find(id);
            if (existingReport == null)
            {
                return NotFound();
            }
            existingReport.BookingId = hhLabReport.BookingId;
            existingReport.TestResultSummary = hhLabReport.TestResultSummary;
            existingReport.ModifiedDate = DateTime.Now;
            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region DeleteLabReport
        [HttpDelete("{id}")]
        public IActionResult DeleteLabReport(int id)
        {
            var report = context.HhLabReports.Find(id);
            if (report == null)
            {
                return NotFound();
            }
            context.HhLabReports.Remove(report);
            context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
