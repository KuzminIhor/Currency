using System;
using CurrencyApp.Core;
using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Model;
using CurrencyApp.Model.Abstracts;
using CurrencyApp.Model.Enums;
using CurrencyApp.Repositories.Interfaces;

namespace CurrencyApp.Helpers
{
	public class AddBankCurrencyProcess: AbstractBankCurrencyHandler, IAddBankCurrencyProcess
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

		public override object AddBankCurrencyHandle(Currency currency, string convertation)
		{
			Add(currency, convertation);

			return FormType.BankUserForm;
		}

		public void Add(Currency currency, string convertation)
		{
			var bankInDb = bankRepository.GetBankByUser(CurrentUser.GetInstance().Id);
			var currencyInDb = currencyRepository.GetCurrencyById(currency.Id);

			bankCurrencyRepository.AddBankCurrencyForm(currencyInDb, bankInDb, Convert.ToDouble(convertation));
		}
	}
}