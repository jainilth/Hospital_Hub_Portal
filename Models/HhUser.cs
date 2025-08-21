using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

    [InverseProperty("User")]
    public virtual ICollection<HhAppointmentCancellationLog> HhAppointmentCancellationLogs { get; set; } = new List<HhAppointmentCancellationLog>();

    [InverseProperty("User")]
    public virtual ICollection<HhAppointment> HhAppointments { get; set; } = new List<HhAppointment>();

    [InverseProperty("User")]
    public virtual ICollection<HhDepartment> HhDepartments { get; set; } = new List<HhDepartment>();

    [InverseProperty("User")]
    public virtual ICollection<HhDoctorAvailableTimeSlot> HhDoctorAvailableTimeSlots { get; set; } = new List<HhDoctorAvailableTimeSlot>();

    [InverseProperty("User")]
    public virtual ICollection<HhDoctorReview> HhDoctorReviews { get; set; } = new List<HhDoctorReview>();

    [InverseProperty("User")]
    public virtual ICollection<HhDoctor> HhDoctors { get; set; } = new List<HhDoctor>();

    [InverseProperty("User")]
    public virtual ICollection<HhEmergency> HhEmergencies { get; set; } = new List<HhEmergency>();

    [InverseProperty("User")]
    public virtual ICollection<HhHospitalReview> HhHospitalReviews { get; set; } = new List<HhHospitalReview>();

    [InverseProperty("User")]
    public virtual ICollection<HhHospital> HhHospitals { get; set; } = new List<HhHospital>();

    [InverseProperty("User")]
    public virtual ICollection<HhLabBooking> HhLabBookings { get; set; } = new List<HhLabBooking>();

    [InverseProperty("UploadedByNavigation")]
    public virtual ICollection<HhLabReport> HhLabReports { get; set; } = new List<HhLabReport>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<HhLabTest> HhLabTests { get; set; } = new List<HhLabTest>();

    [InverseProperty("User")]
    public virtual ICollection<HhMedicineCategory> HhMedicineCategories { get; set; } = new List<HhMedicineCategory>();

    [InverseProperty("User")]
    public virtual ICollection<HhMedicineUnit> HhMedicineUnits { get; set; } = new List<HhMedicineUnit>();

    [InverseProperty("User")]
    public virtual ICollection<HhPayment> HhPayments { get; set; } = new List<HhPayment>();

    [InverseProperty("User")]
    public virtual ICollection<HhSpecialization> HhSpecializations { get; set; } = new List<HhSpecialization>();

}
