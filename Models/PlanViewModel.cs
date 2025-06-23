using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Plan_It.Models
{
    public class PlanViewModel
    {
        public int PlanId { get; set; }

        [Required(ErrorMessage = "Başlık zorunludur.")]
        [StringLength(100)]
        [Display(Name = "Başlık")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Son tarih zorunludur.")]
        [DataType(DataType.Date)]
        [Display(Name = "Son Tarih")]
        public DateTime Deadline { get; set; }

        [Display(Name = "Durum")]
        public PlanStatus Status { get; set; } = PlanStatus.NotStarted;

        [Required(ErrorMessage = "Kategori seçilmelidir.")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem>? Categories { get; set; }
    }
}