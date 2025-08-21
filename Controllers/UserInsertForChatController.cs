using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly HospitalHubContext _context;

        public ChatController(HospitalHubContext context)
        {
            _context = context;
        }

        // ---------------- Start Chat ----------------
        [HttpPost("StartChat")]
        public async Task<IActionResult> StartChat([FromBody] StartChatRequest request)
        {
            if (request == null || request.DoctorId <= 0)
                return BadRequest("Invalid request");

            int patientId = 3; // default patient ID

            // Step 1: Ensure both Doctor and Patient exist in User table
            var usersToAdd = new List<int> { request.DoctorId, patientId };

            foreach (var chatUserId in usersToAdd)
            {
                bool exists = await _context.Users.AnyAsync(u => u.ChatUserId == chatUserId);
                if (!exists)
                {
                    var newUser = new User
                    {
                        ChatUserId = chatUserId
                    };
                    _context.Users.Add(newUser);
                }
            }
            await _context.SaveChangesAsync();

            // Step 2: Check if conversation already exists
            var conversation = await _context.Conversations
                .FirstOrDefaultAsync(c => c.DoctorId == request.DoctorId && c.PatientId == patientId);

            if (conversation == null)
            {
                conversation = new Conversation
                {
                    DoctorId = request.DoctorId,
                    PatientId = patientId
                };

                _context.Conversations.Add(conversation);
                await _context.SaveChangesAsync();
            }

            return Ok(new { message = "Chat setup completed successfully", conversationId = conversation.ConversationId });
        }

        // ---------------- Send Message ----------------
        [HttpPost("SendMSG")]
        public async Task<IActionResult> SendMSG([FromBody] SendMessageRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Message))
                return BadRequest("Invalid request");

            int patientId = 3; // default patient ID
            int doctorId = request.DoctorId; // 

            // Step 1: Find conversation
            var conversation = await _context.Conversations
                .FirstOrDefaultAsync(c => c.DoctorId == doctorId && c.PatientId == patientId);

            if (conversation == null)
            {
                return NotFound("Conversation does not exist. Please start chat first.");
            }

            // Step 2: Create message
            var newMessage = new Message
            {
                ConversationId = conversation.ConversationId,
                Message1 = request.Message,
                CreatedAt = DateTime.Now,
                //SendBy = request.SendBy  // comes from frontend ("Doctor" or "Patient")
                SendBy = 1  // comes from frontend ("Doctor" or "Patient") /////// come from the Frontend Conect With Frontend and then take id from that because i dont have implimnted the jwt
            };

            _context.Messages.Add(newMessage);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Message sent successfully", messageId = newMessage.MessageId });
        }
    }

    // DTOs
    public class StartChatRequest
    {
        public int DoctorId { get; set; }
    }

    public class SendMessageRequest
    {
        public int DoctorId { get; set; }   // taken from frontend
        public string Message { get; set; }
        public string SendBy { get; set; }  // "Doctor" or "Patient"
    }
}

