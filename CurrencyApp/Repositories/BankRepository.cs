using CurrencyApp.Core;
using CurrencyApp.Repositories.Interfaces;

namespace CurrencyApp.Repositories
{
	public class BankRepository: IBankRepository
	{
		public DBAppContext db { get; private set; }

		public BankRepository(DBAppContext db)
		{
			this.db = db;
		}
	}
}