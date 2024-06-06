﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

[Table("Shift")]
public partial class Shift
{
    [Key]
    public int ShiftId { get; set; }

    public int PhysicianId { get; set; }

    public DateOnly StartDate { get; set; }

    [StringLength(7)]
    [Unicode(false)]
    public string? WeekDays { get; set; }

    public int? RepeatUpto { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column("IP")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Ip { get; set; }

    public int CreatedBy { get; set; }

    public bool? IsRepeat { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("Shifts")]
    public virtual AspNetUser CreatedByNavigation { get; set; } = null!;

    [ForeignKey("PhysicianId")]
    [InverseProperty("Shifts")]
    public virtual Physician Physician { get; set; } = null!;

    [InverseProperty("Shift")]
    public virtual ICollection<ShiftDetail> ShiftDetails { get; set; } = new List<ShiftDetail>();
}
