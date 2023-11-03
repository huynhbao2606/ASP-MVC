using System.ComponentModel.DataAnnotations;

namespace ASP_MVC.Models
{
    public class Type
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
