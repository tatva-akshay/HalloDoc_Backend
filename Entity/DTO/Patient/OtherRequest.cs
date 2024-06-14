using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Entity.Models;
using Microsoft.AspNetCore.Http;

namespace Entity.DTO.Patient;

public class OtherRequest
{
    public int RequestType { get; set; }

    [StringLength(50)]
    public string YFirstName { get; set; }

    [StringLength(50)]
    public string? YLastName { get; set; }

    [StringLength(256)]
    // [RegularExpression("//")]
    public string? YEmail { get; set; }

    [DefaultValue(0)]
    public string? YMobile { get; set; }

    [StringLength(100)]
    public string? RelationName { get; set; }

    [StringLength(500)]
    public string? Symptoms { get; set; }

    [StringLength(50)]
    [Required]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string? LastName { get; set; }

    public DateTime Bdate { get; set; }

    [StringLength(256)]
    [Required]
    public string Email { get; set; }

    [Required]
    public string Mobile { get; set; }

    [StringLength(50)]
    [Required]
    public string Street { get; set; }

    [StringLength(100)]
    [Required]
    public string City { get; set; }

    [StringLength(50)]
    public string? State { get; set; }

    [Required]
    public int regionId { get; set; }

    public List<Region>? allRegion { get; set; }

    [Required]
    public decimal ZipCode { get; set; }

    [StringLength(20)]
    public string? Room { get; set; }

    public List<IFormFile>? File { get; set; }
}
