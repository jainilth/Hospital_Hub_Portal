using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

    [JsonIgnore]
    public virtual HhCity? City { get; set; }

    [JsonIgnore]
    public virtual ICollection<HhAppointment> HhAppointments { get; set; } = new List<HhAppointment>();

    [JsonIgnore]
    public virtual ICollection<HhDepartment> HhDepartments { get; set; } = new List<HhDepartment>();

    [JsonIgnore]
    public virtual ICollection<HhDoctor> HhDoctors { get; set; } = new List<HhDoctor>();

    [JsonIgnore]
    public virtual ICollection<HhEmergency> HhEmergencies { get; set; } = new List<HhEmergency>();

    [JsonIgnore]
    public virtual ICollection<HhHospitalReview> HhHospitalReviews { get; set; } = new List<HhHospitalReview>();

    [JsonIgnore]
    public virtual ICollection<HhLabBooking> HhLabBookings { get; set; } = new List<HhLabBooking>();

    [JsonIgnore]
    public virtual ICollection<HhLabTest> HhLabTests { get; set; } = new List<HhLabTest>();

    [JsonIgnore]
    public virtual HhUser? User { get; set; }
}
