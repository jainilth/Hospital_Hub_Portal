using System;
using System.Collections.Generic;

namespace Hospital_Hub_Portal.Models;

public partial class HhDoctorReview
{
    public int ReviewId { get; set; }

    public int? DoctorId { get; set; }

    public decimal? Rating { get; set; }

    public string? ReviewText { get; set; }

    public DateTime? ReviewDate { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual HhDoctor? Doctor { get; set; }

    public virtual HhUser? User { get; set; }
}
