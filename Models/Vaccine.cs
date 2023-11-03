using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ASP_MVC.Models
{
    public class Vaccine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        [DisplayName("Country of Manufacture")]
        public string CountryOfManufacture { get; set; } = "";

        [Required]
        [DisplayName("Expiration Date")]

        public DateTime ExpirationDate { get; set; } = DateTime.Today;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be larger than zero")]
        public double Price { get; set; }

        [DisplayName("Type")]
        public int TypeId { get; set; }
        [ValidateNever]
        public Type Type { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
