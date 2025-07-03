using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Hub_Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabTestController : Controller
    {
        private readonly HospitalHubContext context;
        public LabTestController(HospitalHubContext _context)
        {
            context = _context;
        }

        #region GetAllLabTests
        [HttpGet]
        public IActionResult GetAllLabTests()
        {
            var tests = context.HhLabTests.ToList();
            return Ok(tests);
        }
        #endregion

        #region GetLabTestById
        [HttpGet("{id}")]
        public IActionResult GetLabTestById(int id)
        {
            var test = context.HhLabTests.Find(id);
            if (test == null)
            {
                return NotFound();
            }
            return Ok(test);
        }
        #endregion

        #region AddLabTest
        [HttpPost]
        public IActionResult AddLabTest([FromBody] HhLabTest hhLabTest)
        {
            if (hhLabTest == null)
            {
                return BadRequest("Lab test data is null");
            }
            hhLabTest.CreatedDate = DateTime.Now;
            hhLabTest.ModifiedDate = null;
            context.HhLabTests.Add(hhLabTest);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetLabTestById), new { id = hhLabTest.TestId }, hhLabTest);
        }
        #endregion

        #region UpdateLabTest
        [HttpPut("{id}")]
        public IActionResult UpdateLabTest(int id, [FromBody] HhLabTest hhLabTest)
        {
            if (hhLabTest == null || hhLabTest.TestId != id)
            {
                return BadRequest("Lab test data is null or ID mismatch");
            }
            var existingTest = context.HhLabTests.Find(id);
            if (existingTest == null)
            {
                return NotFound();
            }
            existingTest.TestName = hhLabTest.TestName;
            existingTest.TestDescription = hhLabTest.TestDescription;
            existingTest.TestPrice = hhLabTest.TestPrice;
            existingTest.ModifiedDate = DateTime.Now;
            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region DeleteLabTest
        [HttpDelete("{id}")]
        public IActionResult DeleteLabTest(int id)
        {
            var test = context.HhLabTests.Find(id);
            if (test == null)
            {
                return NotFound();
            }
            context.HhLabTests.Remove(test);
            context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
