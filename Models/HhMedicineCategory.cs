using System;
using System.Collections.Generic;

namespace Hospital_Hub_Portal.Models;

public partial class HhMedicineCategory
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<HhMedicine> HhMedicines { get; set; } = new List<HhMedicine>();

    public virtual HhUser? User { get; set; }
}
