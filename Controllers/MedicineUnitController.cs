using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Hub_Portal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MedicineUnitController : Controller
    {
        private readonly HospitalHubContext context;
        public MedicineUnitController(HospitalHubContext _context)
        {
            context = _context;
        }

        #region GetAllMedicineUnits
        [HttpGet]
        public IActionResult GetAllMedicineUnits()
        {
            var units = context.HhMedicineUnits.ToList();
            return Ok(units);
        }
        #endregion

        #region GetMedicineUnitById
        [HttpGet("{id}")]
        public IActionResult GetMedicineUnitById(int id)
        {
            var unit = context.HhMedicineUnits.Find(id);
            if (unit == null)
            {
                return NotFound();
            }
            return Ok(unit);
        }
        #endregion

        #region AddMedicineUnit
        [HttpPost]
        public IActionResult AddMedicineUnit([FromBody] HhMedicineUnit hhMedicineUnit)
        {
            if (hhMedicineUnit == null)
            {
                return BadRequest("Medicine unit data is null");
            }
            hhMedicineUnit.CreatedDate = DateTime.Now;
            hhMedicineUnit.ModifiedDate = null;
            context.HhMedicineUnits.Add(hhMedicineUnit);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetMedicineUnitById), new { id = hhMedicineUnit.UnitId }, hhMedicineUnit);
        }
        #endregion

        #region UpdateMedicineUnit
        [HttpPut("{id}")]
        public IActionResult UpdateMedicineUnit(int id, [FromBody] HhMedicineUnit hhMedicineUnit)
        {
            if (hhMedicineUnit == null || hhMedicineUnit.UnitId != id)
            {
                return BadRequest("Medicine unit data is null or ID mismatch");
            }
            var existingUnit = context.HhMedicineUnits.Find(id);
            if (existingUnit == null)
            {
                return NotFound();
            }
            existingUnit.UnitName = hhMedicineUnit.UnitName;
            existingUnit.ModifiedDate = DateTime.Now;
            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region DeleteMedicineUnit
        [HttpDelete("{id}")]
        public IActionResult DeleteMedicineUnit(int id)
        {
            var unit = context.HhMedicineUnits.Find(id);
            if (unit == null)
            {
                return NotFound();
            }
            context.HhMedicineUnits.Remove(unit);
            context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
