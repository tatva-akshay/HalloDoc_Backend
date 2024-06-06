using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

[Table("Concierge")]
public partial class Concierge
{
    [Key]
    public int ConciergeId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ConciergeName { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string? Address { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Street { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string City { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string State { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ZipCode { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public int? RegionId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? RoleId { get; set; }

    [ForeignKey("RegionId")]
    [InverseProperty("Concierges")]
    public virtual Region? Region { get; set; }

    [InverseProperty("Concierge")]
    public virtual ICollection<RequestConcierge> RequestConcierges { get; set; } = new List<RequestConcierge>();
}
