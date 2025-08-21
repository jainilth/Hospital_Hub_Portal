using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

[Table("Hh_Language")]
public partial class HhLanguage
{
    [Key]
    [Column("languageId")]
    public int LanguageId { get; set; }

    [Column("language")]
    [StringLength(50)]
    public string? Language { get; set; }
}
