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

    public virtual DbSet<HhMedicine> HhMedicines { get; set; }

    public virtual DbSet<HhMedicineCategory> HhMedicineCategories { get; set; }

    public virtual DbSet<HhMedicineUnit> HhMedicineUnits { get; set; }

    public virtual DbSet<HhPayment> HhPayments { get; set; }

    public virtual DbSet<HhSpecialization> HhSpecializations { get; set; }

    public virtual DbSet<HhState> HhStates { get; set; }

    public virtual DbSet<HhUser> HhUsers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HhAppointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__HH_Appoi__8ECDFCA2CC2E9E6D");

            entity.ToTable("HH_Appointment");

            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.AppointmentCreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.AppointmentDateGivenToPatient).HasColumnType("datetime");
            entity.Property(e => e.AppointmentDateTime).HasColumnType("datetime");
            entity.Property(e => e.AppointmentStatus)
                .HasMaxLength(20)
                .HasDefaultValue("Scheduled");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.HospitalId).HasColumnName("HospitalID");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Doctor).WithMany(p => p.HhAppointments)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__HH_Appoin__Docto__01142BA1");

            entity.HasOne(d => d.Hospital).WithMany(p => p.HhAppointments)
                .HasForeignKey(d => d.HospitalId)
                .HasConstraintName("FK__HH_Appoin__Hospi__02084FDA");

            entity.HasOne(d => d.User).WithMany(p => p.HhAppointments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__HH_Appoin__UserI__00200768");
        });

        modelBuilder.Entity<HhAppointmentCancellationLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__HH_Appoi__5E5499A8A60F34C2");

            entity.ToTable("HH_AppointmentCancellationLog");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.CanceledBy).HasMaxLength(20);
            entity.Property(e => e.CancellationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Appointment).WithMany(p => p.HhAppointmentCancellationLogs)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__HH_Appoin__Appoi__1CBC4616");

            entity.HasOne(d => d.User).WithMany(p => p.HhAppointmentCancellationLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__HH_Appoin__UserI__1EA48E88");
        });

        modelBuilder.Entity<HhCity>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__HH_City__F2D21A96B5A0AA83");

            entity.ToTable("HH_City");

            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CityName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.StateId).HasColumnName("StateID");

            entity.HasOne(d => d.State).WithMany(p => p.HhCities)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK__HH_City__StateID__5070F446");
        });

        modelBuilder.Entity<HhCountry>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__HH_Count__10D160BF2A4AD6C4");

            entity.ToTable("HH_Country");

            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CountryName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<HhDepartment>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__HH_Depar__B2079BCD16A3765A");

            entity.ToTable("HH_Department");

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
            entity.Property(e => e.HospitalId).HasColumnName("HospitalID");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Hospital).WithMany(p => p.HhDepartments)
                .HasForeignKey(d => d.HospitalId)
                .HasConstraintName("FK__HH_Depart__Hospi__5EBF139D");

            entity.HasOne(d => d.User).WithMany(p => p.HhDepartments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__HH_Depart__UserI__5FB337D6");
        });

        modelBuilder.Entity<HhDoctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__HH_Docto__2DC00EDF58857C67");

            entity.ToTable("HH_Doctor");

            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.ConsultationFee).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DoctorContectNo).HasMaxLength(20);
            entity.Property(e => e.DoctorEmail).HasMaxLength(100);
            entity.Property(e => e.DoctorGender).HasMaxLength(10);
            entity.Property(e => e.DoctorName).HasMaxLength(100);
            entity.Property(e => e.DoctorPhotoUrl)
                .HasMaxLength(200)
                .HasColumnName("DoctorPhotoURL");
            entity.Property(e => e.HospitalId).HasColumnName("HospitalID");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Rating)
                .HasDefaultValue(0.0m)
                .HasColumnType("decimal(2, 1)");
            entity.Property(e => e.SpecializationId).HasColumnName("SpecializationID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Department).WithMany(p => p.HhDoctors)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__HH_Doctor__Depar__68487DD7");

            entity.HasOne(d => d.Hospital).WithMany(p => p.HhDoctors)
                .HasForeignKey(d => d.HospitalId)
                .HasConstraintName("FK__HH_Doctor__Hospi__693CA210");

            entity.HasOne(d => d.Specialization).WithMany(p => p.HhDoctors)
                .HasForeignKey(d => d.SpecializationId)
                .HasConstraintName("FK__HH_Doctor__Speci__6754599E");

            entity.HasOne(d => d.User).WithMany(p => p.HhDoctors)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__HH_Doctor__UserI__6B24EA82");
        });

        modelBuilder.Entity<HhDoctorAvailableTimeSlot>(entity =>
        {
            entity.HasKey(e => e.SlotId).HasName("PK__HH_Docto__0A124A4FE6147E7B");

            entity.ToTable("HH_DoctorAvailableTimeSlots");

            entity.Property(e => e.SlotId).HasColumnName("SlotID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DayOfWeek).HasMaxLength(15);
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Doctor).WithMany(p => p.HhDoctorAvailableTimeSlots)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__HH_Doctor__Docto__6EF57B66");

            entity.HasOne(d => d.User).WithMany(p => p.HhDoctorAvailableTimeSlots)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__HH_Doctor__UserI__6FE99F9F");
        });

        modelBuilder.Entity<HhDoctorReview>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__HH_Docto__74BC79AECC2125C1");

            entity.ToTable("HH_DoctorReview");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Rating)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(2, 1)");
            entity.Property(e => e.ReviewDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Doctor).WithMany(p => p.HhDoctorReviews)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__HH_Doctor__Docto__0F624AF8");

            entity.HasOne(d => d.User).WithMany(p => p.HhDoctorReviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__HH_Doctor__UserI__123EB7A3");
        });

        modelBuilder.Entity<HhEmergency>(entity =>
        {
            entity.HasKey(e => e.EmergencyId).HasName("PK__HH_Emerg__7B554433DF68529C");

            entity.ToTable("HH_Emergency");

            entity.Property(e => e.EmergencyId).HasColumnName("EmergencyID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmergencyStatus).HasMaxLength(50);
            entity.Property(e => e.EmergencyType).HasMaxLength(100);
            entity.Property(e => e.HospitalId).HasColumnName("HospitalID");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RequestTime).HasColumnType("datetime");
            entity.Property(e => e.ResponseTime).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Hospital).WithMany(p => p.HhEmergencies)
                .HasForeignKey(d => d.HospitalId)
                .HasConstraintName("FK__HH_Emerge__Hospi__236943A5");

            entity.HasOne(d => d.User).WithMany(p => p.HhEmergencies)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__HH_Emerge__UserI__22751F6C");
        });

        modelBuilder.Entity<HhHospital>(entity =>
        {
            entity.HasKey(e => e.HospitalId).HasName("PK__HH_Hospi__38C2E58F88D8D9EB");

            entity.ToTable("HH_Hospital");

            entity.Property(e => e.HospitalId).HasColumnName("HospitalID");
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.HospitalAddress).HasMaxLength(250);
            entity.Property(e => e.HospitalContectNo).HasMaxLength(20);
            entity.Property(e => e.HospitalEmail).HasMaxLength(100);
            entity.Property(e => e.HospitalName).HasMaxLength(100);
            entity.Property(e => e.HospitalType).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.WebsiteUrl)
                .HasMaxLength(200)
                .HasColumnName("WebsiteURL");

            entity.HasOne(d => d.City).WithMany(p => p.HhHospitals)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK__HH_Hospit__CityI__59FA5E80");

            entity.HasOne(d => d.User).WithMany(p => p.HhHospitals)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__HH_Hospit__UserI__5AEE82B9");
        });

        modelBuilder.Entity<HhHospitalReview>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__HH_Hospi__74BC79AEC1BE4FF3");

            entity.ToTable("HH_HospitalReview");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.HospitalId).HasColumnName("HospitalID");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Rating)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(2, 1)");
            entity.Property(e => e.ReviewDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Hospital).WithMany(p => p.HhHospitalReviews)
                .HasForeignKey(d => d.HospitalId)
                .HasConstraintName("FK__HH_Hospit__Hospi__17036CC0");

            entity.HasOne(d => d.User).WithMany(p => p.HhHospitalReviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__HH_Hospit__UserI__160F4887");
        });

        modelBuilder.Entity<HhLabBooking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__HH_LabBo__73951ACD2D3987B0");

            entity.ToTable("HH_LabBooking");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.BookingDateTime).HasColumnType("datetime");
            entity.Property(e => e.BookingStatus)
                .HasMaxLength(20)
                .HasDefaultValue("Scheduled");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.HospitalId).HasColumnName("HospitalID");
            entity.Property(e => e.IsReportReady).HasDefaultValue(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.TestId).HasColumnName("TestID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Hospital).WithMany(p => p.HhLabBookings)
                .HasForeignKey(d => d.HospitalId)
                .HasConstraintName("FK__HH_LabBoo__Hospi__2DE6D218");

            entity.HasOne(d => d.Payment).WithMany(p => p.HhLabBookings)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__HH_LabBoo__Payme__30C33EC3");

            entity.HasOne(d => d.Test).WithMany(p => p.HhLabBookings)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("FK__HH_LabBoo__TestI__2CF2ADDF");

            entity.HasOne(d => d.User).WithMany(p => p.HhLabBookings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__HH_LabBoo__UserI__2BFE89A6");
        });

        modelBuilder.Entity<HhLabReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__HH_LabRe__D5BD48E557A635D5");

            entity.ToTable("HH_LabReport");

            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ReportFileUrl)
                .HasMaxLength(200)
                .HasColumnName("ReportFileURL");
            entity.Property(e => e.UploadedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Booking).WithMany(p => p.HhLabReports)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__HH_LabRep__Booki__3493CFA7");

            entity.HasOne(d => d.UploadedByNavigation).WithMany(p => p.HhLabReports)
                .HasForeignKey(d => d.UploadedBy)
                .HasConstraintName("FK__HH_LabRep__Uploa__367C1819");
        });

        modelBuilder.Entity<HhLabTest>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__HH_LabTe__8CC331007169CFEF");

            entity.ToTable("HH_LabTest");

            entity.Property(e => e.TestId).HasColumnName("TestID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.HospitalId).HasColumnName("HospitalID");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.TestName).HasMaxLength(100);
            entity.Property(e => e.TestPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.HhLabTests)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__HH_LabTes__Creat__282DF8C2");

            entity.HasOne(d => d.Hospital).WithMany(p => p.HhLabTests)
                .HasForeignKey(d => d.HospitalId)
                .HasConstraintName("FK__HH_LabTes__Hospi__2739D489");
        });

        modelBuilder.Entity<HhMedicine>(entity =>
        {
            entity.HasKey(e => e.MedicineId).HasName("PK__HH_Medic__4F2128F0C8F62947");

            entity.ToTable("HH_Medicine");

            entity.Property(e => e.MedicineId).HasColumnName("MedicineID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MedicineBrand).HasMaxLength(100);
            entity.Property(e => e.MedicineDosage).HasMaxLength(50);
            entity.Property(e => e.MedicineGenericName).HasMaxLength(100);
            entity.Property(e => e.MedicineName).HasMaxLength(100);
            entity.Property(e => e.MedicinePrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");

            entity.HasOne(d => d.Category).WithMany(p => p.HhMedicines)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__HH_Medici__Categ__7B5B524B");

            entity.HasOne(d => d.Unit).WithMany(p => p.HhMedicines)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK__HH_Medici__UnitI__7C4F7684");
        });

        modelBuilder.Entity<HhMedicineCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__HH_Medic__19093A2BCF08E0C7");

            entity.ToTable("HH_MedicineCategory");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.HhMedicineCategories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__HH_Medici__UserI__73BA3083");
        });

        modelBuilder.Entity<HhMedicineUnit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("PK__HH_Medic__44F5EC955C4180CC");

            entity.ToTable("HH_MedicineUnit");

            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.UnitName).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.HhMedicineUnits)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__HH_Medici__UserI__778AC167");
        });

        modelBuilder.Entity<HhPayment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__HH_Payme__9B556A5801F3E2A2");

            entity.ToTable("HH_Payment");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaymentCurrency).HasMaxLength(10);
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.PaymentUpiId)
                .HasMaxLength(100)
                .HasColumnName("PaymentUPI_ID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Appointment).WithMany(p => p.HhPayments)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__HH_Paymen__Appoi__09A971A2");

            entity.HasOne(d => d.Doctor).WithMany(p => p.HhPayments)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__HH_Paymen__Docto__08B54D69");

            entity.HasOne(d => d.User).WithMany(p => p.HhPayments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__HH_Paymen__UserI__07C12930");
        });

        modelBuilder.Entity<HhSpecialization>(entity =>
        {
            entity.HasKey(e => e.SpecializationId).HasName("PK__HH_Speci__5809D84F0237C284");

            entity.ToTable("HH_Specialization");

            entity.Property(e => e.SpecializationId).HasColumnName("SpecializationID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.SpecializationName).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.HhSpecializations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__HH_Specia__UserI__6383C8BA");
        });

        modelBuilder.Entity<HhState>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("PK__HH_State__C3BA3B5AFBDF84C3");

            entity.ToTable("HH_State");

            entity.Property(e => e.StateId).HasColumnName("StateID");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.StateName).HasMaxLength(100);

            entity.HasOne(d => d.Country).WithMany(p => p.HhStates)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__HH_State__Countr__4CA06362");
        });

        modelBuilder.Entity<HhUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__HH_User__1788CCACCAF345A2");

            entity.ToTable("HH_User");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsAdmin).HasDefaultValue(false);
            entity.Property(e => e.IsHospital).HasDefaultValue(false);
            entity.Property(e => e.IsLab).HasDefaultValue(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.UserContactNo).HasMaxLength(20);
            entity.Property(e => e.UserEmail).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(100);
            entity.Property(e => e.UserPassword).HasMaxLength(200);
            entity.Property(e => e.UserRole).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
