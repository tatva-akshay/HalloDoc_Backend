﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

[Table("HealthProfessionalType")]
public partial class HealthProfessionalType
{
    [Key]
    public int HealthProfessionalId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ProfessionName { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    [InverseProperty("ProfessionNavigation")]
    public virtual ICollection<HealthProfessional> HealthProfessionals { get; set; } = new List<HealthProfessional>();
}
