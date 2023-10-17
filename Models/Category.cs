using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASP_MVC.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Can't Be Empty!!")]
        public string? Name { get; set; }

        [DisplayName("My Display Order")]
        [Required(ErrorMessage = "Display Can't Be Empty!!!")]
        public int DisplayOrder { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
