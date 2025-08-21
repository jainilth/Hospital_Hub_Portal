using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

[Table("HH_LabReport")]
public partial class HhLabReport
{
    [Key]
    [Column("ReportID")]
    public int ReportId { get; set; }

    [Column("BookingID")]
    public int? BookingId { get; set; }

    public string? TestResultSummary { get; set; }

    [Column("ReportFileURL")]
    [StringLength(200)]
    public string? ReportFileUrl { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UploadedDate { get; set; }

    public int? UploadedBy { get; set; }

    public string? Notes { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [ForeignKey("BookingId")]
    [InverseProperty("HhLabReports")]
    public virtual HhLabBooking? Booking { get; set; }

    [ForeignKey("UploadedBy")]
    [InverseProperty("HhLabReports")]
    public virtual HhUser? UploadedByNavigation { get; set; }
}
