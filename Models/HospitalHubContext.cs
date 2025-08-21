using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Hub_Portal.Models;

public partial class HospitalHubContext : DbContext
{
    public HospitalHubContext()
    {
    }

    public HospitalHubContext(DbContextOptions<HospitalHubContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Conversation> Conversations { get; set; }

    public virtual DbSet<DoctorTimeSlot> DoctorTimeSlots { get; set; }

    public virtual DbSet<HhAppointment> HhAppointments { get; set; }

    public virtual DbSet<HhAppointmentCancellationLog> HhAppointmentCancellationLogs { get; set; }

    public virtual DbSet<HhCity> HhCities { get; set; }

    public virtual DbSet<HhCountry> HhCountries { get; set; }

    public virtual DbSet<HhDepartment> HhDepartments { get; set; }

    public virtual DbSet<HhDoctor> HhDoctors { get; set; }

    public virtual DbSet<HhDoctorAvailableTimeSlot> HhDoctorAvailableTimeSlots { get; set; }

    public virtual DbSet<HhDoctorReview> HhDoctorReviews { get; set; }

    public virtual DbSet<HhEmergency> HhEmergencies { get; set; }

    public virtual DbSet<HhHospital> HhHospitals { get; set; }

    public virtual DbSet<HhHospitalReview> HhHospitalReviews { get; set; }

    public virtual DbSet<HhLabBooking> HhLabBookings { get; set; }

    public virtual DbSet<HhLabReport> HhLabReports { get; set; }

    public virtual DbSet<HhLabTest> HhLabTests { get; set; }

    public virtual DbSet<HhLanguage> HhLanguages { get; set; }

    public virtual DbSet<HhMedicine> HhMedicines { get; set; }

    public virtual DbSet<HhMedicineCategory> HhMedicineCategories { get; set; }

    public virtual DbSet<HhMedicineUnit> HhMedicineUnits { get; set; }

    public virtual DbSet<HhPayment> HhPayments { get; set; }

    public virtual DbSet<HhSpecialization> HhSpecializations { get; set; }

    public virtual DbSet<HhState> HhStates { get; set; }

    public virtual DbSet<HhUser> HhUsers { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<TimeSlot> TimeSlots { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("workstation id=Hospital_Hub.mssql.somee.com;packet size=4096;user id=Smit_SQLLogin_1;pwd=paqssvlqnr;data source=Hospital_Hub.mssql.somee.com;persist security info=False;initial catalog=Hospital_Hub;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Conversation>(entity =>
        {
            entity.HasOne(d => d.Doctor).WithMany(p => p.Conversations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Conversation_HH_Doctor");

            entity.HasOne(d => d.Patient).WithMany(p => p.Conversations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Conversation_HH_User");
        });

        modelBuilder.Entity<DoctorTimeSlot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DoctorTi__3214EC072A554B9B");

            entity.HasOne(d => d.Doctor).WithMany(p => p.DoctorTimeSlots)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoctorTim__Docto__57DD0BE4");

            entity.HasOne(d => d.TimeSlot).WithMany(p => p.DoctorTimeSlots)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoctorTim__TimeS__56E8E7AB");
        });

        modelBuilder.Entity<HhAppointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__HH_Appoi__8ECDFCA2429119F3");

            entity.Property(e => e.AppointmentCreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.AppointmentStatus).HasDefaultValue("Scheduled");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Doctor).WithMany(p => p.HhAppointments).HasConstraintName("FK__HH_Appoin__Docto__01142BA1");

            entity.HasOne(d => d.Hospital).WithMany(p => p.HhAppointments).HasConstraintName("FK__HH_Appoin__Hospi__02084FDA");

            entity.HasOne(d => d.User).WithMany(p => p.HhAppointments).HasConstraintName("FK__HH_Appoin__UserI__02FC7413");
        });

        modelBuilder.Entity<HhAppointmentCancellationLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__HH_Appoi__5E5499A8FEDAA283");

            entity.Property(e => e.CancellationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Appointment).WithMany(p => p.HhAppointmentCancellationLogs).HasConstraintName("FK__HH_Appoin__Appoi__03F0984C");

