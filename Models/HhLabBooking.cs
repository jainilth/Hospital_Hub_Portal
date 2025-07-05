using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hospital_Hub_Portal.Models;

public partial class HhLabBooking
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }

    public int? TestId { get; set; }

    public int? HospitalId { get; set; }

    public DateTime? BookingDateTime { get; set; }

    public string? BookingStatus { get; set; }

    public string? Symptoms { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? PaymentId { get; set; }

    public bool? IsReportReady { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<HhLabReport> HhLabReports { get; set; } = new List<HhLabReport>();

    [JsonIgnore]
    public virtual HhHospital? Hospital { get; set; }

    [JsonIgnore]
    public virtual HhPayment? Payment { get; set; }

    [JsonIgnore]
    public virtual HhLabTest? Test { get; set; }

    [JsonIgnore]
    public virtual HhUser? User { get; set; }
}
