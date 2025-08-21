using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

[Table("HH_HospitalReview")]
public partial class HhHospitalReview
{
    [Key]
    [Column("ReviewID")]
    public int ReviewId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column("HospitalID")]
    public int? HospitalId { get; set; }

    [Column(TypeName = "decimal(2, 1)")]
    public decimal? Rating { get; set; }

    public string? ReviewText { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReviewDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [ForeignKey("HospitalId")]
    [InverseProperty("HhHospitalReviews")]
    public virtual HhHospital? Hospital { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("HhHospitalReviews")]
    public virtual HhUser? User { get; set; }
}
