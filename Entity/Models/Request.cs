using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

[Table("Request")]
public partial class Request
{
    [Key]
    public int RequestId { get; set; }

    public int RequestTypeId { get; set; }

    public int? UserId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FirstName { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? LastName { get; set; }

    [StringLength(23)]
    [Unicode(false)]
    public string? PhoneNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    public short Status { get; set; }

    public int? PhysicianId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ConfirmationNumber { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public bool? IsDeleted { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? DeclinedBy { get; set; }

    public bool IsUrgentEmailSent { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastWellnessDate { get; set; }

    public bool? IsMobile { get; set; }

    public short? CallType { get; set; }

    public bool? CompletedByPhysician { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastReservationDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AcceptedDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? RelationName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CaseNumber { get; set; }

    [Column("IP")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Ip { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CaseTag { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CaseTagPhysician { get; set; }

    [StringLength(128)]
    [Unicode(false)]
    public string? PatientAccountId { get; set; }

    public int? CreatedUserId { get; set; }

    [InverseProperty("Request")]
    public virtual ICollection<EmailLog> EmailLogs { get; set; } = new List<EmailLog>();

    [ForeignKey("PhysicianId")]
    [InverseProperty("Requests")]
    public virtual Physician? Physician { get; set; }

    [InverseProperty("Request")]
    public virtual ICollection<RequestBusiness> RequestBusinesses { get; set; } = new List<RequestBusiness>();

    [InverseProperty("Request")]
    public virtual ICollection<RequestClient> RequestClients { get; set; } = new List<RequestClient>();

    [InverseProperty("Request")]
    public virtual ICollection<RequestClosed> RequestCloseds { get; set; } = new List<RequestClosed>();

    [InverseProperty("Request")]
    public virtual ICollection<RequestConcierge> RequestConcierges { get; set; } = new List<RequestConcierge>();

    [InverseProperty("Request")]
    public virtual ICollection<RequestNote> RequestNotes { get; set; } = new List<RequestNote>();

    [InverseProperty("Request")]
    public virtual ICollection<RequestStatusLog> RequestStatusLogs { get; set; } = new List<RequestStatusLog>();

    [InverseProperty("Request")]
    public virtual ICollection<RequestWiseFile> RequestWiseFiles { get; set; } = new List<RequestWiseFile>();

    [ForeignKey("UserId")]
    [InverseProperty("Requests")]
    public virtual User? User { get; set; }
}
