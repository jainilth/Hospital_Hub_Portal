using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

[Table("HH_Medicine")]
public partial class HhMedicine
{
    [Key]
    [Column("MedicineID")]
    public int MedicineId { get; set; }

    [StringLength(100)]
    public string? MedicineName { get; set; }

    [StringLength(100)]
    public string? MedicineGenericName { get; set; }

    [StringLength(100)]
    public string? MedicineBrand { get; set; }

    public string? MedicineDescription { get; set; }

    [StringLength(50)]
    public string? MedicineDosage { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? MedicinePrice { get; set; }

    [Column("CategoryID")]
    public int? CategoryId { get; set; }

    [Column("UnitID")]
    public int? UnitId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("HhMedicines")]
    public virtual HhMedicineCategory? Category { get; set; }

    [ForeignKey("UnitId")]
    [InverseProperty("HhMedicines")]
    public virtual HhMedicineUnit? Unit { get; set; }
}
