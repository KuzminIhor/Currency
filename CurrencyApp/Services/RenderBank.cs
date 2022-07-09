using System.Data;
using System.Linq;
using CurrencyApp.Core;
using CurrencyApp.Interfaces;

namespace CurrencyApp.Services
{
	public class RenderBank: IRenderDataTableRows
	{
		public void GetRows(DataTable dt)
		{
			using (DBAppContext db = new DBAppContext())
			{
				var banks = db.Banks.ToList();

				var rowId = 1;
				foreach (var bank in banks)
				{
					dt.Rows.Add(rowId++, bank.Id, bank.BankName);
				}
			}
		}
	}
}