using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ASP_MVC.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = "";

        public string? Description { get; set; }

        [Required]
        public string ISBN { get; set; } = "";

        [Required]
        public string Author { get; set; } = "";

        [Required]
        public double Price { get; set; }

        public double? Price50 { get; set; }

        public double? Price100 { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; } = "default.jpg";

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        public int CoverTypeId { get; set; }
        [ForeignKey("CoverTypeId")]
        [ValidateNever]
        public CoverType CoverType { get; set; }
        public int VaccineId { get; set; }
        [ForeignKey("VaccineId")]
        [ValidateNever]
        public Vaccine Vaccine { get; set; }
        
    }
}
