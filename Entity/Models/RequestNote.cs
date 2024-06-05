﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

public partial class RequestNote
{
    [Key]
    public int RequestNotesId { get; set; }

    public int RequestId { get; set; }

    [Column("strMonth")]
    [StringLength(20)]
    [Unicode(false)]
    public string? StrMonth { get; set; }

    [Column("intYear")]
    public int? IntYear { get; set; }

    [Column("intDate")]
    public int? IntDate { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? PhysicianNotes { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? AdminNotes { get; set; }

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

    [StringLength(500)]
    [Unicode(false)]
    public string? AdministrativeNotes { get; set; }

    [ForeignKey("RequestId")]
    [InverseProperty("RequestNotes")]
    public virtual Request Request { get; set; } = null!;
}
