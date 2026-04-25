using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Plan_It.Models
{
    public class PlanViewModel : IValidatableObject
    {
        public int PlanId { get; set; }

        [Required(ErrorMessage = "Başlık zorunludur.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Başlık 2-50 karakter arasında olmalıdır.")]
        [Display(Name = "Başlık")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Son tarih zorunludur.")]
        [DataType(DataType.Date)]
        [Display(Name = "Son Tarih")]
        public DateTime Deadline { get; set; }

        [Display(Name = "Durum")]
        public PlanStatus Status { get; set; } = PlanStatus.NotStarted;

        [Required(ErrorMessage = "Kategori seçilmelidir.")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir kategori seçiniz.")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Öncelik seçilmelidir.")]
        [EnumDataType(typeof(PriorityLevel), ErrorMessage = "Geçerli bir öncelik seçiniz.")]
        [Display(Name = "Öncelik")]
        public PriorityLevel Priority { get; set; }

        public IEnumerable<SelectListItem>? Categories { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Only enforce future-deadline on create (PlanId == 0).
            // Editing an existing plan may legitimately need a past date untouched.
            if (PlanId == 0 && Deadline.Date < DateTime.UtcNow.Date)
            {
                yield return new ValidationResult(
                    "Son tarih bugün veya ileri bir tarih olmalıdır.",
                    new[] { nameof(Deadline) });
            }
        }
    }
}
