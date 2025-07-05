using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hospital_Hub_Portal.Models;

public partial class HhState
{
    public int StateId { get; set; }

    public string StateName { get; set; } = null!;

    public int? CountryId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [JsonIgnore]
    public virtual HhCountry? Country { get; set; }

    [JsonIgnore]
    public virtual ICollection<HhCity> HhCities { get; set; } = new List<HhCity>();
}
