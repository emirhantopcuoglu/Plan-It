using System.ComponentModel.DataAnnotations;

namespace Plan_It.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Plan> Plans { get; set; } = new List<Plan>();
    }
}