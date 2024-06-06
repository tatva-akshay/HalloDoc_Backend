using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

[Table("Role")]
public partial class Role
{
    [Key]
    public int RoleId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public short AccountType { get; set; }

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

    [Column("IP")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Ip { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<EmailLog> EmailLogs { get; set; } = new List<EmailLog>();

    [InverseProperty("Role")]
    public virtual ICollection<Physician> Physicians { get; set; } = new List<Physician>();

    [InverseProperty("Role")]
    public virtual ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();

    [InverseProperty("Role")]
    public virtual ICollection<Smslog> Smslogs { get; set; } = new List<Smslog>();
}
