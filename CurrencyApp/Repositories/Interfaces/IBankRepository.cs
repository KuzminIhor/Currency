using System.Collections.Generic;
using CurrencyApp.Model;

namespace CurrencyApp.Repositories.Interfaces
{
	public interface IBankRepository
	{
		public List<Bank> GetBanks();
		public Bank GetBankByUserId(int userId);
	}
}