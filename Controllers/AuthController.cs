using Hospital_Hub_Portal.Models;
using Hospital_Hub_Portal.Models.Auth;
using Hospital_Hub_Portal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Hub_Portal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly HospitalHubContext _context;
        private readonly TokenService _tokenService;
        private readonly PasswordService _passwordService;

        public AuthController(HospitalHubContext context, TokenService tokenService, PasswordService passwordService)
        {
            _context = context;
            _tokenService = tokenService;
            _passwordService = passwordService;
        }

        // ✅ Test endpoint to verify API is working
        [AllowAnonymous]
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok(new { Message = "Auth API is working!", Timestamp = DateTime.UtcNow });
        }

        // ✅ Register endpoint
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            try
            {
                // Validate input
                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Name))
                {
                    return BadRequest(new { Message = "All fields are required" });
                }

                // Validate email format
                if (!new EmailAddressAttribute().IsValid(request.Email))
                {
                    return BadRequest(new { Message = "Invalid email format" });
                }

                // Validate password strength
                if (request.Password.Length < 6)
                {
                    return BadRequest(new { Message = "Password must be at least 6 characters long" });
                }

                // Check if user already exists
                if (_context.HhUsers.Any(u => u.UserEmail.ToLower() == request.Email.ToLower()))
                {
                    return BadRequest(new { Message = "User with this email already exists" });
                }

                // Hash password with BCrypt
                var hashedPassword = _passwordService.HashPassword(request.Password);

                var user = new HhUser
                {
                    UserName = request.Name.Trim(),
                    UserEmail = request.Email.Trim(),
                    UserPassword = hashedPassword,
                    UserRole = string.IsNullOrEmpty(request.Role) ? "User" : request.Role,
                    CreatedDate = DateTime.UtcNow
                };

                _context.HhUsers.Add(user);
                _context.SaveChanges();

                return Ok(new { Message = "User registered successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error", Error = ex.Message });
            }
        }

        // ✅ Login endpoint
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                // Validate input
                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                {
                    return BadRequest(new { Message = "Email and password are required" });
                }

                var email = request.Email?.Trim().ToLower();
                var user = _context.HhUsers.FirstOrDefault(u => u.UserEmail.ToLower() == email);
                if (user == null)
                {
                    return Unauthorized(new { Message = "Invalid email or password" });
                }

                // Verify password with BCrypt
                var passwordValid = _passwordService.VerifyPassword(request.Password, user.UserPassword);
                
                // Legacy fallback: if stored password is plaintext or SHA256 and matches input, rehash with BCrypt
                if (!passwordValid && (user.UserPassword == request.Password || user.UserPassword == Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(request.Password)))))
                {
                    user.UserPassword = _passwordService.HashPassword(request.Password);
                    _context.HhUsers.Update(user);
                    _context.SaveChanges();
                    passwordValid = true;
                }


                var requestRole = user.UserRole;

                if (!passwordValid)
                {
                    return Unauthorized(new { Message = "Invalid email or password" });
                }

                // Generate JWT token
                var token = _tokenService.CreateAccessToken(user);

                var safeRole = string.IsNullOrWhiteSpace(user.UserRole) ? "User" : user.UserRole;

                return Ok(new
                {
                    Token = token,
                    User = new
                    {
                        user.UserId,
                        user.UserName,
                        user.UserEmail,
                        UserRole = safeRole
                    },
                    Message = "Login successful"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error", Error = ex.Message });
            }
        }

        // ✅ Validate token endpoint
        [Authorize]
        [HttpGet("validate")]
        public IActionResult ValidateToken()
        {
            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { Message = "Invalid token" });
                }

                var user = _context.HhUsers.FirstOrDefault(u => u.UserId.ToString() == userId);
                if (user == null)
                {
                    return Unauthorized(new { Message = "User not found" });
                }

                return Ok(new
                {
                    User = new
                    {
                        user.UserId,
                        user.UserName,
                        user.UserEmail,
                        user.UserRole
                    },
                    Message = "Token is valid"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error", Error = ex.Message });
            }
        }

        // ✅ Get current user info (protected endpoint)
        [Authorize]
        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { Message = "Not authenticated" });
                }

                var user = _context.HhUsers.FirstOrDefault(u => u.UserId.ToString() == userId);
                if (user == null)
                {
                    return NotFound(new { Message = "User not found" });
                }

                return Ok(new
                {
                    User = new
                    {
                        user.UserId,
                        user.UserName,
                        user.UserEmail,
                        user.UserRole,
                        user.CreatedDate
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error", Error = ex.Message });
            }
        }
    }
}
