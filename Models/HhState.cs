using System;
using System.Collections.Generic;

namespace Hospital_Hub_Portal.Models;

public partial class HhState
{
    public int StateId { get; set; }

    public string StateName { get; set; } = null!;

    public int? CountryId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual HhCountry? Country { get; set; }

    public virtual ICollection<HhCity> HhCities { get; set; } = new List<HhCity>();
}
