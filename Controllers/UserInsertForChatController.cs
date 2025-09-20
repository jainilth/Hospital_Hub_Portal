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

        #region Get Msg By ID
        [HttpGet("GetMSG")]
        public IActionResult GetMSGFromTheIDOfPatientAndDoctor(int doctorId, int patientId)
        {
            try
            {
                var conversation = _context.Conversations
                    .FirstOrDefault(c => c.DoctorId == doctorId && c.PatientId == patientId);

                if (conversation == null)
                {
                    return Ok(new List<object>()); // Return empty list instead of NotFound
                }

                var messages = _context.Messages
                    .Where(m => m.ConversationId == conversation.ConversationId)
                    .OrderBy(m => m.CreatedAt) // Sort by creation date (oldest to newest)
                    .Select(m => new
                    {
                        m.MessageId,
                        m.Message1,    
                        m.SendBy,
                        m.CreatedAt,
                        // Determine sender type based on SendBy field
                        SenderType = m.SendBy == doctorId ? "Doctor" : 
                                   m.SendBy == patientId ? "Patient" : "Admin",
                        // Get sender name
                        SenderName = m.SendBy == doctorId ? 
                                   _context.HhDoctors.FirstOrDefault(d => d.DoctorId == doctorId).DoctorName :
                                   _context.HhUsers.FirstOrDefault(u => u.UserId == m.SendBy).UserName
                    })
                    .ToList();

                return Ok(messages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region Get Chat Partner Name
        [HttpGet("GetChatPartnerName")]
        public IActionResult GetChatPartnerName(int doctorId, int patientId, string userRole)
        {
            try
            {
                string partnerName = "";
                
                if (userRole == "Admin")
                {
                    // Admin is chatting with patient, show patient name
                    var patient = _context.HhUsers.FirstOrDefault(u => u.UserId == patientId);
                    partnerName = patient?.UserName ?? "Unknown Patient";
                }
                else if (userRole == "User")
                {
                    // Patient is chatting with doctor, show doctor name
                    var doctor = _context.HhDoctors.FirstOrDefault(d => d.DoctorId == doctorId);
                    partnerName = doctor?.DoctorName ?? "Unknown Doctor";
                }
                else
                {
                    return BadRequest("Invalid user role");
                }

                return Ok(new { partnerName });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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
