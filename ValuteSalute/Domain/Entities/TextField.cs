using System.ComponentModel.DataAnnotations;

namespace ValuteSalute.Domain.Entities
{
    public class TextField
        : EntityBase
    {
        [Required]
        [Display(Name = "Кодовое слово")]
        public string CodeWord { get; set; }

        [Display(Name = "Заголовок")]
        public string? Title { get; set; }

        [Display(Name = "Содержание")]
        public string? Text { get; set; }
    }
}
