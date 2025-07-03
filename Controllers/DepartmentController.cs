using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Hub_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly HospitalHubContext context;

        public DepartmentController(HospitalHubContext _context)
        {
            context = _context;
        }

        #region GetAllDepartments
        [HttpGet]
        public IActionResult GetAllDepartment()
        {
            var departments = context.HhDepartments.ToList();
            return Ok(departments);
        }
        #endregion

        #region GetDepartmentById
        [HttpGet("{id}")]
        public IActionResult GetDepartment(int id)
        {
            var department = context.HhDepartments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }
        #endregion

        #region AddDepartment
        [HttpPost]
        public IActionResult AddDepartment([FromBody] HhDepartment hhDepartment)
        {
            if (hhDepartment == null)
            {
                return BadRequest("Department data is null");
            }
            hhDepartment.CreatedDate = DateTime.Now;
            hhDepartment.ModifiedDate = null;
            context.HhDepartments.Add(hhDepartment);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetDepartment), new { id = hhDepartment.DepartmentId }, hhDepartment);
        }
        #endregion

        #region UpdateDepartment
        [HttpPut("{id}")]
        public IActionResult UpdateDepartment(int id, [FromBody] HhDepartment hhDepartment)
        {
            if (hhDepartment == null || hhDepartment.DepartmentId != id)
            {
                return BadRequest("Department data is null or ID mismatch");
            }
            var existingDepartment = context.HhDepartments.Find(id);
            if (existingDepartment == null)
            {
                return NotFound();
            }
            existingDepartment.DepartmentName = hhDepartment.DepartmentName;
            existingDepartment.ModifiedDate = DateTime.Now;
            context.HhDepartments.Update(existingDepartment);
            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region DeleteDepartment
        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            var department = context.HhDepartments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            context.HhDepartments.Remove(department);
            context.SaveChanges();
            return NoContent();
        }
        #endregion

    }
}
