using Microsoft.AspNetCore.Mvc.Rendering;
using ValuteSalute.Domain.Entities;

namespace ValuteSalute.Models
{
    public class CurrencyViewModel
    {
        public IEnumerable<KeyValuePair<string, CurrencyItem>> CurrencyData { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SelectList CurrencyIdSelectList { get; set; }
    }
}
