using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

[Table("HH_Doctor")]
public partial class HhDoctor
{
    [Key]
    [Column("DoctorID")]
    public int DoctorId { get; set; }

    [StringLength(100)]
    public string? DoctorName { get; set; }

    [Column("DoctorPhotoURL")]
    [StringLength(200)]
    public string? DoctorPhotoUrl { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? ConsultationFee { get; set; }

    [StringLength(100)]
    public string? DoctorEmail { get; set; }

    [StringLength(20)]
    public string? DoctorContectNo { get; set; }

    [StringLength(10)]
    public string? DoctorGender { get; set; }

    [Column("SpecializationID")]
    public int? SpecializationId { get; set; }

    [Column("DepartmentID")]
    public int? DepartmentId { get; set; }

    [Column("HospitalID")]
    public int? HospitalId { get; set; }

    public int? DoctorExperienceYears { get; set; }

    [Column(TypeName = "decimal(2, 1)")]
    public decimal? Rating { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Qualification { get; set; }

    [StringLength(30)]
    public string? AvailabilityStatus { get; set; }

    public TimeOnly? StartWorkTime { get; set; }

    public TimeOnly? EndWorkTime { get; set; }

    public int? TotalPatient { get; set; }

    [StringLength(200)]
    public string? DoctorAddress { get; set; }

    public int? DoctorCityId { get; set; }

    public int? DoctorStateId { get; set; }

    public int? DoctorCountryId { get; set; }

    [InverseProperty("Doctor")]
    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

    [ForeignKey("DepartmentId")]
    [InverseProperty("HhDoctors")]
    public virtual HhDepartment? Department { get; set; }

    [ForeignKey("DoctorCityId")]
    [InverseProperty("HhDoctors")]
    public virtual HhCity? DoctorCity { get; set; }

    [ForeignKey("DoctorCountryId")]
    [InverseProperty("HhDoctors")]
    public virtual HhCountry? DoctorCountry { get; set; }

    [ForeignKey("DoctorStateId")]
    [InverseProperty("HhDoctors")]
    public virtual HhState? DoctorState { get; set; }

    [InverseProperty("Doctor")]
    public virtual ICollection<DoctorTimeSlot> DoctorTimeSlots { get; set; } = new List<DoctorTimeSlot>();

    [InverseProperty("Doctor")]
    public virtual ICollection<HhAppointment> HhAppointments { get; set; } = new List<HhAppointment>();

    [InverseProperty("Doctor")]
    public virtual ICollection<HhDoctorAvailableTimeSlot> HhDoctorAvailableTimeSlots { get; set; } = new List<HhDoctorAvailableTimeSlot>();

    [InverseProperty("Doctor")]
    public virtual ICollection<HhDoctorReview> HhDoctorReviews { get; set; } = new List<HhDoctorReview>();

    [InverseProperty("Doctor")]
    public virtual ICollection<HhPayment> HhPayments { get; set; } = new List<HhPayment>();

    [ForeignKey("HospitalId")]
    [InverseProperty("HhDoctors")]
    public virtual HhHospital? Hospital { get; set; }

    [ForeignKey("SpecializationId")]
    [InverseProperty("HhDoctors")]
    public virtual HhSpecialization? Specialization { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("HhDoctors")]
    public virtual HhUser? User { get; set; }

    [InverseProperty("ChatUser")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
