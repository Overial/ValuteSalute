using ValuteSalute.Domain.Entities;

namespace ValuteSalute.Models
{
	public class CurrencyItemsCacherModel
	{
		public Dictionary<string, CurrencyItem>? CurrencyItemsCache { get; set; }
		public int TTL { get; set; }

		public CurrencyItemsCacherModel()
		{
			this.CurrencyItemsCache = null;
			this.TTL = 10;
		}
	}
}
