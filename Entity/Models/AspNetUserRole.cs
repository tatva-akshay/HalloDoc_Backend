﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

[PrimaryKey("UserId", "RoleId")]
public partial class AspNetUserRole
{
    [Key]
    [StringLength(128)]
    [Unicode(false)]
    public string UserId { get; set; } = null!;

    [Key]
    [StringLength(128)]
    [Unicode(false)]
    public string RoleId { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("AspNetUserRoles")]
    public virtual AspNetUser User { get; set; } = null!;
}
