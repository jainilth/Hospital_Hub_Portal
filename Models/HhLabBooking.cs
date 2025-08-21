using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

[Table("HH_LabBooking")]
public partial class HhLabBooking
{
    [Key]
    [Column("BookingID")]
    public int BookingId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column("TestID")]
    public int? TestId { get; set; }

    [Column("HospitalID")]
    public int? HospitalId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? BookingDateTime { get; set; }

    [StringLength(20)]
    public string? BookingStatus { get; set; }

    public string? Symptoms { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column("PaymentID")]
    public int? PaymentId { get; set; }

    public bool? IsReportReady { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [InverseProperty("Booking")]
    public virtual ICollection<HhLabReport> HhLabReports { get; set; } = new List<HhLabReport>();

    [ForeignKey("HospitalId")]
    [InverseProperty("HhLabBookings")]
    public virtual HhHospital? Hospital { get; set; }

    [ForeignKey("PaymentId")]
    [InverseProperty("HhLabBookings")]
    public virtual HhPayment? Payment { get; set; }

    [ForeignKey("TestId")]
    [InverseProperty("HhLabBookings")]
    public virtual HhLabTest? Test { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("HhLabBookings")]
    public virtual HhUser? User { get; set; }
}
