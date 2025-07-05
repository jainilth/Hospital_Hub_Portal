using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hospital_Hub_Portal.Models;

public partial class HhLabReport
{
    public int ReportId { get; set; }

    public int? BookingId { get; set; }

    public string? TestResultSummary { get; set; }

    public string? ReportFileUrl { get; set; }

    public DateTime? UploadedDate { get; set; }

    public int? UploadedBy { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [JsonIgnore]
    public virtual HhLabBooking? Booking { get; set; }

    [JsonIgnore]
    public virtual HhUser? UploadedByNavigation { get; set; }
}
