using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Hub_Portal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SpecializationController : Controller
    {
        private readonly HospitalHubContext context;
        public SpecializationController(HospitalHubContext _context)
        {
            context = _context;
        }

        #region GetAllSpecializations
        [HttpGet]
        public IActionResult GetAllSpecializations()
        {
            var specializations = context.HhSpecializations.ToList();
            return Ok(specializations);
        }
        #endregion

        #region GetSpecializationById
        [HttpGet("{id}")]
        public IActionResult GetSpecializationById(int id)
        {
            var specialization = context.HhSpecializations.Find(id);
            if (specialization == null)
            {
                return NotFound();
            }
            return Ok(specialization);
        }
        #endregion

        #region AddSpecialization
        [HttpPost]
        public IActionResult AddSpecialization([FromBody] HhSpecialization hhSpecialization)
        {
            if (hhSpecialization == null)
            {
                return BadRequest("Specialization data is null");
            }
            hhSpecialization.CreatedDate = DateTime.Now;
            hhSpecialization.ModifiedDate = null;
            context.HhSpecializations.Add(hhSpecialization);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetSpecializationById), new { id = hhSpecialization.SpecializationId }, hhSpecialization);
        }
        #endregion

        #region UpdateSpecialization
        [HttpPut("{id}")]
        public IActionResult UpdateSpecialization(int id, [FromBody] HhSpecialization hhSpecialization)
        {
            if (hhSpecialization == null || hhSpecialization.SpecializationId != id)
            {
                return BadRequest("Specialization data is null or ID mismatch");
            }
            var existingSpecialization = context.HhSpecializations.Find(id);
            if (existingSpecialization == null)
            {
                return NotFound();
            }
            existingSpecialization.SpecializationName = hhSpecialization.SpecializationName;
            existingSpecialization.ModifiedDate = DateTime.Now;
            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region DeleteSpecialization
        [HttpDelete("{id}")]
        public IActionResult DeleteSpecialization(int id)
        {
            var specialization = context.HhSpecializations.Find(id);
            if (specialization == null)
            {
                return NotFound();
            }
            context.HhSpecializations.Remove(specialization);
            context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
