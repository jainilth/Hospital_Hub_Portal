using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Hub_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorAvailableTimeSlotController : Controller
    {
        private readonly HospitalHubContext context;
        public DoctorAvailableTimeSlotController(HospitalHubContext _context)
        {
            context = _context;
        }

        #region GetAllDoctorAvailableTimeSlots
        [HttpGet]
        public IActionResult GetAllDoctorAvailableTimeSlots()
        {
            var timeSlots = context.HhDoctorAvailableTimeSlots.ToList();
            return Ok(timeSlots);
        }
        #endregion

        #region GetDoctorAvailableTimeSlotById
        [HttpGet("{id}")]
        public IActionResult GetDoctorAvailableTimeSlotById(int id)
        {
            var timeSlot = context.HhDoctorAvailableTimeSlots.Find(id);
            if (timeSlot == null)
            {
                return NotFound();
            }
            return Ok(timeSlot);
        }
        #endregion

        #region AddDoctorAvailableTimeSlot
        [HttpPost]
        public IActionResult AddDoctorAvailableTimeSlot([FromBody] HhDoctorAvailableTimeSlot hhDoctorAvailableTimeSlot)
        {
            if (hhDoctorAvailableTimeSlot == null)
            {
                return BadRequest("Time slot data is null");
            }
            hhDoctorAvailableTimeSlot.CreatedDate = DateTime.Now;
            hhDoctorAvailableTimeSlot.ModifiedDate = null;
            context.HhDoctorAvailableTimeSlots.Add(hhDoctorAvailableTimeSlot);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetDoctorAvailableTimeSlotById), new { id = hhDoctorAvailableTimeSlot.SlotId }, hhDoctorAvailableTimeSlot);
        }
        #endregion

        #region UpdateDoctorAvailableTimeSlot
        [HttpPut("{id}")]
        public IActionResult UpdateDoctorAvailableTimeSlot(int id, [FromBody] HhDoctorAvailableTimeSlot hhDoctorAvailableTimeSlot)
        {
            if (hhDoctorAvailableTimeSlot == null || hhDoctorAvailableTimeSlot.SlotId != id)
            {
                return BadRequest("Time slot data is null or ID mismatch");
            }
            var existingTimeSlot = context.HhDoctorAvailableTimeSlots.Find(id);
            if (existingTimeSlot == null)
            {
                return NotFound();
            }
            existingTimeSlot.StartTime = hhDoctorAvailableTimeSlot.StartTime;
            existingTimeSlot.EndTime = hhDoctorAvailableTimeSlot.EndTime;
            existingTimeSlot.ModifiedDate = DateTime.Now;
            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region DeleteDoctorAvailableTimeSlot
        [HttpDelete("{id}")]
        public IActionResult DeleteDoctorAvailableTimeSlot(int id)
        {
            var timeSlot = context.HhDoctorAvailableTimeSlots.Find(id);
            if (timeSlot == null)
            {
                return NotFound();
            }
            context.HhDoctorAvailableTimeSlots.Remove(timeSlot);
            context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
