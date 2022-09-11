using CurrencyApp.Core;
using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Model.Abstracts;
using CurrencyApp.Repositories.Interfaces;

namespace CurrencyApp.Helpers
{
	public class RemoveBankProcess: AbstractBankHandler, IRemoveBankProcess
	{
		private readonly IBankRepository bankRepository;

		public RemoveBankProcess()
		{
			bankRepository = ServiceLocator.Get<IBankRepository>();
		}

		public override void RemoveBankHandle(int bankId)
		{
			Remove(bankId);

			base.RemoveBankHandle(bankId);
		}

		public void Remove(int bankId)
		{
			var bank = bankRepository.GetBank(bankId);

			bankRepository.RemoveBank(bank);
		}
	}
}