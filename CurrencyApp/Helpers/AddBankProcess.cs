using CurrencyApp.Core;
using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Model;
using CurrencyApp.Model.Abstracts;
using CurrencyApp.Repositories.Interfaces;

namespace CurrencyApp.Helpers
{
	public class AddBankProcess: AbstractBankHandler, IAddBankProcess
	{
		private readonly IBankRepository bankRepository;

		public AddBankProcess()
		{
			bankRepository = ServiceLocator.Get<IBankRepository>();
		}

		public override void AddBankHandle(string bankName)
		{
			Add(bankName);

			base.AddBankHandle(bankName);
		}

		public void Add(string bankName)
		{
			bankRepository.AddBank(new Bank() {BankName = bankName});
		}
	}
}