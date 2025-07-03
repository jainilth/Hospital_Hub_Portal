using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Hub_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : Controller
    {
        private readonly HospitalHubContext context;
        public DoctorController(HospitalHubContext _context)
        {
            context = _context;
        }

        #region GetAllDoctors
        [HttpGet]
        public IActionResult GetAllDoctors()
        {
            var doctors = context.HhDoctors.ToList();
            return Ok(doctors);
        }
        #endregion

        #region GetDoctorById
        [HttpGet("{id}")]
        public IActionResult GetDoctorById(int id)
        {
            var doctor = context.HhDoctors.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return Ok(doctor);
        }
        #endregion

        #region AddDoctor
        [HttpPost]
        public IActionResult AddDoctor([FromBody] HhDoctor hhDoctor)
        {
            if (hhDoctor == null)
            {
                return BadRequest("Doctor data is null");
            }
            hhDoctor.CreatedDate = DateTime.Now;
            hhDoctor.ModifiedDate = null;
            context.HhDoctors.Add(hhDoctor);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetDoctorById), new { id = hhDoctor.DoctorId }, hhDoctor);
        }
        #endregion

        #region UpdateDoctor
        [HttpPut("{id}")]
        public IActionResult UpdateDoctor(int id, [FromBody] HhDoctor hhDoctor)
        {
            if (hhDoctor == null || hhDoctor.DoctorId != id)
            {
                return BadRequest("Doctor data is null or ID mismatch");
            }
            var existingDoctor = context.HhDoctors.Find(id);
            if (existingDoctor == null)
            {
                return NotFound();
            }
            existingDoctor.DoctorName = hhDoctor.DoctorName;
            existingDoctor.DoctorPhotoUrl = hhDoctor.DoctorPhotoUrl;
            existingDoctor.ConsultationFee = hhDoctor.ConsultationFee;
            existingDoctor.DoctorEmail = hhDoctor.DoctorEmail;
            existingDoctor.DoctorContectNo = hhDoctor.DoctorContectNo;
            existingDoctor.DoctorGender = hhDoctor.DoctorGender;
            existingDoctor.SpecializationId = hhDoctor.SpecializationId;
            existingDoctor.DepartmentId = hhDoctor.DepartmentId;
            existingDoctor.HospitalId = hhDoctor.HospitalId;
            existingDoctor.DoctorExperienceYears = hhDoctor.DoctorExperienceYears;
            existingDoctor.Rating = hhDoctor.Rating;
            existingDoctor.UserId = hhDoctor.UserId;
            existingDoctor.ModifiedDate = DateTime.Now;
            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region DeleteDoctor
        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            var doctor = context.HhDoctors.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }
            context.HhDoctors.Remove(doctor);
            context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
