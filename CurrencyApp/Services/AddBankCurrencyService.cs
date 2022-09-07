using CurrencyApp.Core;
using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Interfaces;
using CurrencyApp.Model;
using CurrencyApp.Model.Interfaces.Helpers;

namespace CurrencyApp.Services
{
	public class AddBankCurrencyService: IAddBankCurrencyService
	{
		private readonly IBankCurrencyFieldsValidator validator;
		private readonly IAddBankCurrencyProcess addingProcess;

		public AddBankCurrencyService()
		{
			validator = ServiceLocator.Get<IBankCurrencyFieldsValidator>();
			addingProcess = ServiceLocator.Get<IAddBankCurrencyProcess>();
		}

		public object AddBankCurrency(Currency currency, string convertation)
		{
			validator.SetNext(addingProcess);

			return validator.Handle(currency, convertation);
		}
	}
}