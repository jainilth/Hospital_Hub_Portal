using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

[Table("HH_MedicineCategory")]
public partial class HhMedicineCategory
{
    [Key]
    [Column("CategoryID")]
    public int CategoryId { get; set; }

    [StringLength(100)]
    public string? CategoryName { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<HhMedicine> HhMedicines { get; set; } = new List<HhMedicine>();

    [ForeignKey("UserId")]
    [InverseProperty("HhMedicineCategories")]
    public virtual HhUser? User { get; set; }
}
