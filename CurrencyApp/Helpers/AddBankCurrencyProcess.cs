using System;
using System.Linq;
using CurrencyApp.Core;
using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Interfaces;
using CurrencyApp.Model;
using CurrencyApp.Model.Abstracts;
using CurrencyApp.Model.Enums;
using CurrencyApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApp.Helpers
{
	public class AddBankCurrencyProcess: AbstractAddBankCurrencyHandler, IAddBankCurrencyProcess
	{
		private readonly IBankRepository bankRepository;
		private readonly ICurrencyRepository currencyRepository;
		private readonly IBankCurrencyRepository bankCurrencyRepository;

		public AddBankCurrencyProcess()
		{
			bankRepository = ServiceLocator.Get<IBankRepository>();
			currencyRepository = ServiceLocator.Get<ICurrencyRepository>();
			bankCurrencyRepository = ServiceLocator.Get<IBankCurrencyRepository>();
		}

		public override object Handle(Currency currency, string convertation)
		{
			Add(currency, convertation);

			return FormType.BankUserForm;
		}

		public void Add(Currency currency, string convertation)
		{
			var bankInDb = bankRepository.GetBankByUserId(CurrentUser.GetInstance().Id);
			var currencyInDb = currencyRepository.GetCurrencyById(currency.Id);

			bankCurrencyRepository.AddBankCurrency(currencyInDb, bankInDb, Convert.ToDouble(convertation));
		}
	}
}