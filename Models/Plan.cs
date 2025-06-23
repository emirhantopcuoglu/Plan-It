using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Plan_It.Models
{
    public class Plan
    {
        [Key]
        public int PlanId { get; set; }

        [Required(ErrorMessage = "Başlık zorunludur.")]
        [StringLength(50, ErrorMessage = "Başlık en fazla 50 karakter olabilir.")]
        [DisplayName("Başlık")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Durum")]
        public PlanStatus Status { get; set; } = PlanStatus.NotStarted;

        [DisplayName("Oluşturulma Tarihi")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Son tarih zorunludur.")]
        [DisplayName("Son Tarih")]
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        [Required(ErrorMessage = "Kategori seçilmelidir.")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser? User { get; set; }
    }
}