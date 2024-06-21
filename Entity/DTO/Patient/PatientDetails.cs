namespace Entity.DTO.Patient;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Entity.Models;
using Microsoft.AspNetCore.Http;

public class PatientDetails
{
    public bool isPatientExist { get; set; } = false;

    [Column("symptoms")]
    [StringLength(500)]
    public string? Symptoms { get; set; }

    [Column("FirstName")]
    [StringLength(50)]
    [Required]

    public string FirstName { get; set; } = null!;

    [Column("LastName")]
    [StringLength(50)]
    [Required]
    public string LastName { get; set; }

    [Column("bdate")]
    [Required(ErrorMessage = "Birth Date is Required")]
    public DateTime Bdate { get; set; }

    [StringLength(256)]
    [Required]
    public string Email { get; set; }

    [Column(TypeName = "character varying")]
    public string? PasswordHash { get; set; }

    [StringLength(20)]
    [Required]
    public string Mobile { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }   

    [StringLength(50)]
    [Required]
    public string Street { get; set; }

    [StringLength(100)]
    [Required]
    public string? City { get; set; }

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
