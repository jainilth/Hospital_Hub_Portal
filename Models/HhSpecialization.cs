using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

[Table("HH_Specialization")]
public partial class HhSpecialization
{
    [Key]
    [Column("SpecializationID")]
    public int SpecializationId { get; set; }

    [StringLength(100)]
    public string? SpecializationName { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [InverseProperty("Specialization")]
    public virtual ICollection<HhDoctor> HhDoctors { get; set; } = new List<HhDoctor>();

    [ForeignKey("UserId")]
    [InverseProperty("HhSpecializations")]
    public virtual HhUser? User { get; set; }
}
