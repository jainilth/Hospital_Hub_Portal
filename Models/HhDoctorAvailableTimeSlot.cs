using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

[Table("HH_DoctorAvailableTimeSlots")]
public partial class HhDoctorAvailableTimeSlot
{
    [Key]
    [Column("SlotID")]
    public int SlotId { get; set; }

    [Column("DoctorID")]
    public int? DoctorId { get; set; }

    [StringLength(15)]
    public string? DayOfWeek { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [ForeignKey("DoctorId")]
    [InverseProperty("HhDoctorAvailableTimeSlots")]
    public virtual HhDoctor? Doctor { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("HhDoctorAvailableTimeSlots")]
    public virtual HhUser? User { get; set; }
}
