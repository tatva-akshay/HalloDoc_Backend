using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

[Table("WeeklyTimeSheet")]
public partial class WeeklyTimeSheet
{
    [Key]
    public int TimeSheetId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int? Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    public int ProviderId { get; set; }

    public int? PayRateId { get; set; }

    public int? AdminId { get; set; }

    public bool? IsFinalized { get; set; }

    [Unicode(false)]
    public string? AdminNote { get; set; }

    public int? BonusAmount { get; set; }

    [ForeignKey("AdminId")]
    [InverseProperty("WeeklyTimeSheets")]
    public virtual Admin? Admin { get; set; }

    [ForeignKey("ProviderId")]
    [InverseProperty("WeeklyTimeSheets")]
    public virtual Physician Provider { get; set; } = null!;

    [InverseProperty("TimeSheet")]
    public virtual ICollection<WeeklyTimeSheetDetail> WeeklyTimeSheetDetails { get; set; } = new List<WeeklyTimeSheetDetail>();
}
