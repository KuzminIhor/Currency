using System.Linq;
using CurrencyApp.Core;
using CurrencyApp.Model;
using CurrencyApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApp.Repositories
{
	public class BankRepository: IBankRepository
	{
		public DBAppContext db { get; private set; }

		public BankRepository(DBAppContext db)
		{
			this.db = db;
		}

		public Bank GetBankByUserId(int userId)
		{
			return db.Users.Include(u => u.Bank).FirstOrDefault(u => u.Id == userId)
				.Bank;
		}
	}
}