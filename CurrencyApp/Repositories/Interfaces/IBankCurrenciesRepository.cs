﻿using System;
using System.Collections.Generic;
using CurrencyApp.Model;

namespace CurrencyApp.Repositories.Interfaces
{
	public interface IBankCurrencyRepository
	{
		public List<BankCurrency> GetBankCurrenciesInCurrentDateRange(DateTime dateFrom, DateTime dateTo);
		public void AddBankCurrency(Currency currency, Bank bank, double uahConvertation);
	}
}