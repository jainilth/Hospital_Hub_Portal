using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

[Table("HH_City")]
public partial class HhCity
{
    [Key]
    [Column("CityID")]
    public int CityId { get; set; }

    [StringLength(100)]
    public string CityName { get; set; } = null!;

    [Column("StateID")]
    public int? StateId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    public int? DemoColumn { get; set; }

    [InverseProperty("DoctorCity")]
    public virtual ICollection<HhDoctor> HhDoctors { get; set; } = new List<HhDoctor>();

    [InverseProperty("City")]
    public virtual ICollection<HhHospital> HhHospitals { get; set; } = new List<HhHospital>();

    [ForeignKey("StateId")]
    [InverseProperty("HhCities")]
    public virtual HhState? State { get; set; }
}
