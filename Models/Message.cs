using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

[Table("Message")]
public partial class Message
{
    [Key]
    [Column("MessageID")]
    public int MessageId { get; set; }

    [Column("ConversationID")]
    public int ConversationId { get; set; }

    [Column("Message")]
    [StringLength(500)]
    public string Message1 { get; set; } = null!;


    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int SendBy { get; set; }

    [ForeignKey("ConversationId")]
    [InverseProperty("Messages")]
    public virtual Conversation Conversation { get; set; } = null!;
}
