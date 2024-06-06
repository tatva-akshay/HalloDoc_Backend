using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

public partial class BlockRequest
{
    [Key]
    public int BlockRequestId { get; set; }

    [Unicode(false)]
    public string? PhoneNumber { get; set; }

    [Unicode(false)]
    public string? Email { get; set; }

    [Unicode(false)]
    public string? Reason { get; set; }

    [Column("IP")]
    [Unicode(false)]
    public string? Ip { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public DateOnly? ModifiedDate { get; set; }

    public int RequestId { get; set; }

    public bool? IsActive { get; set; }

    [ForeignKey("RequestId")]
    [InverseProperty("BlockRequests")]
    public virtual Request Request { get; set; } = null!;
}
