using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ValuteSalute.Domain.Entities
{
    public class CurrencyItem
        : EntityBase
    {
        [Required]
        [Display(Name = "Дата")]
        [JsonProperty("Date")]
        public string Date { get; set; }

        [Required]
        [Display(Name = "Предыдущая дата")]
        [JsonProperty("PreviousDate")]
        public string PreviousDate { get; set; }

        [Required]
        [Display(Name = "Предыдущий URL")]
        [JsonProperty("PreviousURL")]
        public string PreviousUrl { get; set; }

        [Required]
        [Display(Name = "Отметка времени")]
        [JsonProperty("Timestamp")]
        public string Timestamp { get; set; }

        [Required]
        [Display(Name = "Идентификатор валюты")]
        [JsonProperty("ID")]
        public string CurrencyId { get; set; }

        [Required]
        [Display(Name = "Численный код")]
        [JsonProperty("NumCode")]
        public string NumCode { get; set; }

        [Required]
        [Display(Name = "Символьный код")]
        [JsonProperty("CharCode")]
        public string CharCode { get; set; }

        [Required]
        [Display(Name = "Номинал")]
        [JsonProperty("Nominal")]
        public string Nominal { get; set; }

        [Required]
        [Display(Name = "Название")]
        [JsonProperty("Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Текущий курс")]
        [JsonProperty("Value")]
        public string Value { get; set; }

        [Required]
        [Display(Name = "Предыдущий курс")]
        [JsonProperty("Previous")]
        public string Previous { get; set; }
    }
}
