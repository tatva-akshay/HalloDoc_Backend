﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

[Table("Physician")]
public partial class Physician
{
    [Key]
    public int PhysicianId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? LastName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Mobile { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? MedicalLicense { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Photo { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? AdminNotes { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Address1 { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Address2 { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? City { get; set; }

    public int? RegionId { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? Zip { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? AltPhone { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    public short? Status { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string BusinessName { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string BusinessWebsite { get; set; } = null!;

    public int? RoleId { get; set; }

    [Column("NPINumber")]
    [StringLength(500)]
    [Unicode(false)]
    public string? Npinumber { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Signature { get; set; }

    public bool? IsTokenGenerate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SyncEmailAddress { get; set; }

    public int? AspNetUserId { get; set; }

    public int CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public bool? IsAgreementDoc { get; set; }

    public bool? IsBackgroundDoc { get; set; }

    public bool? IsTrainingDoc { get; set; }

    public bool? IsNonDisclosureDoc { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsLicenseDoc { get; set; }

    public bool? IsCredentialDoc { get; set; }

    [ForeignKey("AspNetUserId")]
    [InverseProperty("PhysicianAspNetUsers")]
    public virtual AspNetUser? AspNetUser { get; set; }

    [InverseProperty("Provider")]
    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    [ForeignKey("CreatedBy")]
    [InverseProperty("PhysicianCreatedByNavigations")]
    public virtual AspNetUser CreatedByNavigation { get; set; } = null!;

    [ForeignKey("ModifiedBy")]
    [InverseProperty("PhysicianModifiedByNavigations")]
    public virtual AspNetUser? ModifiedByNavigation { get; set; }

    [InverseProperty("Physician")]
    public virtual ICollection<PayRate> PayRates { get; set; } = new List<PayRate>();

    [InverseProperty("Physician")]
    public virtual ICollection<PhysicianLocation> PhysicianLocations { get; set; } = new List<PhysicianLocation>();

    [InverseProperty("Physician")]
    public virtual ICollection<PhysicianNotification> PhysicianNotifications { get; set; } = new List<PhysicianNotification>();

    [InverseProperty("Physician")]
    public virtual ICollection<PhysicianRegion> PhysicianRegions { get; set; } = new List<PhysicianRegion>();

    [InverseProperty("Physician")]
    public virtual ICollection<RequestStatusLog> RequestStatusLogPhysicians { get; set; } = new List<RequestStatusLog>();

    [InverseProperty("TransToPhysician")]
    public virtual ICollection<RequestStatusLog> RequestStatusLogTransToPhysicians { get; set; } = new List<RequestStatusLog>();

    [InverseProperty("Physician")]
    public virtual ICollection<RequestWiseFile> RequestWiseFiles { get; set; } = new List<RequestWiseFile>();

    [InverseProperty("Physician")]
    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    [ForeignKey("RoleId")]
    [InverseProperty("Physicians")]
    public virtual Role? Role { get; set; }

    [InverseProperty("Physician")]
    public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();

    [InverseProperty("Provider")]
    public virtual ICollection<WeeklyTimeSheet> WeeklyTimeSheets { get; set; } = new List<WeeklyTimeSheet>();
}
