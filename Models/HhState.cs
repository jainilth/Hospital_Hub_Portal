using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

[Table("HH_State")]
public partial class HhState
{
    [Key]
    [Column("StateID")]
    public int StateId { get; set; }

    [StringLength(100)]
    public string StateName { get; set; } = null!;

    [Column("CountryID")]
    public int? CountryId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("HhStates")]
    public virtual HhCountry? Country { get; set; }

    [InverseProperty("State")]
    public virtual ICollection<HhCity> HhCities { get; set; } = new List<HhCity>();

    [InverseProperty("DoctorState")]
    public virtual ICollection<HhDoctor> HhDoctors { get; set; } = new List<HhDoctor>();

    [InverseProperty("State")]
    public virtual ICollection<HhHospital> HhHospitals { get; set; } = new List<HhHospital>();
}
