using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hospital_Hub_Portal.Models;

public partial class HhSpecialization
{
    public int SpecializationId { get; set; }

    public string? SpecializationName { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<HhDoctor> HhDoctors { get; set; } = new List<HhDoctor>();

    [JsonIgnore]
    public virtual HhUser? User { get; set; }
}
