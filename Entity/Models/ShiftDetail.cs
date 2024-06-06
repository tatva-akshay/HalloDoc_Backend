using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

[Table("ShiftDetail")]
public partial class ShiftDetail
{
    [Key]
    public int ShiftDetailId { get; set; }

    public int ShiftId { get; set; }

    public DateOnly ShiftDate { get; set; }

    public int? RegionId { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public short Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastRunningDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? EventId { get; set; }

    public int? Modifiedby { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsSync { get; set; }

    [ForeignKey("Modifiedby")]
    [InverseProperty("ShiftDetails")]
    public virtual AspNetUser? ModifiedbyNavigation { get; set; }

    [ForeignKey("ShiftId")]
    [InverseProperty("ShiftDetails")]
    public virtual Shift Shift { get; set; } = null!;

    [InverseProperty("ShiftDetail")]
    public virtual ICollection<ShiftDetailRegion> ShiftDetailRegions { get; set; } = new List<ShiftDetailRegion>();
}
