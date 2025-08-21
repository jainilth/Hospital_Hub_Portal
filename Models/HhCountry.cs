using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

[Table("HH_Country")]
public partial class HhCountry
{
    [Key]
    [Column("CountryID")]
    public int CountryId { get; set; }

    [StringLength(100)]
    public string CountryName { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [InverseProperty("DoctorCountry")]
    public virtual ICollection<HhDoctor> HhDoctors { get; set; } = new List<HhDoctor>();

    [InverseProperty("Country")]
    public virtual ICollection<HhState> HhStates { get; set; } = new List<HhState>();
}
