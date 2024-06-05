using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

public partial class AspNetRole
{
    [Key]
    [StringLength(128)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(256)]
    [Unicode(false)]
    public string Name { get; set; } = null!;
}
