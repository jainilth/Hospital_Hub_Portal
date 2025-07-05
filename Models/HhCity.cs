using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace Hospital_Hub_Portal.Models;

public partial class HhCity
{
    public int CityId { get; set; }

    public string CityName { get; set; } = null!;

    public int? StateId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<HhHospital> HhHospitals { get; set; } = new List<HhHospital>();

    [JsonIgnore]
    public virtual HhState? State { get; set; }
}
