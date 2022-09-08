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

		private readonly IBankCurrencyRepository bankCurrencyRepository;

		public BankCurrencyService()
		{
			validator = ServiceLocator.Get<IBankCurrencyFieldsValidator>();
			addingProcess = ServiceLocator.Get<IAddBankCurrencyProcess>();

			bankCurrencyRepository = ServiceLocator.Get<IBankCurrencyRepository>();
		}

		public object AddBankCurrency(Currency currency, string convertation)
		{
			validator.SetNext(addingProcess);

			return validator.Handle(currency, convertation);
		}

		public void UpdateBankCurrency(string convertation, int bankCurrencyId)
		{
			validator.ValidateConvertation(convertation);

			var bankCurrency = bankCurrencyRepository.GetBankCurrency(bankCurrencyId);
			bankCurrency.UAHConvertation = Convert.ToDouble(convertation);
			bankCurrency.ModificationDate = DateTime.Now;

			bankCurrencyRepository.UpdateBankCurrency(bankCurrency);
		}

		public void RemoveBankCurrency(int bankCurrencyId)
		{
			var bankCurrency = bankCurrencyRepository.GetBankCurrency(bankCurrencyId);

			bankCurrencyRepository.RemoveBankCurrency(bankCurrency);
		}
	}
}