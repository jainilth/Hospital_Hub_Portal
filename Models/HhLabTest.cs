using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

[Table("HH_LabTest")]
public partial class HhLabTest
{
    [Key]
    [Column("TestID")]
    public int TestId { get; set; }

    [StringLength(100)]
    public string? TestName { get; set; }

    public string? TestDescription { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? TestPrice { get; set; }

    [Column("HospitalID")]
    public int? HospitalId { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("HhLabTests")]
    public virtual HhUser? CreatedByNavigation { get; set; }

    [InverseProperty("Test")]
    public virtual ICollection<HhLabBooking> HhLabBookings { get; set; } = new List<HhLabBooking>();

    [ForeignKey("HospitalId")]
    [InverseProperty("HhLabTests")]
    public virtual HhHospital? Hospital { get; set; }
}
