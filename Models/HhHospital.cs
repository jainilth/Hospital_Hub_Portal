using System;
using System.Collections.Generic;

namespace Hospital_Hub_Portal.Models;

public partial class HhHospital
{
    public int HospitalId { get; set; }

    public string HospitalName { get; set; } = null!;

    public string? HospitalType { get; set; }

    public string? HospitalAddress { get; set; }

    public int? CityId { get; set; }

    public string? HospitalContectNo { get; set; }

    public string? HospitalEmail { get; set; }

    public DateOnly? EstablishedDate { get; set; }

    public string? WebsiteUrl { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual HhCity? City { get; set; }

    public virtual ICollection<HhAppointment> HhAppointments { get; set; } = new List<HhAppointment>();

    public virtual ICollection<HhDepartment> HhDepartments { get; set; } = new List<HhDepartment>();

    public virtual ICollection<HhDoctor> HhDoctors { get; set; } = new List<HhDoctor>();

    public virtual ICollection<HhEmergency> HhEmergencies { get; set; } = new List<HhEmergency>();

    public virtual ICollection<HhHospitalReview> HhHospitalReviews { get; set; } = new List<HhHospitalReview>();

    public virtual ICollection<HhLabBooking> HhLabBookings { get; set; } = new List<HhLabBooking>();

    public virtual ICollection<HhLabTest> HhLabTests { get; set; } = new List<HhLabTest>();

    public virtual HhUser? User { get; set; }
}
