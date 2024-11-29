using System.ComponentModel.DataAnnotations;

namespace Plan_It.Models
{
    public class Plan
    {
        [Key]
        public int PlanId { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime Deadline { get; set; }

    }
}