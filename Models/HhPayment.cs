using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

[Table("HH_Payment")]
public partial class HhPayment
{
    [Key]
    [Column("PaymentID")]
    public int PaymentId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column("DoctorID")]
    public int? DoctorId { get; set; }

    [Column("AppointmentID")]
    public int? AppointmentId { get; set; }

    [Column("PaymentUPI_ID")]
    [StringLength(100)]
    public string? PaymentUpiId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? PaymentAmount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PaymentDate { get; set; }

    [StringLength(20)]
    public string? PaymentStatus { get; set; }

    [StringLength(50)]
    public string? PaymentMethod { get; set; }

    [StringLength(10)]
    public string? PaymentCurrency { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [ForeignKey("AppointmentId")]
    [InverseProperty("HhPayments")]
    public virtual HhAppointment? Appointment { get; set; }

    [ForeignKey("DoctorId")]
    [InverseProperty("HhPayments")]
    public virtual HhDoctor? Doctor { get; set; }

    [InverseProperty("Payment")]
    public virtual ICollection<HhLabBooking> HhLabBookings { get; set; } = new List<HhLabBooking>();

    [ForeignKey("UserId")]
    [InverseProperty("HhPayments")]
    public virtual HhUser? User { get; set; }
}
