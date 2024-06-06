using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

public partial class OrderDetail
{
    [Key]
    public int Id { get; set; }

    public int? VendorId { get; set; }

    public int? RequestId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? FaxNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? BusinessContact { get; set; }

    [Unicode(false)]
    public string? Prescription { get; set; }

    public int? NoOfRefill { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CreatedBy { get; set; }

    [ForeignKey("RequestId")]
    [InverseProperty("OrderDetails")]
    public virtual Request? Request { get; set; }
}
