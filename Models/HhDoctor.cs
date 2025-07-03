using System;
using System.Collections.Generic;

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

    public virtual HhDepartment? Department { get; set; }

    public virtual ICollection<HhAppointment> HhAppointments { get; set; } = new List<HhAppointment>();

    public virtual ICollection<HhDoctorAvailableTimeSlot> HhDoctorAvailableTimeSlots { get; set; } = new List<HhDoctorAvailableTimeSlot>();

    public virtual ICollection<HhDoctorReview> HhDoctorReviews { get; set; } = new List<HhDoctorReview>();

    public virtual ICollection<HhPayment> HhPayments { get; set; } = new List<HhPayment>();

    public virtual HhHospital? Hospital { get; set; }

    public virtual HhSpecialization? Specialization { get; set; }

    public virtual HhUser? User { get; set; }
}
