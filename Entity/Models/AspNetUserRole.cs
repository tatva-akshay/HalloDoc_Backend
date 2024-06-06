using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

[PrimaryKey("UserId", "RoleId")]
public partial class AspNetUserRole
{
    [Key]
    public int UserId { get; set; }

    [Key]
    public int RoleId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("AspNetUserRoles")]
    public virtual AspNetRole Role { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("AspNetUserRoles")]
    public virtual AspNetUser User { get; set; } = null!;
}
