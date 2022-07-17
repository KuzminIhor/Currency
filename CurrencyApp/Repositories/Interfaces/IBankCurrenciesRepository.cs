using System;
using System.Collections.Generic;
using CurrencyApp.Model;

namespace CurrencyApp.Repositories.Interfaces
{
	public interface IBankCurrenciesRepository
	{
		public List<BankCurrency> GetBankCurrenciesInCurrentDateRange(DateTime dateFrom, DateTime dateTo);
	}
}