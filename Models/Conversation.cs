using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

[Table("Conversation")]
public partial class Conversation
{
    [Key]
    [Column("ConversationID")]
    public int ConversationId { get; set; }

    [Column("DoctorID")]
    public int DoctorId { get; set; }

    [Column("PatientID")]
    public int PatientId { get; set; }

    [ForeignKey("DoctorId")]
    [InverseProperty("Conversations")]
    public virtual HhDoctor Doctor { get; set; } = null!;

    [InverseProperty("Conversation")]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    [ForeignKey("PatientId")]
    [InverseProperty("Conversations")]
    public virtual HhUser Patient { get; set; } = null!;
}
