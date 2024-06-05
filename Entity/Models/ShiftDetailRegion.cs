﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

[Table("ShiftDetailRegion")]
public partial class ShiftDetailRegion
{
    [Key]
    public int ShiftDetailRegionId { get; set; }

    public int ShiftDetailId { get; set; }

    public int RegionId { get; set; }

    public bool? IsDeleted { get; set; }

    [ForeignKey("RegionId")]
    [InverseProperty("ShiftDetailRegions")]
    public virtual Region Region { get; set; } = null!;

    [ForeignKey("ShiftDetailId")]
    [InverseProperty("ShiftDetailRegions")]
    public virtual ShiftDetail ShiftDetail { get; set; } = null!;
}
