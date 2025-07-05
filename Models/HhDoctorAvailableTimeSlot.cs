using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace Hospital_Hub_Portal.Models;

public partial class HhDoctorAvailableTimeSlot
{
    public int SlotId { get; set; }

    public int? DoctorId { get; set; }

    public string? DayOfWeek { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [JsonIgnore]
    public virtual HhDoctor? Doctor { get; set; }

    [JsonIgnore]
    public virtual HhUser? User { get; set; }
}
