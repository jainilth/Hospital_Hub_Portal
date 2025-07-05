using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace Hospital_Hub_Portal.Models;

public partial class HhAppointmentCancellationLog
{
    public int LogId { get; set; }

    public int? AppointmentId { get; set; }

    public string? CanceledBy { get; set; }

    public string? CancellationReason { get; set; }

    public DateTime? CancellationDate { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [JsonIgnore]
    public virtual HhAppointment? Appointment { get; set; }

    [JsonIgnore]
    public virtual HhUser? User { get; set; }
}
