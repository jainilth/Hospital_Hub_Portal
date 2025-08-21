using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

[Table("HH_Hospital")]
public partial class HhHospital
{
    [Key]
    [Column("HospitalID")]
    public int HospitalId { get; set; }

    [StringLength(100)]
    public string HospitalName { get; set; } = null!;

    [StringLength(100)]
    public string? HospitalType { get; set; }

    [StringLength(250)]
    public string? HospitalAddress { get; set; }

    [Column("CityID")]
    public int? CityId { get; set; }

    [StringLength(20)]
    public string? HospitalContectNo { get; set; }

    [StringLength(100)]
    public string? HospitalEmail { get; set; }

    public DateOnly? EstablishedDate { get; set; }

    [Column("WebsiteURL")]
    [StringLength(200)]
    public string? WebsiteUrl { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [Column("StateID")]
    public int? StateId { get; set; }

    [ForeignKey("CityId")]
    [InverseProperty("HhHospitals")]
    public virtual HhCity? City { get; set; }

    [InverseProperty("Hospital")]
    public virtual ICollection<HhAppointment> HhAppointments { get; set; } = new List<HhAppointment>();

    [InverseProperty("Hospital")]
    public virtual ICollection<HhDepartment> HhDepartments { get; set; } = new List<HhDepartment>();

    [InverseProperty("Hospital")]
    public virtual ICollection<HhDoctor> HhDoctors { get; set; } = new List<HhDoctor>();

    [InverseProperty("Hospital")]
    public virtual ICollection<HhEmergency> HhEmergencies { get; set; } = new List<HhEmergency>();

    [InverseProperty("Hospital")]
    public virtual ICollection<HhHospitalReview> HhHospitalReviews { get; set; } = new List<HhHospitalReview>();

    [InverseProperty("Hospital")]
    public virtual ICollection<HhLabBooking> HhLabBookings { get; set; } = new List<HhLabBooking>();

    [InverseProperty("Hospital")]
    public virtual ICollection<HhLabTest> HhLabTests { get; set; } = new List<HhLabTest>();

    [ForeignKey("StateId")]
    [InverseProperty("HhHospitals")]
    public virtual HhState? State { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("HhHospitals")]
    public virtual HhUser? User { get; set; }
}
