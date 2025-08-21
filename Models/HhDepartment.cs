using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

[Table("HH_Department")]
public partial class HhDepartment
{
    [Key]
    [Column("DepartmentID")]
    public int DepartmentId { get; set; }

    [StringLength(100)]
    public string? DepartmentName { get; set; }

    [Column("HospitalID")]
    public int? HospitalId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [InverseProperty("Department")]
    public virtual ICollection<HhDoctor> HhDoctors { get; set; } = new List<HhDoctor>();

    [ForeignKey("HospitalId")]
    [InverseProperty("HhDepartments")]
    public virtual HhHospital? Hospital { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("HhDepartments")]
    public virtual HhUser? User { get; set; }
}
