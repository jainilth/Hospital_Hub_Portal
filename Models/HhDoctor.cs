using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace Hospital_Hub_Portal.Models;

public partial class HhDoctor
{
    public int DoctorId { get; set; }

    public string? DoctorName { get; set; }

    public string? DoctorPhotoUrl { get; set; }

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

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Qualification { get; set; }

    public int? DoctorCityId { get; set; }
    public int? DoctorStateId { get; set; }
    public int? DoctorCountryId { get; set; }
    public TimeSpan? StartWorkTime { get; set; }
    public TimeSpan? EndWorkTime { get; set; }
    public int? TotalPatient { get; set; }
    public string? DoctorAddress { get; set; }
    public string? AvailabilityStatus { get; set; }

    [JsonIgnore]
    public virtual HhDepartment? Department { get; set; }

    [JsonIgnore]
    public virtual ICollection<HhAppointment> HhAppointments { get; set; } = new List<HhAppointment>();

    [JsonIgnore]
    public virtual ICollection<HhDoctorAvailableTimeSlot> HhDoctorAvailableTimeSlots { get; set; } = new List<HhDoctorAvailableTimeSlot>();

    [JsonIgnore]
    public virtual ICollection<HhDoctorReview> HhDoctorReviews { get; set; } = new List<HhDoctorReview>();

    [JsonIgnore]
    public virtual ICollection<HhPayment> HhPayments { get; set; } = new List<HhPayment>();

    [JsonIgnore]
    public virtual HhHospital? Hospital { get; set; }

    [JsonIgnore]
    public virtual HhSpecialization? Specialization { get; set; }

    [JsonIgnore]
    public virtual HhUser? User { get; set; }
}
