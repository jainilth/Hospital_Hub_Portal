using System;
using System.Collections.Generic;

namespace Hospital_Hub_Portal.Models;

public partial class HhDepartment
{
    public int DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public int? HospitalId { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<HhDoctor> HhDoctors { get; set; } = new List<HhDoctor>();

    public virtual HhHospital? Hospital { get; set; }

    public virtual HhUser? User { get; set; }
}
