using System.Data;
using System.Linq;
using CurrencyApp.Core;
using CurrencyApp.Interfaces;

namespace CurrencyApp.Helpers
{
	public class RenderCurrency: IRenderCurrency
	{
		public void GetRows(DataTable dt)
		{
			using (DBAppContext db = new DBAppContext())
			{
				var currencies = db.Currencies.ToList();
				var rowId = 1;

				foreach (var currency in currencies)
				{
					dt.Rows.Add(rowId++, currency.Id, currency.CurrencyName);
				}
			}
		}
	}
}