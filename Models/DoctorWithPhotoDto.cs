using Microsoft.AspNetCore.Http;

namespace Hospital_Hub_Portal.Models
{
    public class DoctorWithPhotoDto
    {
        public string? DoctorName { get; set; }
        //public string DoctorPhotoUrl { get; set; } // optional, will be set after upload
        public decimal? ConsultationFee { get; set; }
        public string? DoctorEmail { get; set; }
        public string? DoctorContectNo { get; set; }
        public string? DoctorGender { get; set; }
        public int? SpecializationId { get; set; }
        public int? DepartmentId { get; set; }
        public int? HospitalId { get; set; }
        public int? DoctorExperienceYears { get; set; }
        public decimal? Rating { get; set; }
        public int? UserId { get; set; }
        public string? Qualification { get; set; }
        public IFormFile? ProfilePhoto { get; set; }
        public int? DoctorCityId { get; set; }
        public int? DoctorStateId { get; set; }
        public int? DoctorCountryId { get; set; }
        public TimeSpan? StartWorkTime { get; set; }
        public TimeSpan? EndWorkTime { get; set; }
        public int? TotalPatient { get; set; }
        public string? DoctorAddress { get; set; }
        public string? AvailabilityStatus { get; set; }
        
        // Additional fields that frontend sends
        public string? languages { get; set; }
        public string? nextAvailable { get; set; }
    }
}
