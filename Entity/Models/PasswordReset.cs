using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

[Table("PasswordReset")]
public partial class PasswordReset
{
    [Key]
    public int Id { get; set; }

    [Unicode(false)]
    public string Token { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public bool? IsUpdated { get; set; }
}
