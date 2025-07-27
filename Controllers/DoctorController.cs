using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoctorController : Controller
    {
        private readonly HospitalHubContext _context;
        private readonly Cloudinary _cloudinary;

        // Inject Cloudinary and your DbContext via DI
        public DoctorController(HospitalHubContext context, Cloudinary cloudinary)
        {
            _context = context;
            _cloudinary = cloudinary;
        }

        #region GetAllDoctors
        [HttpGet]
        public IActionResult GetAllDoctors()
        {
            var doctors = _context.HhDoctors.ToList();
            return Ok(doctors);
        }
        #endregion

        #region GetDoctorById
        [HttpGet("{id}")]
        public IActionResult GetDoctorById(int id)
        {
            var doctor = _context.HhDoctors.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return Ok(doctor);
        }
        #endregion

        #region AddDoctor
        [HttpPost]
        public async Task<IActionResult> AddDoctor([FromForm] DoctorWithPhotoDto dto)
        {
           var doctor = new HhDoctor
           {
               DoctorName = dto.DoctorName,
               ConsultationFee = dto.ConsultationFee,
               DoctorEmail = dto.DoctorEmail,
               DoctorContectNo = dto.DoctorContectNo,
               DoctorGender = dto.DoctorGender,
               SpecializationId = dto.SpecializationId,
               DepartmentId = dto.DepartmentId,
               HospitalId = dto.HospitalId,
               DoctorExperienceYears = dto.DoctorExperienceYears,
               Rating = dto.Rating,
               UserId = dto.UserId,
               Qualification = dto.Qualification,
               CreatedDate = DateTime.Now,
               ModifiedDate = DateTime.Now
           };

           if (dto.ProfilePhoto != null && dto.ProfilePhoto.Length > 0)
           {
               using (var stream = dto.ProfilePhoto.OpenReadStream())
               {
                   var uploadParams = new ImageUploadParams
                   {
                       File = new FileDescription(dto.ProfilePhoto.FileName, stream)
                   };

                   var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                   if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
                       return BadRequest("Image upload failed: " + uploadResult.Error?.Message);

                   doctor.DoctorPhotoUrl = uploadResult.SecureUrl.ToString();
               }
           }

           _context.HhDoctors.Add(doctor);
           await _context.SaveChangesAsync();

           return Ok(new
           {
               message = "Doctor added successfully",
               doctorId = doctor.DoctorId,
               photoUrl = doctor.DoctorPhotoUrl
           });
        }
        #endregion

        #region UpdateDoctor
        [HttpPut("{id}")]
        public IActionResult UpdateDoctor(int id, [FromBody] HhDoctor hhDoctor)
        {
            if (hhDoctor == null || hhDoctor.DoctorId != id)
            {
                return BadRequest("Doctor data is null or ID mismatch");
            }
            var existingDoctor = _context.HhDoctors.Find(id);
            if (existingDoctor == null)
            {
                return NotFound();
            }
            existingDoctor.DoctorName = hhDoctor.DoctorName;
            existingDoctor.DoctorPhotoUrl = hhDoctor.DoctorPhotoUrl;
            existingDoctor.ConsultationFee = hhDoctor.ConsultationFee;
            existingDoctor.DoctorEmail = hhDoctor.DoctorEmail;
            existingDoctor.DoctorContectNo = hhDoctor.DoctorContectNo;
            existingDoctor.DoctorGender = hhDoctor.DoctorGender;
            existingDoctor.SpecializationId = hhDoctor.SpecializationId;
            existingDoctor.DepartmentId = hhDoctor.DepartmentId;
            existingDoctor.HospitalId = hhDoctor.HospitalId;
            existingDoctor.DoctorExperienceYears = hhDoctor.DoctorExperienceYears;
            existingDoctor.Rating = hhDoctor.Rating;
            existingDoctor.UserId = hhDoctor.UserId;
            existingDoctor.Qualification = hhDoctor.Qualification;
            existingDoctor.ModifiedDate = DateTime.Now;
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region DeleteDoctor
        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            var doctor = _context.HhDoctors.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }
            _context.HhDoctors.Remove(doctor);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
