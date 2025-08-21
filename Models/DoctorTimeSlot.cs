using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

[Index("DoctorId", "TimeSlotId", "SlotDate", Name = "UQ_DoctorSlot", IsUnique = true)]
public partial class DoctorTimeSlot
{
    [Key]
    public int Id { get; set; }

    public int TimeSlotId { get; set; }

    public int DoctorId { get; set; }

    public DateOnly SlotDate { get; set; }

    [ForeignKey("DoctorId")]
    [InverseProperty("DoctorTimeSlots")]
    public virtual HhDoctor Doctor { get; set; } = null!;

    [ForeignKey("TimeSlotId")]
    [InverseProperty("DoctorTimeSlots")]
    public virtual TimeSlot TimeSlot { get; set; } = null!;
}
