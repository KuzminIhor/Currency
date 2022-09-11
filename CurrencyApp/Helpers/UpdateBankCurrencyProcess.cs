using System;
using CurrencyApp.Core;
using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Model.Abstracts;
using CurrencyApp.Repositories.Interfaces;

namespace CurrencyApp.Helpers
{
	public class UpdateBankCurrencyProcess: AbstractBankCurrencyHandler, IUpdateBankCurrencyProcess
	{
		private readonly IBankCurrencyRepository bankCurrencyRepository;

		public UpdateBankCurrencyProcess()
		{
			bankCurrencyRepository = ServiceLocator.Get<IBankCurrencyRepository>();
		}

		public override void UpdateBankCurrencyHandle(int bankCurrencyId, string convertation)
		{
			Update(bankCurrencyId, convertation);

			base.UpdateBankCurrencyHandle(bankCurrencyId, convertation);
		}

		public void Update(int bankCurrencyId, string convertation)
		{
			var bankCurrency = bankCurrencyRepository.GetBankCurrency(bankCurrencyId);

			bankCurrency.UAHConvertation = Convert.ToDouble(convertation);
			bankCurrency.ModificationDate = DateTime.Now;

			bankCurrencyRepository.UpdateBankCurrency(bankCurrency);
		}
	}
}