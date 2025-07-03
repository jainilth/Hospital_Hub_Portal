using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Hub_Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : Controller
    {
        private readonly HospitalHubContext context;
        public StateController(HospitalHubContext _context)
        {
            context = _context;
        }

        #region GetAllStates
        [HttpGet]
        public IActionResult GetAllStates()
        {
            var states = context.HhStates.ToList();
            return Ok(states);
        }
        #endregion

        #region GetStateById
        [HttpGet("{id}")]
        public IActionResult GetStateById(int id)
        {
            var state = context.HhStates.Find(id);
            if (state == null)
            {
                return NotFound();
            }
            return Ok(state);
        }
        #endregion

        #region AddState
        [HttpPost]
        public IActionResult AddState([FromBody] HhState hhState)
        {
            if (hhState == null)
            {
                return BadRequest("State data is null");
            }
            hhState.CreatedDate = DateTime.Now;
            hhState.ModifiedDate = null;
            context.HhStates.Add(hhState);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetStateById), new { id = hhState.StateId }, hhState);
        }
        #endregion

        #region UpdateState
        [HttpPut("{id}")]
        public IActionResult UpdateState(int id, [FromBody] HhState hhState)
        {
            if (hhState == null || hhState.StateId != id)
            {
                return BadRequest("State data is null or ID mismatch");
            }
            var existingState = context.HhStates.Find(id);
            if (existingState == null)
            {
                return NotFound();
            }
            existingState.StateName = hhState.StateName;
            existingState.ModifiedDate = DateTime.Now;
            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region DeleteState
        [HttpDelete("{id}")]
        public IActionResult DeleteState(int id)
        {
            var state = context.HhStates.Find(id);
            if (state == null)
            {
                return NotFound();
            }
            context.HhStates.Remove(state);
            context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
