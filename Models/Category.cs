using System.ComponentModel.DataAnnotations;

namespace Plan_It.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Kategori adı 2-40 karakter arasında olmalıdır.")]
        [Display(Name = "Kategori Adı")]
        public string Name { get; set; } = string.Empty;

        public ICollection<Plan> Plans { get; set; } = new List<Plan>();
    }
}
