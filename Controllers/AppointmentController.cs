using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Hub_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : Controller
    {
        private readonly HospitalHubContext context;
        public AppointmentController(HospitalHubContext _context)
        {
            context = _context;
        }

        #region GetAllAppointments
        [HttpGet]
        public IActionResult GetAllAppointments()
        {
            var appointments = context.HhAppointments.ToList();
            return Ok(appointments);
        }
        #endregion

        #region GetAppointmentById
        [HttpGet("{id}")]
        public IActionResult GetAppointmentById(int id)
        {
            var appointment = context.HhAppointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }
        #endregion

        #region AddAppointment
        [HttpPost]
        public IActionResult AddAppointment([FromBody] HhAppointment hhAppointment)
        {
            if (hhAppointment == null)
            {
                return BadRequest("Appointment data is null");
            }
            hhAppointment.CreatedDate = DateTime.Now;
            hhAppointment.ModifiedDate = null;
            context.HhAppointments.Add(hhAppointment);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetAppointmentById), new { id = hhAppointment.AppointmentId }, hhAppointment);
        }
        #endregion

        #region UpdateAppointment
        [HttpPut("{id}")]
        public IActionResult UpdateAppointment(int id, [FromBody] HhAppointment hhAppointment)
        {
            if (hhAppointment == null || hhAppointment.AppointmentId != id)
            {
                return BadRequest("Appointment data is null or ID mismatch");
            }
            var existingAppointment = context.HhAppointments.Find(id);
            if (existingAppointment == null)
            {
                return NotFound();
            }
            existingAppointment.UserId = hhAppointment.UserId;
            existingAppointment.DoctorId = hhAppointment.DoctorId;
            existingAppointment.HospitalId = hhAppointment.HospitalId;
            existingAppointment.AppointmentDateTime = hhAppointment.AppointmentDateTime;
            existingAppointment.AppointmentStatus = hhAppointment.AppointmentStatus;
            existingAppointment.Symptoms = hhAppointment.Symptoms;
            existingAppointment.Notes = hhAppointment.Notes;
            existingAppointment.ModifiedDate = DateTime.Now;
            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region DeleteAppointment
        [HttpDelete("{id}")]
        public IActionResult DeleteAppointment(int id)
        {
            var appointment = context.HhAppointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }
            context.HhAppointments.Remove(appointment);
            context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
