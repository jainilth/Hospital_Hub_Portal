using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Controllers
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class UserInsertForChatController : Controller
    {
        private readonly HospitalHubContext context;

        public UserInsertForChatController(HospitalHubContext _context)
        {
            context = _context;
        }

        [HttpPost]
        public IActionResult ConsultNow(int patientId, int doctorId)
        {
            // 🔹 Check if patient exists
            var patientExists = context.Users.Any(u => u.UserId == patientId);
            
            if (!patientExists)
            {

                var newPatient = new User
                {
                    ChatUserId = patientId,
                };
                context.Users.Add(newPatient);
            }

            // 🔹 Check if doctor exists
            var doctorExists = context.Users.Any(u => u.UserId == doctorId);
            if (!doctorExists)
            {
                var newDoctor = new User
                {
                    ChatUserId = doctorId,
                };
                context.Users.Add(newDoctor);
            }

            context.SaveChanges();

            return Ok("Patient and Doctor check complete. New records added if not already present.");
        }
    }
}
