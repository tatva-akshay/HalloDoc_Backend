using System;
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

    [StringLength(128)]
    [Unicode(false)]
    public string? AspNetUserId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? LastName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

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

    public bool? IsAgreementDoc { get; set; }

    public bool? IsBackgroundDoc { get; set; }

    public bool? IsTrainingDoc { get; set; }

    public bool? IsNonDisclosureDoc { get; set; }

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

    [StringLength(128)]
    [Unicode(false)]
    public string CreatedBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [StringLength(128)]
    [Unicode(false)]
    public string? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    public short? Status { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string BusinessName { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string BusinessWebsite { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public int? RoleId { get; set; }

    [Column("NPINumber")]
    [StringLength(500)]
    [Unicode(false)]
    public string? Npinumber { get; set; }

    public bool? IsLicenseDoc { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Signature { get; set; }

    public bool? IsCredentialDoc { get; set; }

    public bool? IsTokenGenerate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SyncEmailAddress { get; set; }

    [ForeignKey("AspNetUserId")]
    [InverseProperty("PhysicianAspNetUsers")]
    public virtual AspNetUser? AspNetUser { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("PhysicianCreatedByNavigations")]
    public virtual AspNetUser CreatedByNavigation { get; set; } = null!;

    [ForeignKey("ModifiedBy")]
    [InverseProperty("PhysicianModifiedByNavigations")]
    public virtual AspNetUser? ModifiedByNavigation { get; set; }

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

    [InverseProperty("Physician")]
    public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();
}
