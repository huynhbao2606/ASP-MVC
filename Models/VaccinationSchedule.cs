using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ASP_MVC.Models
{
    public class VaccinationSchedule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        [DisplayName("Vaccination Dates")]
        public string VaccinationDates { get; set; } = "";

        [DisplayName("Vaccine")]
        public int VaccineId { get; set; }
        [ValidateNever]
        public Vaccine Vaccine { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
