using System.ComponentModel.DataAnnotations;

namespace ValuteSalute.Domain.Entities
{
    public abstract class EntityBase
    {
        protected EntityBase() => DateAdded = DateTime.UtcNow;

        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Метатег Title")]
        public string? MetaTitle { get; set; }

        [DataType(DataType.Time)]
        public DateTime DateAdded { get; set; }
    }
}
