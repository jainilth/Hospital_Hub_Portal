using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Hub_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : Controller
    {
        private readonly HospitalHubContext context;
        public HospitalController(HospitalHubContext _context)
        {
            context = _context;
        }

        #region GetAllHospitals
        [HttpGet]
        public IActionResult GetAllHospitals()
        {
            var hospitals = context.HhHospitals.ToList();
            return Ok(hospitals);
        }
        #endregion

        #region GetHospitalById
        [HttpGet("{id}")]
        public IActionResult GetHospitalById(int id)
        {
            var hospital = context.HhHospitals.Find(id);
            if (hospital == null)
            {
                return NotFound();
            }
            return Ok(hospital);
        }
        #endregion

        #region AddHospital
        [HttpPost]
        public IActionResult AddHospital([FromBody] HhHospital hhHospital)
        {
            if (hhHospital == null)
            {
                return BadRequest("Hospital data is null");
            }
            hhHospital.CreatedDate = DateTime.Now;
            hhHospital.ModifiedDate = null;
            context.HhHospitals.Add(hhHospital);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetHospitalById), new { id = hhHospital.HospitalId }, hhHospital);
        }
        #endregion

        #region UpdateHospital
        [HttpPut("{id}")]
        public IActionResult UpdateHospital(int id, [FromBody] HhHospital hhHospital)
        {
            if (hhHospital == null || hhHospital.HospitalId != id)
            {
                return BadRequest("Hospital data is null or ID mismatch");
            }
            var existingHospital = context.HhHospitals.Find(id);
            if (existingHospital == null)
            {
                return NotFound();
            }
            existingHospital.HospitalName = hhHospital.HospitalName;
            existingHospital.HospitalAddress = hhHospital.HospitalAddress;
            existingHospital.HospitalContectNo = hhHospital.HospitalContectNo;
            existingHospital.ModifiedDate = DateTime.Now;
            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region DeleteHospital
        [HttpDelete("{id}")]
        public IActionResult DeleteHospital(int id)
        {
            var hospital = context.HhHospitals.Find(id);
            if (hospital == null)
            {
                return NotFound();
            }
            context.HhHospitals.Remove(hospital);
            context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
