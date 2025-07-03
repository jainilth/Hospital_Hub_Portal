using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Hub_Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : Controller
    {
        private readonly HospitalHubContext context;
        public MedicineController(HospitalHubContext _context)
        {
            context = _context;
        }

        #region GetAllMedicines
        [HttpGet]
        public IActionResult GetAllMedicines()
        {
            var medicines = context.HhMedicines.ToList();
            return Ok(medicines);
        }
        #endregion

        #region GetMedicineById
        [HttpGet("{id}")]
        public IActionResult GetMedicineById(int id)
        {
            var medicine = context.HhMedicines.Find(id);
            if (medicine == null)
            {
                return NotFound();
            }
            return Ok(medicine);
        }
        #endregion

        #region AddMedicine
        [HttpPost]
        public IActionResult AddMedicine([FromBody] HhMedicine hhMedicine)
        {
            if (hhMedicine == null)
            {
                return BadRequest("Medicine data is null");
            }
            hhMedicine.CreatedDate = DateTime.Now;
            hhMedicine.ModifiedDate = null;
            context.HhMedicines.Add(hhMedicine);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetMedicineById), new { id = hhMedicine.MedicineId }, hhMedicine);
        }
        #endregion

        #region UpdateMedicine
        [HttpPut]
        public IActionResult UpdateMedicine(int id, [FromBody] HhMedicine hhMedicine)
        {
            if (hhMedicine == null || hhMedicine.MedicineId != id)
            {
                return BadRequest("Medicine data is null or ID mismatch");
            }
            var existingMedicine = context.HhMedicines.Find(id);
            if (existingMedicine == null)
            {
                return NotFound();
            }
            existingMedicine.MedicineName = hhMedicine.MedicineName;
            existingMedicine.MedicineDescription = hhMedicine.MedicineDescription;
            existingMedicine.MedicinePrice = hhMedicine.MedicinePrice;
            existingMedicine.ModifiedDate = DateTime.Now;
            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region DeleteMedicine
        [HttpDelete]
        public IActionResult DeleteMedicine(int id)
        {
            var medicine = context.HhMedicines.Find(id);
            if (medicine == null)
            {
                return NotFound();
            }
            context.HhMedicines.Remove(medicine);
            context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
    }
