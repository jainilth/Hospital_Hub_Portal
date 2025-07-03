using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hospital_Hub_Portal.Models;

public partial class HhCountry
{
    public int CountryId { get; set; }

    public string CountryName { get; set; } = null!;

    [JsonIgnore]
    public DateTime? CreatedDate { get; set; }

    [JsonIgnore]
    public DateTime? ModifiedDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<HhState> HhStates { get; set; } = new List<HhState>();
}
