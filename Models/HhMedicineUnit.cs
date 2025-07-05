using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hospital_Hub_Portal.Models;

public partial class HhMedicineUnit
{
    public int UnitId { get; set; }

    public string? UnitName { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<HhMedicine> HhMedicines { get; set; } = new List<HhMedicine>();

    [JsonIgnore]
    public virtual HhUser? User { get; set; }
}
