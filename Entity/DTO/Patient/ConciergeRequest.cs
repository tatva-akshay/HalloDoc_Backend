using System.ComponentModel.DataAnnotations;
using Entity.Models;

namespace Entity.DTO.Patient;

public class ConciergeRequest
{
    
        [StringLength(50)]
        public string? YFirstName { get; set; } 

        [StringLength(50)]
        public string? YLastName { get; set; }

        [StringLength(256)]
        public string? YEmail { get; set; }

        [StringLength(20)]

        public string? YMobile { get; set; }

        [StringLength(50)]
        public string? Hotelpropertyname { get; set; }

        [StringLength(50)]
        public string? CStreet { get; set; }

        [StringLength(100)]
        public string? CCity { get; set; }

        public int CregionId { get; set; }
        [StringLength(50)]
        public string? CState { get; set; }

        public decimal? CZipCode { get; set; }

        [StringLength(500)]
        public string? Symptoms { get; set; }

        [StringLength(50)]
        [Required]
        public string FirstName { get; set; } = null!;

        [StringLength(50)]
        public string? LastName { get; set; }

        [Required(ErrorMessage ="Date is Reuired")]
        public DateOnly? Bdate { get; set; }

        [StringLength(256)]
        [Required]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Mobile { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string? Street { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(50)]
        public string? State { get; set; }

        [Required]
        public int regionId { get; set; }

        public List<Region> allRegion { get; set; }

        public decimal? ZipCode { get; set; }

        [StringLength(20)]
        public string? Room { get; set; }
}
