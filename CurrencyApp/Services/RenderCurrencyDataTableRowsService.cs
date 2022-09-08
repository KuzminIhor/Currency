using System.Data;
using System.Linq;
using CurrencyApp.Core;
using CurrencyApp.Interfaces;
using CurrencyApp.Repositories.Interfaces;

namespace CurrencyApp.Helpers
{
	public class RenderCurrencyDataTableRowsService: IRenderDataTableRows
	{
		private readonly ICurrencyRepository currencyRepository;

		public RenderCurrencyDataTableRowsService()
		{
			currencyRepository = ServiceLocator.Get<ICurrencyRepository>();
		}

		public void GetRows(DataTable dt)
		{
			var currencies = currencyRepository.GetCurrencies();
			var rowId = 1;

			foreach (var currency in currencies)
			{
				dt.Rows.Add(rowId++, currency.Id, currency.CurrencyName);
			}
		}
	}
}