using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

[Table("HH_MedicineUnit")]
public partial class HhMedicineUnit
{
    [Key]
    [Column("UnitID")]
    public int UnitId { get; set; }

    [StringLength(50)]
    public string? UnitName { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [InverseProperty("Unit")]
    public virtual ICollection<HhMedicine> HhMedicines { get; set; } = new List<HhMedicine>();

    [ForeignKey("UserId")]
    [InverseProperty("HhMedicineUnits")]
    public virtual HhUser? User { get; set; }
}
