using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Hub_Portal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly HospitalHubContext context;
        public UserController(HospitalHubContext _context)
        {
            context = _context;
        }

        #region GetAllUsers
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = context.HhUsers.ToList();
            return Ok(users);
        }
        #endregion

        #region GetUserById
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = context.HhUsers.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        #endregion

        #region AddUser
        [HttpPost]
        public IActionResult AddUser([FromBody] HhUser hhUser)
        {
            if (hhUser == null)
            {
                return BadRequest("User data is null");
            }
            hhUser.CreatedDate = DateTime.Now;
            hhUser.ModifiedDate = null;
            context.HhUsers.Add(hhUser);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetUserById), new { id = hhUser.UserId }, hhUser);
        }
        #endregion

        #region UpdateUser
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] HhUser hhUser)
        {
            if (hhUser == null || hhUser.UserId != id)
            {
                return BadRequest("User data is null or ID mismatch");
            }
            var existingUser = context.HhUsers.Find(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            existingUser.UserName = hhUser.UserName;
            existingUser.UserEmail = hhUser.UserEmail;
            existingUser.ModifiedDate = DateTime.Now;
            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region DeleteUser
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = context.HhUsers.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            context.HhUsers.Remove(user);
            context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
