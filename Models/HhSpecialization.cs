using System;
using System.Collections.Generic;

namespace Hospital_Hub_Portal.Models;

public partial class HhSpecialization
{
    public int SpecializationId { get; set; }

    public string? SpecializationName { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<HhDoctor> HhDoctors { get; set; } = new List<HhDoctor>();

    public virtual HhUser? User { get; set; }
}