            entity.HasOne(d => d.User).WithMany(p => p.HhAppointmentCancellationLogs).HasConstraintName("FK__HH_Appoin__UserI__04E4BC85");
        });

        modelBuilder.Entity<HhCity>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__HH_City__F2D21A96E3180155");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.State).WithMany(p => p.HhCities).HasConstraintName("FK__HH_City__StateID__05D8E0BE");
        });

        modelBuilder.Entity<HhCountry>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__HH_Count__10D160BF3262400E");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<HhDepartment>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__HH_Depar__B2079BCD9C7E1884");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Hospital).WithMany(p => p.HhDepartments).HasConstraintName("FK__HH_Depart__Hospi__06CD04F7");

            entity.HasOne(d => d.User).WithMany(p => p.HhDepartments).HasConstraintName("FK__HH_Depart__UserI__07C12930");
        });

        modelBuilder.Entity<HhDoctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__HH_Docto__2DC00EDFEAF90E8F");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Rating).HasDefaultValue(0.0m);

            entity.HasOne(d => d.Department).WithMany(p => p.HhDoctors).HasConstraintName("FK__HH_Doctor__Depar__08B54D69");

            entity.HasOne(d => d.DoctorCity).WithMany(p => p.HhDoctors).HasConstraintName("FK_HH_Doctor_City");

            entity.HasOne(d => d.DoctorCountry).WithMany(p => p.HhDoctors).HasConstraintName("FK_HH_Doctor_Country");

            entity.HasOne(d => d.DoctorState).WithMany(p => p.HhDoctors).HasConstraintName("FK_HH_Doctor_State");

            entity.HasOne(d => d.Hospital).WithMany(p => p.HhDoctors).HasConstraintName("FK__HH_Doctor__Hospi__09A971A2");

            entity.HasOne(d => d.Specialization).WithMany(p => p.HhDoctors).HasConstraintName("FK__HH_Doctor__Speci__0A9D95DB");

            entity.HasOne(d => d.User).WithMany(p => p.HhDoctors).HasConstraintName("FK__HH_Doctor__UserI__0B91BA14");
        });

        modelBuilder.Entity<HhDoctorAvailableTimeSlot>(entity =>
        {
            entity.HasKey(e => e.SlotId).HasName("PK__HH_Docto__0A124A4F1A11BA47");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Doctor).WithMany(p => p.HhDoctorAvailableTimeSlots).HasConstraintName("FK__HH_Doctor__Docto__0C85DE4D");

            entity.HasOne(d => d.User).WithMany(p => p.HhDoctorAvailableTimeSlots).HasConstraintName("FK__HH_Doctor__UserI__0D7A0286");
        });

        modelBuilder.Entity<HhDoctorReview>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__HH_Docto__74BC79AED55FE6F7");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Rating).HasDefaultValue(0m);
            entity.Property(e => e.ReviewDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Doctor).WithMany(p => p.HhDoctorReviews).HasConstraintName("FK__HH_Doctor__Docto__0E6E26BF");

            entity.HasOne(d => d.User).WithMany(p => p.HhDoctorReviews).HasConstraintName("FK__HH_Doctor__UserI__0F624AF8");
        });

        modelBuilder.Entity<HhEmergency>(entity =>
        {
            entity.HasKey(e => e.EmergencyId).HasName("PK__HH_Emerg__7B554433E1976C63");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Hospital).WithMany(p => p.HhEmergencies).HasConstraintName("FK__HH_Emerge__Hospi__10566F31");

            entity.HasOne(d => d.User).WithMany(p => p.HhEmergencies).HasConstraintName("FK__HH_Emerge__UserI__114A936A");
        });

        modelBuilder.Entity<HhHospital>(entity =>
        {
            entity.HasKey(e => e.HospitalId).HasName("PK__HH_Hospi__38C2E58FC1E916CD");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.City).WithMany(p => p.HhHospitals).HasConstraintName("FK__HH_Hospit__CityI__123EB7A3");

            entity.HasOne(d => d.State).WithMany(p => p.HhHospitals).HasConstraintName("FK_HH_Hospital_HH_State");

            entity.HasOne(d => d.User).WithMany(p => p.HhHospitals).HasConstraintName("FK__HH_Hospit__UserI__1332DBDC");
        });

        modelBuilder.Entity<HhHospitalReview>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__HH_Hospi__74BC79AE35DF7A69");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Rating).HasDefaultValue(0m);
            entity.Property(e => e.ReviewDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Hospital).WithMany(p => p.HhHospitalReviews).HasConstraintName("FK__HH_Hospit__Hospi__14270015");

            entity.HasOne(d => d.User).WithMany(p => p.HhHospitalReviews).HasConstraintName("FK__HH_Hospit__UserI__151B244E");
        });

        modelBuilder.Entity<HhLabBooking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__HH_LabBo__73951ACDD84934E0");

            entity.Property(e => e.BookingStatus).HasDefaultValue("Scheduled");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsReportReady).HasDefaultValue(false);

            entity.HasOne(d => d.Hospital).WithMany(p => p.HhLabBookings).HasConstraintName("FK__HH_LabBoo__Hospi__160F4887");

            entity.HasOne(d => d.Payment).WithMany(p => p.HhLabBookings).HasConstraintName("FK__HH_LabBoo__Payme__17036CC0");

            entity.HasOne(d => d.Test).WithMany(p => p.HhLabBookings).HasConstraintName("FK__HH_LabBoo__TestI__17F790F9");

            entity.HasOne(d => d.User).WithMany(p => p.HhLabBookings).HasConstraintName("FK__HH_LabBoo__UserI__18EBB532");
        });

        modelBuilder.Entity<HhLabReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__HH_LabRe__D5BD48E5D509D324");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UploadedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Booking).WithMany(p => p.HhLabReports).HasConstraintName("FK__HH_LabRep__Booki__19DFD96B");

            entity.HasOne(d => d.UploadedByNavigation).WithMany(p => p.HhLabReports).HasConstraintName("FK__HH_LabRep__Uploa__1AD3FDA4");
        });

        modelBuilder.Entity<HhLabTest>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__HH_LabTe__8CC331005FA6D75F");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.HhLabTests).HasConstraintName("FK__HH_LabTes__Creat__1BC821DD");

            entity.HasOne(d => d.Hospital).WithMany(p => p.HhLabTests).HasConstraintName("FK__HH_LabTes__Hospi__1CBC4616");
        });

        modelBuilder.Entity<HhLanguage>(entity =>
        {
            entity.HasKey(e => e.LanguageId).HasName("PK__Hh_Langu__12696A62CEDD30E4");
        });

        modelBuilder.Entity<HhMedicine>(entity =>
        {
            entity.HasKey(e => e.MedicineId).HasName("PK__HH_Medic__4F2128F046C51246");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Category).WithMany(p => p.HhMedicines).HasConstraintName("FK__HH_Medici__Categ__1DB06A4F");

            entity.HasOne(d => d.Unit).WithMany(p => p.HhMedicines).HasConstraintName("FK__HH_Medici__UnitI__1EA48E88");
        });

        modelBuilder.Entity<HhMedicineCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__HH_Medic__19093A2B8F72C09A");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.HhMedicineCategories).HasConstraintName("FK__HH_Medici__UserI__1F98B2C1");
        });

        modelBuilder.Entity<HhMedicineUnit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("PK__HH_Medic__44F5EC950A660756");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.HhMedicineUnits).HasConstraintName("FK__HH_Medici__UserI__208CD6FA");
        });

        modelBuilder.Entity<HhPayment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__HH_Payme__9B556A582970F098");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PaymentDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PaymentStatus).HasDefaultValue("Pending");

            entity.HasOne(d => d.Appointment).WithMany(p => p.HhPayments).HasConstraintName("FK__HH_Paymen__Appoi__2180FB33");

            entity.HasOne(d => d.Doctor).WithMany(p => p.HhPayments).HasConstraintName("FK__HH_Paymen__Docto__22751F6C");

            entity.HasOne(d => d.User).WithMany(p => p.HhPayments).HasConstraintName("FK__HH_Paymen__UserI__236943A5");
        });

        modelBuilder.Entity<HhSpecialization>(entity =>
        {
            entity.HasKey(e => e.SpecializationId).HasName("PK__HH_Speci__5809D84FFEB5395B");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.HhSpecializations).HasConstraintName("FK__HH_Specia__UserI__245D67DE");
        });

        modelBuilder.Entity<HhState>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("PK__HH_State__C3BA3B5AD61130EC");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Country).WithMany(p => p.HhStates).HasConstraintName("FK__HH_State__Countr__25518C17");
        });

        modelBuilder.Entity<HhUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__HH_User__1788CCACE27AEED8");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsAdmin).HasDefaultValue(false);
            entity.Property(e => e.IsHospital).HasDefaultValue(false);
            entity.Property(e => e.IsLab).HasDefaultValue(false);
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.Property(e => e.Message1).IsFixedLength();

            entity.HasOne(d => d.Conversation).WithMany(p => p.Messages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Message_Conversation");
        });

        modelBuilder.Entity<TimeSlot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TimeSlot__3214EC07E00EC11E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
