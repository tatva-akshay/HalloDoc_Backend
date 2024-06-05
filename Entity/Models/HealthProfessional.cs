using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

public partial class HealthProfessional
{
    [Key]
    public int VendorId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string VendorName { get; set; } = null!;

    public int? Profession { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FaxNumber { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string? Address { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? City { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? State { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Zip { get; set; }

    public int? RegionId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? PhoneNumber { get; set; }

    public bool? IsDeleted { get; set; }

    [Column("IP")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Ip { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? BusinessContact { get; set; }

    [ForeignKey("Profession")]
    [InverseProperty("HealthProfessionals")]
    public virtual HealthProfessionalType? ProfessionNavigation { get; set; }
}
