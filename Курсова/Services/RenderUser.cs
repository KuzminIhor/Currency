using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CurrencyApp.Core;
using CurrencyApp.Interfaces;

namespace CurrencyApp.Helpers
{
	public class RenderUser: IRenderDataTableRows
	{
		public void GetRows(DataTable dt)
		{
			using (DBAppContext db = new DBAppContext())
			{
				var users = db.Users.Include(u => u.Bank).Where(u => u.Id > 2).ToList();

				var rowId = 1;
				foreach (var user in users)
				{
					dt.Rows.Add(rowId++, user.Id, user.LastName, user.FirstName, user.Bank.BankName);
				}
			}
		}
	}
}