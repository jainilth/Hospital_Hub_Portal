using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hospital_Hub_Portal.Models;

public partial class HhLabTest
{
    public int TestId { get; set; }

    public string? TestName { get; set; }

    public string? TestDescription { get; set; }

    public decimal? TestPrice { get; set; }

    public int? HospitalId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [JsonIgnore]
    public virtual HhUser? CreatedByNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<HhLabBooking> HhLabBookings { get; set; } = new List<HhLabBooking>();

    [JsonIgnore]
    public virtual HhHospital? Hospital { get; set; }
}
