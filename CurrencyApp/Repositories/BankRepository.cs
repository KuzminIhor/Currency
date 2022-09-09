using System.Collections.Generic;
using System.Linq;
using CurrencyApp.Core;
using CurrencyApp.Model;
using CurrencyApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApp.Repositories
{
	public class BankRepository: IBankRepository
	{
		public readonly DBAppContext db;

		public BankRepository(DBAppContext db)
		{
			this.db = db;
		}

		public List<Bank> GetBanks()
		{
			return db.Banks.ToList();
		}

		public Bank GetBank(int bankId)
		{
			return db.Banks.FirstOrDefault(b => b.Id == bankId);
		}

		public Bank GetBankByUser(int userId)
		{
			return db.Users.Include(u => u.Bank).FirstOrDefault(u => u.Id == userId)
				.Bank;
		}

		public void AddBank(Bank bank)
		{
			db.Banks.Add(bank);
			db.SaveChanges();
		}

		public void UpdateBank(Bank bank)
		{
			db.Banks.Update(bank);
			db.SaveChanges();
		}

		public void RemoveBank(Bank bank)
		{
			db.Banks.Remove(bank);
			db.SaveChanges();
		}
	}
}