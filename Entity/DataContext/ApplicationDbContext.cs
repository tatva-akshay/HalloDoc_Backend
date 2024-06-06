using System;
using System.Collections.Generic;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Entity.DataContext;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<AdminRegion> AdminRegions { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }

    public virtual DbSet<BlockRequest> BlockRequests { get; set; }

    public virtual DbSet<Business> Businesses { get; set; }

    public virtual DbSet<CaseTag> CaseTags { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<Concierge> Concierges { get; set; }

    public virtual DbSet<EmailLog> EmailLogs { get; set; }

    public virtual DbSet<Encounter> Encounters { get; set; }

    public virtual DbSet<HealthProfessional> HealthProfessionals { get; set; }

    public virtual DbSet<HealthProfessionalType> HealthProfessionalTypes { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<PasswordReset> PasswordResets { get; set; }

    public virtual DbSet<PayRate> PayRates { get; set; }

    public virtual DbSet<Physician> Physicians { get; set; }

    public virtual DbSet<PhysicianLocation> PhysicianLocations { get; set; }

    public virtual DbSet<PhysicianNotification> PhysicianNotifications { get; set; }

    public virtual DbSet<PhysicianRegion> PhysicianRegions { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<RequestBusiness> RequestBusinesses { get; set; }

    public virtual DbSet<RequestClient> RequestClients { get; set; }

    public virtual DbSet<RequestClosed> RequestCloseds { get; set; }

    public virtual DbSet<RequestConcierge> RequestConcierges { get; set; }

    public virtual DbSet<RequestNote> RequestNotes { get; set; }

    public virtual DbSet<RequestStatusLog> RequestStatusLogs { get; set; }

    public virtual DbSet<RequestType> RequestTypes { get; set; }

    public virtual DbSet<RequestWiseFile> RequestWiseFiles { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleMenu> RoleMenus { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<ShiftDetail> ShiftDetails { get; set; }

    public virtual DbSet<ShiftDetailRegion> ShiftDetailRegions { get; set; }

    public virtual DbSet<Smslog> Smslogs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WeeklyTimeSheet> WeeklyTimeSheets { get; set; }

    public virtual DbSet<WeeklyTimeSheetDetail> WeeklyTimeSheetDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=PCE114\\SQL2019; Initial Catalog=HalloDoc; User id=sa; Password=Tatva@123; Integrated Security=false; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admin__719FE488850ACD27");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.AspNetUser).WithMany(p => p.AdminAspNetUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Admin_AspNetUserId");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.AdminModifiedByNavigations).HasConstraintName("FK_Admin_ModifiedBy");
        });

        modelBuilder.Entity<AdminRegion>(entity =>
        {
            entity.HasKey(e => e.AdminRegionId).HasName("PK__AdminReg__F5BDF9269968C5FF");

            entity.HasOne(d => d.Admin).WithMany(p => p.AdminRegions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AdminRegion_AdminId_fkey");

            entity.HasOne(d => d.Region).WithMany(p => p.AdminRegions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AdminRegion_RegionId_fkey");
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AspNetRo__3214EC074C841CF9");
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AspNetUs__3214EC076A0ADD22");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<AspNetUserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId }).HasName("AspNetUserRoles_pkey");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetUserRoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AspNetUserRoles_RoleId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserRoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AspNetUserRoles_UserId_fkey");
        });

        modelBuilder.Entity<BlockRequest>(entity =>
        {
            entity.HasKey(e => e.BlockRequestId).HasName("PK__BlockReq__0AD66C44490E6BA6");

            entity.HasOne(d => d.Request).WithMany(p => p.BlockRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BlockRequests_RequestId_fkey");
        });

        modelBuilder.Entity<Business>(entity =>
        {
            entity.HasKey(e => e.BusinessId).HasName("PK__Business__F1EAA36EDE4235FE");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.BusinessCreatedByNavigations).HasConstraintName("Business_CreatedBy_fkey");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.BusinessModifiedByNavigations).HasConstraintName("Business_ModifiedBy_fkey");

            entity.HasOne(d => d.Region).WithMany(p => p.Businesses).HasConstraintName("Business_RegionId_fkey");
        });

        modelBuilder.Entity<CaseTag>(entity =>
        {
            entity.HasKey(e => e.CaseTagId).HasName("PK__CaseTag__BE55BE48C3507E2F");

            entity.Property(e => e.CaseTagId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Chat__3214EC077C5BA345");

            entity.HasOne(d => d.Admin).WithMany(p => p.Chats).HasConstraintName("Chat_AdminId_fkey");

            entity.HasOne(d => d.Provider).WithMany(p => p.Chats).HasConstraintName("Chat_ProviderId_fkey");

            entity.HasOne(d => d.Request).WithMany(p => p.Chats).HasConstraintName("Chat_RequestId_fkey");
        });

        modelBuilder.Entity<Concierge>(entity =>
        {
            entity.HasKey(e => e.ConciergeId).HasName("PK__Concierg__8BCFB3F446DDC55E");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Region).WithMany(p => p.Concierges).HasConstraintName("Concierge_RegionId_fkey");
        });

        modelBuilder.Entity<EmailLog>(entity =>
        {
            entity.HasKey(e => e.EmailLogId).HasName("PK__EmailLog__E8CB41EC34AA0171");

            entity.HasOne(d => d.Request).WithMany(p => p.EmailLogs).HasConstraintName("EmailLog_RequestId_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.EmailLogs).HasConstraintName("EmailLog_RoleId_fkey");
        });

        modelBuilder.Entity<Encounter>(entity =>
        {
            entity.HasKey(e => e.EncounterId).HasName("PK__Encounte__4278DD36FAE1528E");

            entity.HasOne(d => d.Request).WithMany(p => p.Encounters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Encounter_RequestId_fkey");
        });

        modelBuilder.Entity<HealthProfessional>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("PK__HealthPr__FC8618F3ADC0A884");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.ProfessionNavigation).WithMany(p => p.HealthProfessionals).HasConstraintName("HealthProfessionals_Profession_fkey");
        });

        modelBuilder.Entity<HealthProfessionalType>(entity =>
        {
            entity.HasKey(e => e.HealthProfessionalId).HasName("PK__HealthPr__B003C36AFC99F250");

            entity.Property(e => e.HealthProfessionalId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("PK__Menu__C99ED23006195F13");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3214EC0766872F57");

            entity.HasOne(d => d.Request).WithMany(p => p.OrderDetails).HasConstraintName("OrderDetails_RequestId_fkey");
        });

        modelBuilder.Entity<PasswordReset>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Password__3214EC0707937D81");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsUpdated).HasDefaultValue(false);
        });

        modelBuilder.Entity<PayRate>(entity =>
        {
            entity.HasKey(e => e.PayRateId).HasName("PK__PayRate__5AAC8EAA759E308B");

            entity.HasOne(d => d.Physician).WithMany(p => p.PayRates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PayRate_PhysicianId_fkey");
        });

        modelBuilder.Entity<Physician>(entity =>
        {
            entity.HasKey(e => e.PhysicianId).HasName("PK__Physicia__DFF5D29374EBA982");

            entity.HasOne(d => d.AspNetUser).WithMany(p => p.PhysicianAspNetUsers).HasConstraintName("Physician_AspNetUserId_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PhysicianCreatedByNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Physician_CreatedBy_fkey");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.PhysicianModifiedByNavigations).HasConstraintName("Physician_ModifiedBy_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Physicians).HasConstraintName("Physician_RoleId_fkey");
        });

        modelBuilder.Entity<PhysicianLocation>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__Physicia__E7FEA4975BBED68A");

            entity.HasOne(d => d.Physician).WithMany(p => p.PhysicianLocations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PhysicianLocation_PhysicianId_fkey");
        });

        modelBuilder.Entity<PhysicianNotification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Physicia__3214EC071498FF6D");

            entity.HasOne(d => d.Physician).WithMany(p => p.PhysicianNotifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PhysicianNotification_PhysicianId_fkey");
        });

        modelBuilder.Entity<PhysicianRegion>(entity =>
        {
            entity.HasKey(e => e.PhysicianRegionId).HasName("PK__Physicia__2999048CEA741721");

            entity.HasOne(d => d.Physician).WithMany(p => p.PhysicianRegions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PhysicianRegion_PhysicianId_fkey");

            entity.HasOne(d => d.Region).WithMany(p => p.PhysicianRegions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PhysicianRegion_RegionId_fkey");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.RegionId).HasName("Region_pkey");

            entity.Property(e => e.RegionId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Request__33A8517A337FCE6C");

            entity.HasOne(d => d.Physician).WithMany(p => p.Requests).HasConstraintName("Request_PhysicianId_fkey");
        });

        modelBuilder.Entity<RequestBusiness>(entity =>
        {
            entity.HasKey(e => e.RequestBusinessId).HasName("PK__RequestB__E270A14088FEA37B");

            entity.HasOne(d => d.Business).WithMany(p => p.RequestBusinesses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestBusiness_BusinessId_fkey");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestBusinesses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestBusiness_RequestId_fkey");
        });

        modelBuilder.Entity<RequestClient>(entity =>
        {
            entity.HasKey(e => e.RequestClientId).HasName("PK__RequestC__7BB6396D412CF45F");

            entity.HasOne(d => d.Region).WithMany(p => p.RequestClients).HasConstraintName("RequestClient_RegionId_fkey");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestClients).HasConstraintName("RequestClient_RequestId_fkey");
        });

        modelBuilder.Entity<RequestClosed>(entity =>
        {
            entity.HasKey(e => e.RequestClosedId).HasName("PK__RequestC__0E98B97ABDE7F607");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestCloseds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestClosed_RequestId_fkey");
        });

        modelBuilder.Entity<RequestConcierge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RequestC__3214EC071AC17FAA");

            entity.HasOne(d => d.Concierge).WithMany(p => p.RequestConcierges)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestConcierge_ConciergeId_fkey");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestConcierges)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestConcierge_RequestId_fkey");
        });

        modelBuilder.Entity<RequestNote>(entity =>
        {
            entity.HasKey(e => e.RequestNotesId).HasName("PK__RequestN__6D10A824B55EB4F5");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.RequestNoteCreatedByNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestNotes_CreatedBy_fkey");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.RequestNoteModifiedByNavigations).HasConstraintName("RequestNotes_ModifiedBy_fkey");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestNotes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestNotes_RequestId_fkey");
        });

        modelBuilder.Entity<RequestStatusLog>(entity =>
        {
            entity.HasKey(e => e.RequestStatusLogId).HasName("PK__RequestS__0DB8FB9D5BC833F5");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Admin).WithMany(p => p.RequestStatusLogs).HasConstraintName("RequestStatusLog_AdminId_fkey");

            entity.HasOne(d => d.Physician).WithMany(p => p.RequestStatusLogPhysicians).HasConstraintName("RequestStatusLog_PhysicianId_fkey");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestStatusLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestStatusLog_RequestId_fkey");

            entity.HasOne(d => d.TransToPhysician).WithMany(p => p.RequestStatusLogTransToPhysicians).HasConstraintName("RequestStatusLog_TransToPhysicianId_fkey");
        });

        modelBuilder.Entity<RequestType>(entity =>
        {
            entity.HasKey(e => e.RequestTypeId).HasName("PK__RequestT__4D328B83A54A8F62");
        });

        modelBuilder.Entity<RequestWiseFile>(entity =>
        {
            entity.HasKey(e => e.RequestWiseFileId).HasName("PK__RequestW__C2397E8B4ADF6796");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Admin).WithMany(p => p.RequestWiseFiles).HasConstraintName("RequestWiseFile_AdminId_fkey");

            entity.HasOne(d => d.Physician).WithMany(p => p.RequestWiseFiles).HasConstraintName("RequestWiseFile_PhysicianId_fkey");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestWiseFiles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestWiseFile_RequestId_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1A2F0B6199");
        });

        modelBuilder.Entity<RoleMenu>(entity =>
        {
            entity.HasKey(e => e.RoleMenuId).HasName("PK__RoleMenu__F86287B62C46209F");

            entity.HasOne(d => d.Menu).WithMany(p => p.RoleMenus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RoleMenu_MenuId_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleMenus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RoleMenu_RoleId_fkey");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.ShiftId).HasName("PK__Shift__C0A83881941E66A8");

            entity.Property(e => e.WeekDays).IsFixedLength();

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Shifts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Shift_CreatedBy_fkey");

            entity.HasOne(d => d.Physician).WithMany(p => p.Shifts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Shift_PhysicianId_fkey");
        });

        modelBuilder.Entity<ShiftDetail>(entity =>
        {
            entity.HasKey(e => e.ShiftDetailId).HasName("PK__ShiftDet__337A927EDE7F5351");

            entity.HasOne(d => d.ModifiedbyNavigation).WithMany(p => p.ShiftDetails).HasConstraintName("ShiftDetail_Modifiedby_fkey");

            entity.HasOne(d => d.Shift).WithMany(p => p.ShiftDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ShiftDetail_ShiftId_fkey");
        });

        modelBuilder.Entity<ShiftDetailRegion>(entity =>
        {
            entity.HasKey(e => e.ShiftDetailRegionId).HasName("PK__ShiftDet__2B07BF616A16D0BA");

            entity.HasOne(d => d.Region).WithMany(p => p.ShiftDetailRegions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ShiftDetailRegion_RegionId_fkey");

            entity.HasOne(d => d.ShiftDetail).WithMany(p => p.ShiftDetailRegions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ShiftDetailRegion_ShiftDetailId_fkey");
        });

        modelBuilder.Entity<Smslog>(entity =>
        {
            entity.HasKey(e => e.SmslogId).HasName("PK__SMSLog__59682B60789BD626");

            entity.HasOne(d => d.Request).WithMany(p => p.Smslogs).HasConstraintName("SMSLog_RequestId_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Smslogs).HasConstraintName("SMSLog_RoleId_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CA14FC88B");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.AspNetUser).WithMany(p => p.Users).HasConstraintName("User_AspNetUserId_fkey");
        });

        modelBuilder.Entity<WeeklyTimeSheet>(entity =>
        {
            entity.HasKey(e => e.TimeSheetId).HasName("PK__WeeklyTi__0625574ACE64208C");

            entity.HasOne(d => d.Admin).WithMany(p => p.WeeklyTimeSheets).HasConstraintName("WeeklyTimeSheet_AdminId_fkey");

            entity.HasOne(d => d.Provider).WithMany(p => p.WeeklyTimeSheets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("WeeklyTimeSheet_ProviderId_fkey");
        });

        modelBuilder.Entity<WeeklyTimeSheetDetail>(entity =>
        {
            entity.HasKey(e => e.TimeSheetDetailId).HasName("PK__WeeklyTi__8CC64E0A11D55AA2");

            entity.HasOne(d => d.TimeSheet).WithMany(p => p.WeeklyTimeSheetDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("WeeklyTimeSheetDetail_TimeSheetId_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
