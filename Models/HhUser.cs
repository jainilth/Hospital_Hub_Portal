using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

[Table("HH_User")]
public partial class HhUser
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(100)]
    public string UserName { get; set; } = null!;

    [StringLength(100)]
    public string UserEmail { get; set; } = null!;

    [StringLength(20)]
    public string? UserContactNo { get; set; }

    [StringLength(20)]
    public string? UserRole { get; set; }

    [StringLength(200)]
    public string UserPassword { get; set; } = null!;

    public bool? IsAdmin { get; set; }

    public bool? IsHospital { get; set; }

    public bool? IsLab { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [InverseProperty("Patient")]
    [JsonIgnore]
    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

    [InverseProperty("User")]
    [JsonIgnore]
    public virtual ICollection<HhAppointmentCancellationLog> HhAppointmentCancellationLogs { get; set; } = new List<HhAppointmentCancellationLog>();

    [InverseProperty("User")]
    [JsonIgnore]
    public virtual ICollection<HhAppointment> HhAppointments { get; set; } = new List<HhAppointment>();

    [InverseProperty("User")]
    [JsonIgnore]
    public virtual ICollection<HhDepartment> HhDepartments { get; set; } = new List<HhDepartment>();

    [InverseProperty("User")]
    [JsonIgnore]
    public virtual ICollection<HhDoctorAvailableTimeSlot> HhDoctorAvailableTimeSlots { get; set; } = new List<HhDoctorAvailableTimeSlot>();

    [InverseProperty("User")]
    [JsonIgnore]
    public virtual ICollection<HhDoctorReview> HhDoctorReviews { get; set; } = new List<HhDoctorReview>();

    [InverseProperty("User")]
    [JsonIgnore]
    public virtual ICollection<HhDoctor> HhDoctors { get; set; } = new List<HhDoctor>();

    [InverseProperty("User")]
    [JsonIgnore]
    public virtual ICollection<HhEmergency> HhEmergencies { get; set; } = new List<HhEmergency>();

    [InverseProperty("User")]
    [JsonIgnore]
    public virtual ICollection<HhHospitalReview> HhHospitalReviews { get; set; } = new List<HhHospitalReview>();

    [InverseProperty("User")]
    [JsonIgnore]
    public virtual ICollection<HhHospital> HhHospitals { get; set; } = new List<HhHospital>();

    [InverseProperty("User")]
    [JsonIgnore]
    public virtual ICollection<HhLabBooking> HhLabBookings { get; set; } = new List<HhLabBooking>();

    [InverseProperty("UploadedByNavigation")]
    [JsonIgnore]
    public virtual ICollection<HhLabReport> HhLabReports { get; set; } = new List<HhLabReport>();

    [InverseProperty("CreatedByNavigation")]
    [JsonIgnore]
    public virtual ICollection<HhLabTest> HhLabTests { get; set; } = new List<HhLabTest>();

    [InverseProperty("User")]
    [JsonIgnore]
    public virtual ICollection<HhMedicineCategory> HhMedicineCategories { get; set; } = new List<HhMedicineCategory>();

    [InverseProperty("User")]
    [JsonIgnore]
    public virtual ICollection<HhMedicineUnit> HhMedicineUnits { get; set; } = new List<HhMedicineUnit>();

    [InverseProperty("User")]
    [JsonIgnore]
    public virtual ICollection<HhPayment> HhPayments { get; set; } = new List<HhPayment>();

    [InverseProperty("User")]
    [JsonIgnore]
    public virtual ICollection<HhSpecialization> HhSpecializations { get; set; } = new List<HhSpecialization>();

}
