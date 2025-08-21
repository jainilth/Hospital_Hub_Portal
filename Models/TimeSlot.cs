using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

public partial class TimeSlot
{
    [Key]
    public int Id { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    [InverseProperty("TimeSlot")]
    public virtual ICollection<DoctorTimeSlot> DoctorTimeSlots { get; set; } = new List<DoctorTimeSlot>();
}
