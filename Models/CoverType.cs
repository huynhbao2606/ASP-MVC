using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASP_MVC.Models
{
    public class CoverType
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Cover Type")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
