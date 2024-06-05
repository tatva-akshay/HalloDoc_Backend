using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

[Table("RequestClient")]
public partial class RequestClient
{
    [Key]
    public int RequestClientId { get; set; }

    public int RequestId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? LastName { get; set; }

    [StringLength(23)]
    [Unicode(false)]
    public string? PhoneNumber { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Location { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Address { get; set; }

    public int? RegionId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? NotiMobile { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? NotiEmail { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Notes { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("strMonth")]
    [StringLength(20)]
    [Unicode(false)]
    public string? StrMonth { get; set; }

    [Column("intYear")]
    public int? IntYear { get; set; }

    [Column("intDate")]
    public int? IntDate { get; set; }

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

    [StringLength(10)]
    [Unicode(false)]
    public string? ZipCode { get; set; }

    public short? CommunicationType { get; set; }

    public short? RemindReservationCount { get; set; }

    public short? RemindHouseCallCount { get; set; }

    public short? IsSetFollowupSent { get; set; }

    [Column("IP")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Ip { get; set; }

    public short? IsReservationReminderSent { get; set; }

    [Column(TypeName = "decimal(11, 8)")]
    public decimal? Latitude { get; set; }

    [Column(TypeName = "decimal(11, 8)")]
    public decimal? Longitude { get; set; }

    [ForeignKey("RegionId")]
    [InverseProperty("RequestClients")]
    public virtual Region? Region { get; set; }

    [ForeignKey("RequestId")]
    [InverseProperty("RequestClients")]
    public virtual Request Request { get; set; } = null!;
}
