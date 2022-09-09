using CurrencyApp.Core;
using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Model.Abstracts;
using CurrencyApp.Repositories.Interfaces;

namespace CurrencyApp.Helpers
{
	public class RemoveUsersWorkingInBankProcess: AbstractBankHandler, IRemoveUsersWorkingInBankProcess
	{
		private readonly IUserRepository userRepository;

		public RemoveUsersWorkingInBankProcess()
		{
			userRepository = ServiceLocator.Get<IUserRepository>();
		}

		public override void RemoveBankHandle(int bankId)
		{
			Remove(bankId);

			base.RemoveBankHandle(bankId);
		}

		public void Remove(int bankId)
		{
			var users = userRepository.GetUsersCollection(bankId);
			
			userRepository.RemoveUsers(users);
		}
	}
}