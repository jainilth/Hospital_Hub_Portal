using System;

namespace Hospital_Hub_Portal.Models
{
    public class DoctorListItemDto
    {
        public int DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public string? DoctorPhotoUrl { get; set; }
        public decimal? ConsultationFee { get; set; }
        public string? DoctorEmail { get; set; }
        public string? DoctorContectNo { get; set; }
        public string? DoctorGender { get; set; }
        public int? SpecializationId { get; set; }
        public string? SpecializationName { get; set; }
        public int? HospitalId { get; set; }
        public string? HospitalName { get; set; }
        public int? DoctorExperienceYears { get; set; }
        public decimal? Rating { get; set; }
        public string? Qualification { get; set; }
        public string? AvailabilityStatus { get; set; }
        public string? DoctorAddress { get; set; }
        public int? TotalPatient { get; set; }
    }
}


