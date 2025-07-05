using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace Hospital_Hub_Portal.Models;

public partial class HhEmergency
{
    public int EmergencyId { get; set; }

    public int? UserId { get; set; }

    public int? HospitalId { get; set; }

    public string? EmergencyType { get; set; }

    public string? Description { get; set; }

    public string? EmergencyStatus { get; set; }

    public DateTime? RequestTime { get; set; }

    public DateTime? ResponseTime { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [JsonIgnore]
    public virtual HhHospital? Hospital { get; set; }

    [JsonIgnore]
    public virtual HhUser? User { get; set; }
}
