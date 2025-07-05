using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hospital_Hub_Portal.Models;

public partial class HhUser
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string? UserContactNo { get; set; }

    public string? UserRole { get; set; }

    public string UserPassword { get; set; } = null!;

    public bool? IsAdmin { get; set; }

    public bool? IsHospital { get; set; }

    public bool? IsLab { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<HhAppointmentCancellationLog> HhAppointmentCancellationLogs { get; set; } = new List<HhAppointmentCancellationLog>();

    [JsonIgnore]
    public virtual ICollection<HhAppointment> HhAppointments { get; set; } = new List<HhAppointment>();

    [JsonIgnore]
    public virtual ICollection<HhDepartment> HhDepartments { get; set; } = new List<HhDepartment>();

    [JsonIgnore]
    public virtual ICollection<HhDoctorAvailableTimeSlot> HhDoctorAvailableTimeSlots { get; set; } = new List<HhDoctorAvailableTimeSlot>();

    [JsonIgnore]
    public virtual ICollection<HhDoctorReview> HhDoctorReviews { get; set; } = new List<HhDoctorReview>();

    [JsonIgnore]
    public virtual ICollection<HhDoctor> HhDoctors { get; set; } = new List<HhDoctor>();

    [JsonIgnore]
    public virtual ICollection<HhEmergency> HhEmergencies { get; set; } = new List<HhEmergency>();

    [JsonIgnore]
    public virtual ICollection<HhHospitalReview> HhHospitalReviews { get; set; } = new List<HhHospitalReview>();

    [JsonIgnore]
    public virtual ICollection<HhHospital> HhHospitals { get; set; } = new List<HhHospital>();

    [JsonIgnore]
    public virtual ICollection<HhLabBooking> HhLabBookings { get; set; } = new List<HhLabBooking>();

    [JsonIgnore]
    public virtual ICollection<HhLabReport> HhLabReports { get; set; } = new List<HhLabReport>();

    [JsonIgnore]
    public virtual ICollection<HhLabTest> HhLabTests { get; set; } = new List<HhLabTest>();

    [JsonIgnore]
    public virtual ICollection<HhMedicineCategory> HhMedicineCategories { get; set; } = new List<HhMedicineCategory>();

    [JsonIgnore]
    public virtual ICollection<HhMedicineUnit> HhMedicineUnits { get; set; } = new List<HhMedicineUnit>();

    [JsonIgnore]
    public virtual ICollection<HhPayment> HhPayments { get; set; } = new List<HhPayment>();

    [JsonIgnore]
    public virtual ICollection<HhSpecialization> HhSpecializations { get; set; } = new List<HhSpecialization>();
}
