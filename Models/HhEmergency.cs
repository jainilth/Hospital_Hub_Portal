using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

[Table("HH_Emergency")]
public partial class HhEmergency
{
    [Key]
    [Column("EmergencyID")]
    public int EmergencyId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column("HospitalID")]
    public int? HospitalId { get; set; }

    [StringLength(100)]
    public string? EmergencyType { get; set; }

    public string? Description { get; set; }

    [StringLength(50)]
    public string? EmergencyStatus { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RequestTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ResponseTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [ForeignKey("HospitalId")]
    [InverseProperty("HhEmergencies")]
    public virtual HhHospital? Hospital { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("HhEmergencies")]
    public virtual HhUser? User { get; set; }
}
