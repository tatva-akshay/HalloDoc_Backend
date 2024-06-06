using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

[Table("RequestClosed")]
public partial class RequestClosed
{
    [Key]
    public int RequestClosedId { get; set; }

    public int RequestId { get; set; }

    public int RequestStatusLogId { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? PhyNotes { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? ClientNotes { get; set; }

    [Column("IP")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Ip { get; set; }

    [ForeignKey("RequestId")]
    [InverseProperty("RequestCloseds")]
    public virtual Request Request { get; set; } = null!;
}
