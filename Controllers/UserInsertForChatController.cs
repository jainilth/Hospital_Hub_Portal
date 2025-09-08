using System.Security.Claims;
using System.Threading.Tasks;
using Hospital_Hub_Portal.Dtos;
using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Require JWT
    public class ChatController : ControllerBase
    {
        private readonly HospitalHubContext _context;

        public ChatController(HospitalHubContext context)
        {
            _context = context;
        }

        #region Chat Send
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
        {
            // ✅ 1. Validate input
            if (string.IsNullOrEmpty(request.Message))
                return BadRequest("Message cannot be empty.");

            // ✅ 2. Find or create conversation
            var conversation = await _context.Conversations
                .FirstOrDefaultAsync(c => c.DoctorId == request.DoctorId && c.PatientId == request.PatientId);

            if (conversation == null)
            {
                conversation = new Conversation
                {
                    DoctorId = request.DoctorId,
                    PatientId = request.PatientId
                };

                _context.Conversations.Add(conversation);
                await _context.SaveChangesAsync(); // need ID for message FK
            }

            // ✅ 3. Decide sender based on userRole
            int sendById;
            if (request.UserRole == "User")
                sendById = request.PatientId;
            else if (request.UserRole == "Admin")
                sendById = request.DoctorId;
            else
                return BadRequest("Invalid userRole. Must be 'Patient' or 'Doctor'.");

            // ✅ 4. Save message
            var message = new Message
            {
                ConversationId = conversation.ConversationId,
                Message1 = request.Message,
                SendBy = sendById,
                CreatedAt = DateTime.UtcNow
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            // ✅ 5. Return response
            return Ok(new
            {
                messageId = message.MessageId,
                conversationId = conversation.ConversationId,
                message = message.Message1,
                sendBy = message.SendBy,
                createdAt = message.CreatedAt
            });
        }
        #endregion

    public class PatientRequestDto
    {
        public int DoctorId { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; }
        public string Admin { get; set; }
        public string Email { get; set; }
    }

        //#region Get Patient IDs who have sent messages to the Doctor
        //[HttpPost("patients")]
        //[Authorize] // requires token
        //public async Task<IActionResult> GetPatientsByDoctor([FromBody] PatientRequestDto request)
        //{
        //    var patientIds = await _context.Conversations
        //        .Where(c => c.DoctorId == request.DoctorId)
        //        .Select(c => c.PatientId)
        //        .Distinct()
        //        .ToListAsync();

        //    return Ok(new
        //    {
        //        request.DoctorId,
        //        request.UserId,
        //        request.Role,
        //        request.Admin,
        //        request.Email,
        //        Patients = patientIds
        //    });
        //}
        //#endregion

        #region Test endpoint without authentication (for testing only)
        [HttpPost("patients-test")]
        [AllowAnonymous] // No authentication required for testing
        public async Task<IActionResult> GetPatientsByDoctorTest([FromBody] PatientRequestDto request)
        {
            var patientIds = await _context.Conversations
                .Where(c => c.DoctorId == request.DoctorId)
                .Select(c => c.PatientId)
                .Distinct()
                .ToListAsync();

            return Ok(new
            {
                request.DoctorId,
                request.UserId,
                request.Role,
                request.Admin,
                request.Email,
                Patients = patientIds,
                Message = "This is a test endpoint - no authentication required"
            });
        }
        #endregion

        //#region Get Chat in the Message 
        //[HttpGet("getChat")]
        //public IActionResult getChat()
        //{

        //}
        //#endregion
    }
}
