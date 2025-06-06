using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Model.Models;

public partial class ViegoDb1Context : DbContext
{
    public ViegoDb1Context()
    {
    }

    public ViegoDb1Context(DbContextOptions<ViegoDb1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public DbSet<DiscountCode> DiscountCodes { get; set; }


    public virtual DbSet<Itinerary> Itineraries { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ServiceProvider> ServiceProviders { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    public virtual DbSet<TourAssignment> TourAssignments { get; set; }

    public virtual DbSet<TourAttendee> TourAttendees { get; set; }

    public virtual DbSet<TourExclusion> TourExclusions { get; set; }

    public virtual DbSet<TourGuide> TourGuides { get; set; }

    public virtual DbSet<TourHighlight> TourHighlights { get; set; }

    public virtual DbSet<TourInclusion> TourInclusions { get; set; }

    public virtual DbSet<TourSchedule> TourSchedules { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VoucherUsage> VoucherUsages { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951ACDA720ACAE");

            entity.HasIndex(e => e.BookingStatus, "IX_Bookings_BookingStatus");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.BookingStatus)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DiscountCodeId).HasColumnName("DiscountCodeID");
            entity.Property(e => e.NumChildren).HasDefaultValue(0);
            entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodID");
            entity.Property(e => e.PaymentNotes)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.SpecialRequests).HasColumnType("text");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TourId).HasColumnName("TourID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            //entity.HasOne(d => d.DiscountCode).WithMany(p => p.Bookings)
            //    .HasForeignKey(d => d.DiscountCodeId)
            //    .HasConstraintName("FK__Bookings__Discou__6383C8BA");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("FK__Bookings__Paymen__628FA481");

            entity.HasOne(d => d.Schedule).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__Schedu__60A75C0F");

            entity.HasOne(d => d.Tour).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__TourID__5FB337D6");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__UserID__619B8048");
        });

        //modelBuilder.Entity<DiscountCode>(entity =>
        //{
        //    entity.HasKey(e => e.DiscountCodeId).HasName("PK__Discount__6F31602A0CDFE43F");

        //    entity.HasIndex(e => e.Code, "UQ__Discount__A25C5AA7603332B6").IsUnique();

        //    entity.Property(e => e.DiscountCodeId).HasColumnName("DiscountCodeID");
        //    entity.Property(e => e.Code)
        //        .HasMaxLength(20)
        //        .IsUnicode(false);
        //    entity.Property(e => e.Description)
        //        .HasMaxLength(100)
        //        .IsUnicode(false);
        //    entity.Property(e => e.DiscountValue).HasColumnType("decimal(10, 2)");
        //    entity.Property(e => e.Status)
        //        .HasMaxLength(10)
        //        .IsUnicode(false);
        //    entity.Property(e => e.UsedQuantity).HasDefaultValue(0);
        //});

        modelBuilder.Entity<DiscountCode>()
                .Property(dc => dc.DiscountValue)
                .HasColumnType("DECIMAL(10,2)");

        modelBuilder.Entity<DiscountCode>()
            .Property(dc => dc.Status)
            .HasConversion<string>()
            .HasMaxLength(10);


        modelBuilder.Entity<Itinerary>(entity =>
        {
            entity.HasKey(e => e.ItineraryId).HasName("PK__Itinerar__361216A6434CD361");

            entity.Property(e => e.ItineraryId).HasColumnName("ItineraryID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TourId).HasColumnName("TourID");

            entity.HasOne(d => d.Tour).WithMany(p => p.Itineraries)
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Itinerari__TourI__66603565");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PK__PaymentM__DC31C1F3DA96D595");

            entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AE519CED32");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.Comment).HasColumnType("text");
            entity.Property(e => e.TourId).HasColumnName("TourID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Booking).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__Booking__74AE54BC");

            entity.HasOne(d => d.Tour).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__TourID__72C60C4A");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__UserID__73BA3083");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A8B036B87");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B61604B73FECF").IsUnique();

            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ServiceProvider>(entity =>
        {
            entity.HasKey(e => e.ProviderId).HasName("PK__ServiceP__B54C689D5CD7CFFC");

            entity.HasIndex(e => e.Email, "UQ__ServiceP__A9D105345E1479E4").IsUnique();

            entity.Property(e => e.ProviderId).HasColumnName("ProviderID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ProviderType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.ServiceProviders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ServicePr__UserI__440B1D61");
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.TourId).HasName("PK__Tours__604CEA103FD60ACF");

            entity.HasIndex(e => e.TourCode, "UQ__Tours__1982F8D0366BFBCC").IsUnique();

            entity.Property(e => e.TourId).HasColumnName("TourID");
            entity.Property(e => e.DepartureCity)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Destination)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Duration)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FeaturedImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("FeaturedImageURL");
            entity.Property(e => e.Policies).HasColumnType("text");
            entity.Property(e => e.ServiceProviderId).HasColumnName("ServiceProviderID");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TourCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TourName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TourType)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.ServiceProvider).WithMany(p => p.Tours)
                .HasForeignKey(d => d.ServiceProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tours__ServicePr__5629CD9C");
        });

        modelBuilder.Entity<TourAssignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__TourAssi__32499E57890AFDC1");

            entity.HasIndex(e => e.Status, "IX_TourAssignments_Status");

            entity.Property(e => e.AssignmentId).HasColumnName("AssignmentID");
            entity.Property(e => e.GuideId).HasColumnName("GuideID");
            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TourId).HasColumnName("TourID");

            entity.HasOne(d => d.Guide).WithMany(p => p.TourAssignments)
                .HasForeignKey(d => d.GuideId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TourAssig__Guide__7A672E12");

            entity.HasOne(d => d.Schedule).WithMany(p => p.TourAssignments)
                .HasForeignKey(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TourAssig__Sched__797309D9");

            entity.HasOne(d => d.Tour).WithMany(p => p.TourAssignments)
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TourAssig__TourI__787EE5A0");
        });

        modelBuilder.Entity<TourAttendee>(entity =>
        {
            entity.HasKey(e => e.AttendeeId).HasName("PK__TourAtte__1844012862DF1396");

            entity.HasIndex(e => e.AttendanceStatus, "IX_TourAttendees_AttendanceStatus");

            entity.Property(e => e.AttendeeId).HasColumnName("AttendeeID");
            entity.Property(e => e.AttendanceStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Absent");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Booking).WithMany(p => p.TourAttendees)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TourAtten__Booki__04E4BC85");

            entity.HasOne(d => d.Schedule).WithMany(p => p.TourAttendees)
                .HasForeignKey(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TourAtten__Sched__05D8E0BE");

            entity.HasOne(d => d.User).WithMany(p => p.TourAttendees)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__TourAtten__UserI__06CD04F7");
        });

        modelBuilder.Entity<TourExclusion>(entity =>
        {
            entity.HasKey(e => e.ExclusionId).HasName("PK__TourExcl__2703AF95C66C02AC");

            entity.Property(e => e.ExclusionId).HasColumnName("ExclusionID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TourId).HasColumnName("TourID");

            entity.HasOne(d => d.Tour).WithMany(p => p.TourExclusions)
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TourExclu__TourI__6EF57B66");
        });

        modelBuilder.Entity<TourGuide>(entity =>
        {
            entity.HasKey(e => e.GuideId).HasName("PK__TourGuid__E77EE03E436CB6D4");

            entity.HasIndex(e => e.Email, "UQ__TourGuid__A9D1053484EADB1F").IsUnique();

            entity.Property(e => e.GuideId).HasColumnName("GuideID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Bio).HasColumnType("text");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.GuideType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.TourGuides)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__TourGuide__UserI__4AB81AF0");
        });

        modelBuilder.Entity<TourHighlight>(entity =>
        {
            entity.HasKey(e => e.HighlightId).HasName("PK__TourHigh__B11CEDD069534D93");

            entity.Property(e => e.HighlightId).HasColumnName("HighlightID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TourId).HasColumnName("TourID");

            entity.HasOne(d => d.Tour).WithMany(p => p.TourHighlights)
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TourHighl__TourI__693CA210");
        });

        modelBuilder.Entity<TourInclusion>(entity =>
        {
            entity.HasKey(e => e.InclusionId).HasName("PK__TourIncl__0486AEBF9957F28C");

            entity.Property(e => e.InclusionId).HasColumnName("InclusionID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TourId).HasColumnName("TourID");

            entity.HasOne(d => d.Tour).WithMany(p => p.TourInclusions)
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TourInclu__TourI__6C190EBB");
        });

        modelBuilder.Entity<TourSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__TourSche__9C8A5B690191ADF8");

            entity.HasIndex(e => e.DepartureDate, "IX_TourSchedules_DepartureDate");

            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.MeetingPoint)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TourId).HasColumnName("TourID");

            entity.HasOne(d => d.Tour).WithMany(p => p.TourSchedules)
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TourSched__TourI__59FA5E80");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACA952F6BB");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534396A980C").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Notes).HasColumnType("text");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserType)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleId__3E52440B");
        });

        modelBuilder.Entity<VoucherUsage>(entity =>
        {
            entity.HasKey(e => e.UsageId).HasName("PK__VoucherU__29B197C03EFF77A3");

            entity.ToTable("VoucherUsage");

            entity.Property(e => e.UsageId).HasColumnName("UsageID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.DiscountCodeId).HasColumnName("DiscountCodeID");
            entity.Property(e => e.TourId).HasColumnName("TourID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Booking).WithMany(p => p.VoucherUsages)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VoucherUs__Booki__7E37BEF6");

            //entity.HasOne(d => d.DiscountCode).WithMany(p => p.VoucherUsages)
            //    .HasForeignKey(d => d.DiscountCodeId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__VoucherUs__Disco__7D439ABD");

            entity.HasOne(d => d.Tour).WithMany(p => p.VoucherUsages)
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VoucherUs__TourI__00200768");

            entity.HasOne(d => d.User).WithMany(p => p.VoucherUsages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VoucherUs__UserI__7F2BE32F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
