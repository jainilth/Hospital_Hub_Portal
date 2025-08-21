using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

[Table("HH_AppointmentCancellationLog")]
public partial class HhAppointmentCancellationLog
{
    [Key]
    [Column("LogID")]
    public int LogId { get; set; }

    [Column("AppointmentID")]
    public int? AppointmentId { get; set; }

    [StringLength(20)]
    public string? CanceledBy { get; set; }

    public string? CancellationReason { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CancellationDate { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [ForeignKey("AppointmentId")]
    [InverseProperty("HhAppointmentCancellationLogs")]
    public virtual HhAppointment? Appointment { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("HhAppointmentCancellationLogs")]
    public virtual HhUser? User { get; set; }
}
