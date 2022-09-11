using CurrencyApp.Core;
using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Model.Abstracts;
using CurrencyApp.Repositories.Interfaces;

namespace CurrencyApp.Helpers
{
	public class UpdateBankProcess: AbstractBankHandler, IUpdateBankProcess
	{
		private readonly IBankRepository bankRepository;

		public UpdateBankProcess()
		{
			bankRepository = ServiceLocator.Get<IBankRepository>();
		}

		public override void UpdateBankHandle(int bankId, string newBankName)
		{
			Update(bankId, newBankName);

			base.UpdateBankHandle(bankId, newBankName);
		}

		public void Update(int bankId, string newBankName)
		{
			var bank = bankRepository.GetBank(bankId);
			
			bank.BankName = newBankName;

			bankRepository.UpdateBank(bank);
		}
	}
}