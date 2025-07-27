using Microsoft.AspNetCore.Http;

namespace Hospital_Hub_Portal.Models
{
    public class DoctorWithPhotoDto
    {
        public string DoctorName { get; set; }
        //public string DoctorPhotoUrl { get; set; } // optional, will be set after upload
        public decimal? ConsultationFee { get; set; }
        public string DoctorEmail { get; set; }
        public string DoctorContectNo { get; set; }
        public string DoctorGender { get; set; }
        public int? SpecializationId { get; set; }
        public int? DepartmentId { get; set; }
        public int? HospitalId { get; set; }
        public int? DoctorExperienceYears { get; set; }
        public decimal? Rating { get; set; }
        public int? UserId { get; set; }
        public string? Qualification { get; set; }
        public IFormFile ProfilePhoto { get; set; }
    }
} 