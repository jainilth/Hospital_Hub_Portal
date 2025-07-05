using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hospital_Hub_Portal.Models;

public partial class HhHospitalReview
{
    public int ReviewId { get; set; }

    public int? UserId { get; set; }

    public int? HospitalId { get; set; }

    public decimal? Rating { get; set; }

    public string? ReviewText { get; set; }

    public DateTime? ReviewDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [JsonIgnore]
    public virtual HhHospital? Hospital { get; set; }

    [JsonIgnore]
    public virtual HhUser? User { get; set; }
}
