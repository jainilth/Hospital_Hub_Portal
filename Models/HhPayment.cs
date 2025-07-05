using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hospital_Hub_Portal.Models;

public partial class HhPayment
{
    public int PaymentId { get; set; }

    public int? UserId { get; set; }

    public int? DoctorId { get; set; }

    public int? AppointmentId { get; set; }

    public string? PaymentUpiId { get; set; }

    public decimal? PaymentAmount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? PaymentStatus { get; set; }

    public string? PaymentMethod { get; set; }

    public string? PaymentCurrency { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [JsonIgnore]
    public virtual HhAppointment? Appointment { get; set; }

    [JsonIgnore]
    public virtual HhDoctor? Doctor { get; set; }

    [JsonIgnore]
    public virtual ICollection<HhLabBooking> HhLabBookings { get; set; } = new List<HhLabBooking>();

    [JsonIgnore]
    public virtual HhUser? User { get; set; }
}
