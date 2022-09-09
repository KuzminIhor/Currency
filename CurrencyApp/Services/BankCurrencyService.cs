using System;
using CurrencyApp.Core;
using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Interfaces;
using CurrencyApp.Model;
using CurrencyApp.Repositories.Interfaces;

namespace CurrencyApp.Services
{
	public class BankCurrencyService: IBankCurrencyService
	{
		private readonly IBankCurrencyFieldsValidator validator;
		private readonly IAddBankCurrencyProcess addingProcess;
		private readonly IUpdateBankCurrencyProcess updatingProcess;

		private readonly IBankCurrencyRepository bankCurrencyRepository;

		public BankCurrencyService()
		{
			validator = ServiceLocator.Get<IBankCurrencyFieldsValidator>();
			addingProcess = ServiceLocator.Get<IAddBankCurrencyProcess>();
			updatingProcess = ServiceLocator.Get<IUpdateBankCurrencyProcess>();

			bankCurrencyRepository = ServiceLocator.Get<IBankCurrencyRepository>();
		}

		public object AddBankCurrency(Currency currency, string convertation)
		{
			validator.SetNext(addingProcess);

			return validator.AddBankCurrencyHandle(currency, convertation);
		}

		public void UpdateBankCurrency(int bankCurrencyId, string convertation)
		{
			validator.SetNext(updatingProcess);

			validator.UpdateBankCurrencyHandle(bankCurrencyId, convertation);
		}

		public void RemoveBankCurrency(int bankCurrencyId)
		{
			var bankCurrency = bankCurrencyRepository.GetBankCurrency(bankCurrencyId);

			bankCurrencyRepository.RemoveBankCurrency(bankCurrency);
		}
	}
}