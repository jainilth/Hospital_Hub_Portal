using System;
using System.Collections.Generic;

namespace Hospital_Hub_Portal.Models;

public partial class HhAppointment
{
    public int AppointmentId { get; set; }

    public int? UserId { get; set; }

    public int? DoctorId { get; set; }

    public int? HospitalId { get; set; }

    public DateTime? AppointmentDateTime { get; set; }

    public string? AppointmentStatus { get; set; }

    public string? Symptoms { get; set; }

    public DateTime? AppointmentCreatedDate { get; set; }

    public DateTime? AppointmentDateGivenToPatient { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual HhDoctor? Doctor { get; set; }

    public virtual ICollection<HhAppointmentCancellationLog> HhAppointmentCancellationLogs { get; set; } = new List<HhAppointmentCancellationLog>();

    public virtual ICollection<HhPayment> HhPayments { get; set; } = new List<HhPayment>();

    public virtual HhHospital? Hospital { get; set; }

    public virtual HhUser? User { get; set; }
}
