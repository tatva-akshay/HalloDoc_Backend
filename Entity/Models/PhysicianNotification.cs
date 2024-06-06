using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

[Table("PhysicianNotification")]
public partial class PhysicianNotification
{
    [Key]
    public int Id { get; set; }

    public int PhysicianId { get; set; }

    public bool IsNotificationStopped { get; set; }

    [ForeignKey("PhysicianId")]
    [InverseProperty("PhysicianNotifications")]
    public virtual Physician Physician { get; set; } = null!;
}
