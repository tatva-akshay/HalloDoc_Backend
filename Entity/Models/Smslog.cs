using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

[Table("SMSLog")]
public partial class Smslog
{
    [Key]
    [Column("SMSLogID", TypeName = "decimal(9, 0)")]
    public decimal SmslogId { get; set; }

    [Column("SMSTemplate")]
    [Unicode(false)]
    public string Smstemplate { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string MobileNumber { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string? ConfirmationNumber { get; set; }

    public int? RoleId { get; set; }

    public int? AdminId { get; set; }

    public int? RequestId { get; set; }

    public int? PhysicianId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SentDate { get; set; }

    [Column("IsSMSSent")]
    public bool? IsSmssent { get; set; }

    public int SentTries { get; set; }

    public int? Action { get; set; }
}
