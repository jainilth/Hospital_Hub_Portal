using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Hub_Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineCategoryController : Controller
    {
        private readonly HospitalHubContext context;
        public MedicineCategoryController(HospitalHubContext _context)
        {
            context = _context;
        }

        #region GetAllMedicineCategories
        [HttpGet]
        public IActionResult GetAllMedicineCategories()
        {
            var categories = context.HhMedicineCategories.ToList();
            return Ok(categories);
        }
        #endregion

        #region GetMedicineCategoryById
        [HttpGet("{id}")]
        public IActionResult GetMedicineCategoryById(int id)
        {
            var category = context.HhMedicineCategories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        #endregion

        #region AddMedicineCategory
        [HttpPost]
        public IActionResult AddMedicineCategory([FromBody] HhMedicineCategory hhMedicineCategory)
        {
            if (hhMedicineCategory == null)
            {
                return BadRequest("Medicine category data is null");
            }
            hhMedicineCategory.CreatedDate = DateTime.Now;
            hhMedicineCategory.ModifiedDate = null;
            context.HhMedicineCategories.Add(hhMedicineCategory);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetMedicineCategoryById), new { id = hhMedicineCategory.CategoryId }, hhMedicineCategory);
        }
        #endregion

        #region UpdateMedicineCategory
        [HttpPut("{id}")]
        public IActionResult UpdateMedicineCategory(int id, [FromBody] HhMedicineCategory hhMedicineCategory)
        {
            if (hhMedicineCategory == null || hhMedicineCategory.CategoryId != id)
            {
                return BadRequest("Medicine category data is null or ID mismatch");
            }
            var existingCategory = context.HhMedicineCategories.Find(id);
            if (existingCategory == null)
            {
                return NotFound();
            }
            existingCategory.CategoryName = hhMedicineCategory.CategoryName;
            existingCategory.ModifiedDate = DateTime.Now;
            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region DeleteMedicineCategory
        [HttpDelete("{id}")]
        public IActionResult DeleteMedicineCategory(int id)
        {
            var category = context.HhMedicineCategories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            context.HhMedicineCategories.Remove(category);
            context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
