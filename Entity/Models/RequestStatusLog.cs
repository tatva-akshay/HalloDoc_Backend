﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

[Table("RequestStatusLog")]
public partial class RequestStatusLog
{
    [Key]
    public int RequestStatusLogId { get; set; }

    public int RequestId { get; set; }

    public short Status { get; set; }

    public int? PhysicianId { get; set; }

    public int? AdminId { get; set; }

    public int? TransToPhysicianId { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Notes { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column("IP")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Ip { get; set; }

    public bool? TransToAdmin { get; set; }

    [ForeignKey("AdminId")]
    [InverseProperty("RequestStatusLogs")]
    public virtual Admin? Admin { get; set; }

    [ForeignKey("PhysicianId")]
    [InverseProperty("RequestStatusLogPhysicians")]
    public virtual Physician? Physician { get; set; }

    [ForeignKey("RequestId")]
    [InverseProperty("RequestStatusLogs")]
    public virtual Request Request { get; set; } = null!;

    [ForeignKey("TransToPhysicianId")]
    [InverseProperty("RequestStatusLogTransToPhysicians")]
    public virtual Physician? TransToPhysician { get; set; }
}
