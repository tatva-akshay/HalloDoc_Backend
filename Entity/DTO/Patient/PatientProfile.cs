using System.ComponentModel.DataAnnotations;
using Entity.DTO.General;
using Entity.Models;

namespace Entity.DTO.Patient;

public class PatientProfile
{
    public int UserId { get; set; }

    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    public string? LastName { get; set; }

    [StringLength(50)]
    [RegularExpression(@"^[\w-\.]+@([\w -]+\.)+[\w-]{2,4}$", ErrorMessage = "Please enter valid Email")]
    public string Email { get; set; } = null!;

    [StringLength(20)]
    public string? Mobile { get; set; }

    public DateTime? Bdate { get; set; }

    [StringLength(100)]
    public string? Street { get; set; }

    [StringLength(100)]
    public string? City { get; set; }

    public string? State { get; set; }

    [Required]
    public int regionId { get; set; }

    public List<RegionsDropDown> allRegion { get; set; }

    public string? ZipCode { get; set; }
}
