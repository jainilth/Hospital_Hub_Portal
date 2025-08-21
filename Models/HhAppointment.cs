using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

[Table("HH_Appointment")]
public partial class HhAppointment
{
    [Key]
    [Column("AppointmentID")]
    public int AppointmentId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column("DoctorID")]
    public int? DoctorId { get; set; }

    [Column("HospitalID")]
    public int? HospitalId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AppointmentDateTime { get; set; }

    [StringLength(20)]
    public string? AppointmentStatus { get; set; }

    public string? Symptoms { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AppointmentCreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AppointmentDateGivenToPatient { get; set; }

    public string? Notes { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [ForeignKey("DoctorId")]
    [InverseProperty("HhAppointments")]
    public virtual HhDoctor? Doctor { get; set; }

    [InverseProperty("Appointment")]
    public virtual ICollection<HhAppointmentCancellationLog> HhAppointmentCancellationLogs { get; set; } = new List<HhAppointmentCancellationLog>();

    [InverseProperty("Appointment")]
    public virtual ICollection<HhPayment> HhPayments { get; set; } = new List<HhPayment>();

    [ForeignKey("HospitalId")]
    [InverseProperty("HhAppointments")]
    public virtual HhHospital? Hospital { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("HhAppointments")]
    public virtual HhUser? User { get; set; }
}
