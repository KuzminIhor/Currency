using System.Collections.Generic;
using CurrencyApp.Model;

namespace CurrencyApp.Repositories.Interfaces
{
	public interface IBankRepository
	{
		public List<Bank> GetBanks();
		public Bank GetBank(int bankId);
		public Bank GetBankByUser(int userId);
		public void AddBank(Bank bank);
		public void UpdateBank(Bank bank);
		public void RemoveBank(Bank bank);
	}
}