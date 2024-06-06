using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

[Table("Business")]
public partial class Business
{
    [Key]
    public int BusinessId { get; set; }

    [StringLength(128)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string? Address1 { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Address2 { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? City { get; set; }

    public int? RegionId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ZipCode { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? PhoneNumber { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? FaxNumber { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    public short? Status { get; set; }

    [Column("IP")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Ip { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsRegistered { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("BusinessCreatedByNavigations")]
    public virtual AspNetUser? CreatedByNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("BusinessModifiedByNavigations")]
    public virtual AspNetUser? ModifiedByNavigation { get; set; }

    [ForeignKey("RegionId")]
    [InverseProperty("Businesses")]
    public virtual Region? Region { get; set; }

    [InverseProperty("Business")]
    public virtual ICollection<RequestBusiness> RequestBusinesses { get; set; } = new List<RequestBusiness>();
}
