using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? LastName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string? Mobile { get; set; }

    public bool? IsMobile { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Street { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? City { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? State { get; set; }

    public int? RegionId { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ZipCode { get; set; }

    [Column("strMonth")]
    [StringLength(20)]
    [Unicode(false)]
    public string? StrMonth { get; set; }

    [Column("intYear")]
    public int? IntYear { get; set; }

    [Column("intDate")]
    public int? IntDate { get; set; }

    [StringLength(128)]
    [Unicode(false)]
    public string? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [StringLength(128)]
    [Unicode(false)]
    public string? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    public short? Status { get; set; }

    public bool? IsDeleted { get; set; }

    [Column("IP")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Ip { get; set; }

    public bool? IsRequestWithEmail { get; set; }

    public int? AspNetUserId { get; set; }

    [ForeignKey("AspNetUserId")]
    [InverseProperty("Users")]
    public virtual AspNetUser? AspNetUser { get; set; }
}
