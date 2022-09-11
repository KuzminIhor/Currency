using CurrencyApp.Core;
using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Interfaces;
using CurrencyApp.Repositories.Interfaces;

namespace CurrencyApp.Services
{
	public class BankService: IBankService
	{
		private readonly IBankFieldsValidator validator;
		private readonly IAddBankProcess addBankProcess;
		private readonly IUpdateBankProcess updateBankProcess;

		private readonly IRemoveUsersWorkingInBankProcess removeUsersWorkingInBankProcess;
		private readonly IRemoveBankCurrenciesBelongedToBankProcess removeBankCurrenciesBelongedToBankProcess;
		private readonly IRemoveBankProcess removeBankProcess;

		private readonly IBankRepository bankRepository;

		public BankService()
		{
			validator = ServiceLocator.Get<IBankFieldsValidator>();
			addBankProcess = ServiceLocator.Get<IAddBankProcess>();
			updateBankProcess = ServiceLocator.Get<IUpdateBankProcess>();

			removeUsersWorkingInBankProcess = ServiceLocator.Get<IRemoveUsersWorkingInBankProcess>();
			removeBankCurrenciesBelongedToBankProcess =
				ServiceLocator.Get<IRemoveBankCurrenciesBelongedToBankProcess>();
			removeBankProcess = ServiceLocator.Get<IRemoveBankProcess>();

			bankRepository = ServiceLocator.Get<IBankRepository>();
		}

		public void AddBank(string bankName)
		{
			validator.SetNext(addBankProcess);

			validator.AddBankHandle(bankName);
		}

		public void UpdateBank(int bankId, string newBankName)
		{
			validator.SetNext(updateBankProcess);

			validator.UpdateBankHandle(bankId, newBankName);
		}

		public void RemoveBank(int bankId)
		{
			removeUsersWorkingInBankProcess.SetNext(removeBankCurrenciesBelongedToBankProcess)
				.SetNext(removeBankProcess);

			removeUsersWorkingInBankProcess.RemoveBankHandle(bankId);
		}
	}
}