using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        //[HttpGet]
        //public IActionResult GetAllDoctors()
        //{
        //    var doctors = _context.HhDoctors.ToList();
        //    return Ok(doctors);
        //}

        #region GetAllDoctors
        [HttpGet]
        public IActionResult GetAllDoctors()
        {
            var doctors = _context.HhDoctors
                .Include(d => d.Specialization) // join specialization
                .Select(d => new
                {
                    d.DoctorId,
                    d.DoctorName,
                    d.DoctorPhotoUrl,
                    d.ConsultationFee,
                    d.DoctorEmail,
                    d.DoctorContectNo,
                    d.DoctorGender,
                    d.DepartmentId,
                    d.HospitalId,
                    d.DoctorExperienceYears,
                    d.Rating,
                    d.UserId,
                    d.CreatedDate,
                    d.ModifiedDate,
                    d.Qualification,
                    d.DoctorAddress,
                    d.AvailabilityStatus,

                    // Specialization info
                    SpecializationId = d.SpecializationId,
                    SpecializationName = d.Specialization.SpecializationName
                })
                .ToList();

            return Ok(doctors);
        }
        #endregion

        #region GetDoctorsWithPagination
        [HttpGet]
        public IActionResult GetDoctorsWithPagination(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string searchTerm = "",
            [FromQuery] string specializationFilter = "",
            [FromQuery] string statusFilter = "",
            [FromQuery] string sortBy = "DoctorName",
            [FromQuery] string sortDirection = "asc")
        {
            try
            {
                var query = _context.HhDoctors
                    .Include(d => d.Specialization)
                    .AsQueryable();

                // Apply search filter
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(d => 
                        d.DoctorName.Contains(searchTerm) ||
                        d.DoctorEmail.Contains(searchTerm) ||
                        d.DoctorContectNo.Contains(searchTerm) ||
                        d.Qualification.Contains(searchTerm) ||
                        (d.Specialization != null && d.Specialization.SpecializationName.Contains(searchTerm))
                    );
                }

                // Apply specialization filter
                if (!string.IsNullOrEmpty(specializationFilter))
                {
                    query = query.Where(d => d.Specialization != null && 
                        d.Specialization.SpecializationName == specializationFilter);
                }

                // Apply status filter
                if (!string.IsNullOrEmpty(statusFilter))
                {
                    query = query.Where(d => d.AvailabilityStatus == statusFilter);
                }

                // Apply sorting
                switch (sortBy.ToLower())
                {
                    case "doctorname":
                        query = sortDirection.ToLower() == "desc" ? 
                            query.OrderByDescending(d => d.DoctorName) : 
                            query.OrderBy(d => d.DoctorName);
                        break;
                    case "specialization":
                        query = sortDirection.ToLower() == "desc" ? 
                            query.OrderByDescending(d => d.Specialization.SpecializationName) : 
                            query.OrderBy(d => d.Specialization.SpecializationName);
                        break;
                    case "experience":
                        query = sortDirection.ToLower() == "desc" ? 
                            query.OrderByDescending(d => d.DoctorExperienceYears) : 
                            query.OrderBy(d => d.DoctorExperienceYears);
                        break;
                    case "rating":
                        query = sortDirection.ToLower() == "desc" ? 
                            query.OrderByDescending(d => d.Rating) : 
                            query.OrderBy(d => d.Rating);
                        break;
                    case "consultationfee":
                        query = sortDirection.ToLower() == "desc" ? 
                            query.OrderByDescending(d => d.ConsultationFee) : 
                            query.OrderBy(d => d.ConsultationFee);
                        break;
                    default:
                        query = query.OrderBy(d => d.DoctorName);
                        break;
                }

                // Get total count before pagination
                int totalRecords = query.Count();

                // Apply pagination
                var pagedDoctors = query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(d => new
                    {
                        d.DoctorId,
                        d.DoctorName,
                        d.DoctorPhotoUrl,
                        d.ConsultationFee,
                        d.DoctorEmail,
                        d.DoctorContectNo,
                        d.DoctorGender,
                        d.DepartmentId,
                        d.HospitalId,
                        d.DoctorExperienceYears,
                        d.Rating,
                        d.UserId,
                        d.CreatedDate,
                        d.ModifiedDate,
                        d.Qualification,
                        d.DoctorAddress,
                        d.AvailabilityStatus,
                        d.TotalPatient,
                        SpecializationId = d.SpecializationId,
                        SpecializationName = d.Specialization != null ? d.Specialization.SpecializationName : null
                    })
                    .ToList();

                var result = new PagedResult<object>
                {
                    Data = pagedDoctors.Cast<object>().ToList(),
                    TotalRecords = totalRecords,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving doctors: {ex.Message}");
            }
        }
        #endregion

        #region GetDoctorsBySpecialization
        // GET: api/Doctor/GetDoctorsBySpecialization/{specializationId}
        [HttpGet("{specializationId}")]
        public IActionResult GetDoctorsBySpecialization(int specializationId)
        {
            var doctors = _context.HhDoctors
                .Where(d => d.SpecializationId == specializationId)
                .Select(d => new DoctorListItemDto
                {
                    DoctorId = d.DoctorId,
                    DoctorName = d.DoctorName,
                    DoctorPhotoUrl = d.DoctorPhotoUrl,
                    ConsultationFee = d.ConsultationFee,
                    DoctorEmail = d.DoctorEmail,
                    DoctorContectNo = d.DoctorContectNo,
                    DoctorGender = d.DoctorGender,
                    SpecializationId = d.SpecializationId,
                    SpecializationName = d.Specialization != null ? d.Specialization.SpecializationName : null,
                    HospitalId = d.HospitalId,
                    HospitalName = d.Hospital != null ? d.Hospital.HospitalName : null,
                    DoctorExperienceYears = d.DoctorExperienceYears,
                    Rating = d.Rating,
                    Qualification = d.Qualification,
                    AvailabilityStatus = d.AvailabilityStatus,
                    DoctorAddress = d.DoctorAddress,
                    TotalPatient = d.TotalPatient
                })
                .ToList();

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
               DoctorCityId = dto.DoctorCityId,
               DoctorStateId = dto.DoctorStateId,
               DoctorCountryId = dto.DoctorCountryId,
               StartWorkTime = dto.StartWorkTime.HasValue ? TimeOnly.FromTimeSpan(dto.StartWorkTime.Value) : null,
               EndWorkTime = dto.EndWorkTime.HasValue ? TimeOnly.FromTimeSpan(dto.EndWorkTime.Value) : null,
               TotalPatient = dto.TotalPatient,
               DoctorAddress = dto.DoctorAddress,
               AvailabilityStatus = dto.AvailabilityStatus,
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
        public async Task<IActionResult> UpdateDoctor(int id, [FromForm] DoctorWithPhotoDto dto)
        {
            try
            {
                var existingDoctor = await _context.HhDoctors.FindAsync(id);
                if (existingDoctor == null)
                {
                    return NotFound(new { message = "Doctor not found" });
                }

                // Update basic properties
                existingDoctor.DoctorName = dto.DoctorName ?? existingDoctor.DoctorName;
                existingDoctor.ConsultationFee = dto.ConsultationFee ?? existingDoctor.ConsultationFee;
                existingDoctor.DoctorEmail = dto.DoctorEmail ?? existingDoctor.DoctorEmail;
                existingDoctor.DoctorContectNo = dto.DoctorContectNo ?? existingDoctor.DoctorContectNo;
                existingDoctor.DoctorGender = dto.DoctorGender ?? existingDoctor.DoctorGender;
                existingDoctor.SpecializationId = dto.SpecializationId ?? existingDoctor.SpecializationId;
                existingDoctor.DepartmentId = dto.DepartmentId ?? existingDoctor.DepartmentId;
                existingDoctor.HospitalId = dto.HospitalId ?? existingDoctor.HospitalId;
                existingDoctor.DoctorExperienceYears = dto.DoctorExperienceYears ?? existingDoctor.DoctorExperienceYears;
                existingDoctor.Rating = dto.Rating ?? existingDoctor.Rating;
                existingDoctor.UserId = dto.UserId ?? existingDoctor.UserId;
                existingDoctor.Qualification = dto.Qualification ?? existingDoctor.Qualification;
                existingDoctor.DoctorCityId = dto.DoctorCityId ?? existingDoctor.DoctorCityId;
                existingDoctor.DoctorStateId = dto.DoctorStateId ?? existingDoctor.DoctorStateId;
                existingDoctor.DoctorCountryId = dto.DoctorCountryId ?? existingDoctor.DoctorCountryId;
                existingDoctor.TotalPatient = dto.TotalPatient ?? existingDoctor.TotalPatient;
                existingDoctor.DoctorAddress = dto.DoctorAddress ?? existingDoctor.DoctorAddress;
                existingDoctor.AvailabilityStatus = dto.AvailabilityStatus ?? existingDoctor.AvailabilityStatus;
                
                // Handle time fields
                if (dto.StartWorkTime.HasValue)
                {
                    existingDoctor.StartWorkTime = TimeOnly.FromTimeSpan(dto.StartWorkTime.Value);
                }
                if (dto.EndWorkTime.HasValue)
                {
                    existingDoctor.EndWorkTime = TimeOnly.FromTimeSpan(dto.EndWorkTime.Value);
                }
                
                // Log fields that aren't currently stored in database
                if (!string.IsNullOrEmpty(dto.languages))
                {
                    // TODO: Future enhancement - add languages field to database
                    Console.WriteLine($"Languages field received but not stored: {dto.languages}");
                }
                if (!string.IsNullOrEmpty(dto.nextAvailable))
                {
                    // TODO: Future enhancement - add nextAvailable field to database
                    Console.WriteLine($"NextAvailable field received but not stored: {dto.nextAvailable}");
                }

                // Handle profile photo upload
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
                        {
                            return BadRequest(new { message = "Image upload failed: " + uploadResult.Error?.Message });
                        }

                        existingDoctor.DoctorPhotoUrl = uploadResult.SecureUrl.ToString();
                    }
                }

                existingDoctor.ModifiedDate = DateTime.Now;

                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Doctor updated successfully",
                    doctorId = existingDoctor.DoctorId,
                    photoUrl = existingDoctor.DoctorPhotoUrl
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error updating doctor: " + ex.Message });
            }
        }
        #endregion

        #region GetDoctorStatistics
        [HttpGet]
        public IActionResult GetDoctorStatistics()
        {
            try
            {
                var totalDoctors = _context.HhDoctors.Count();
                
                // Handle case-insensitive and variations of availability status
                var availableDoctors = _context.HhDoctors.Count(d => 
                    !string.IsNullOrEmpty(d.AvailabilityStatus) && 
                    d.AvailabilityStatus.ToLower().Contains("available"));
                    
                var busyDoctors = _context.HhDoctors.Count(d => 
                    !string.IsNullOrEmpty(d.AvailabilityStatus) && 
                    d.AvailabilityStatus.ToLower().Contains("busy"));
                    
                var onLeaveDoctors = _context.HhDoctors.Count(d => 
                    !string.IsNullOrEmpty(d.AvailabilityStatus) && 
                    (d.AvailabilityStatus.ToLower().Contains("leave") || 
                     d.AvailabilityStatus.ToLower().Contains("unavailable")));
                
                // Count doctors with no status or invalid status
                var noStatusDoctors = _context.HhDoctors.Count(d => 
                    string.IsNullOrEmpty(d.AvailabilityStatus) || 
                    d.AvailabilityStatus.ToLower() == "string");

                var statistics = new
                {
                    totalDoctors = totalDoctors,
                    availableNow = availableDoctors,
                    busy = busyDoctors,
                    onLeave = onLeaveDoctors,
                    noStatus = noStatusDoctors // For debugging
                };

                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error retrieving doctor statistics: " + ex.Message });
            }
        }
        #endregion

        #region NormalizeDoctorStatuses
        [HttpPost]
        public async Task<IActionResult> NormalizeDoctorStatuses()
        {
            try
            {
                var doctors = await _context.HhDoctors.ToListAsync();
                int updatedCount = 0;

                foreach (var doctor in doctors)
                {
                    string normalizedStatus = null;
                    
                    if (!string.IsNullOrEmpty(doctor.AvailabilityStatus))
                    {
                        var status = doctor.AvailabilityStatus.ToLower().Trim();
                        
                        if (status.Contains("available"))
                        {
                            normalizedStatus = "Available";
                        }
                        else if (status.Contains("busy"))
                        {
                            normalizedStatus = "Busy";
                        }
                        else if (status.Contains("leave") || status.Contains("unavailable"))
                        {
                            normalizedStatus = "On Leave";
                        }
                        else if (status == "string" || status == "")
                        {
                            normalizedStatus = "Available"; // Default for invalid entries
                        }
                        else
                        {
                            normalizedStatus = "Available"; // Default for unrecognized entries
                        }
                    }
                    else
                    {
                        normalizedStatus = "Available"; // Default for null/empty entries
                    }

                    if (doctor.AvailabilityStatus != normalizedStatus)
                    {
                        doctor.AvailabilityStatus = normalizedStatus;
                        doctor.ModifiedDate = DateTime.Now;
                        updatedCount++;
                    }
                }

                await _context.SaveChangesAsync();

                return Ok(new 
                {
                    message = "Doctor availability statuses normalized successfully",
                    updatedCount = updatedCount,
                    totalDoctors = doctors.Count
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error normalizing doctor statuses: " + ex.Message });
            }
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
