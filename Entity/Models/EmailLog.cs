using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

[Table("EmailLog")]
public partial class EmailLog
{
    [Key]
    [Column("EmailLogID")]
    public int EmailLogId { get; set; }

    [Unicode(false)]
    public string EmailTemplate { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string SubjectName { get; set; } = null!;

    [Column("EmailID")]
    [StringLength(200)]
    [Unicode(false)]
    public string EmailId { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string? ConfirmationNumber { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? FilePath { get; set; }

    public int? RoleId { get; set; }

    public int? RequestId { get; set; }

    public int? AdminId { get; set; }

    public int? PhysicianId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SentDate { get; set; }

    public bool? IsEmailSent { get; set; }

    public int? SentTries { get; set; }

    public int? Action { get; set; }

    [ForeignKey("RequestId")]
    [InverseProperty("EmailLogs")]
    public virtual Request? Request { get; set; }
}
