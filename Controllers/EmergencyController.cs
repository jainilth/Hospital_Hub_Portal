using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Hub_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmergencyController : Controller
    {
        private readonly HospitalHubContext context;
        public EmergencyController(HospitalHubContext _context)
        {
            context = _context;
        }

        #region GetAllEmergencies
        [HttpGet]
        public IActionResult GetAllEmergencies()
        {
            var emergencies = context.HhEmergencies.ToList();
            return Ok(emergencies);
        }
        #endregion

        #region GetEmergencyById
        [HttpGet("{id}")]
        public IActionResult GetEmergencyById(int id)
        {
            var emergency = context.HhEmergencies.Find(id);
            if (emergency == null)
            {
                return NotFound();
            }
            return Ok(emergency);
        }
        #endregion

        #region AddEmergency
        [HttpPost]
        public IActionResult AddEmergency([FromBody] HhEmergency hhEmergency)
        {
            if (hhEmergency == null)
            {
                return BadRequest("Emergency data is null");
            }
            hhEmergency.CreatedDate = DateTime.Now;
            hhEmergency.ModifiedDate = null;
            context.HhEmergencies.Add(hhEmergency);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetEmergencyById), new { id = hhEmergency.EmergencyId }, hhEmergency);
        }
        #endregion

        #region UpdateEmergency
        [HttpPut("{id}")]
        public IActionResult UpdateEmergency(int id, [FromBody] HhEmergency hhEmergency)
        {
            if (hhEmergency == null || hhEmergency.EmergencyId != id)
            {
                return BadRequest("Emergency data is null or ID mismatch");
            }
            var existingEmergency = context.HhEmergencies.Find(id);
            if (existingEmergency == null)
            {
                return NotFound();
            }
            existingEmergency.Description = hhEmergency.Description;
            existingEmergency.ModifiedDate = DateTime.Now;
            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region DeleteEmergency
        [HttpDelete("{id}")]
        public IActionResult DeleteEmergency(int id)
        {
            var emergency = context.HhEmergencies.Find(id);
            if (emergency == null)
            {
                return NotFound();
            }
            context.HhEmergencies.Remove(emergency);
            context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
