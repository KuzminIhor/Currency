using CurrencyApp.Core;
using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Model.Abstracts;
using CurrencyApp.Repositories.Interfaces;

namespace CurrencyApp.Helpers
{
	public class RemoveUserProcess: AbstractUserHandler, IRemoveUserProcess
	{
		private readonly IUserRepository userRepository;

		public RemoveUserProcess()
		{
			userRepository = ServiceLocator.Get<IUserRepository>();
		}

		public override void RemoveUserHandle(int userId)
		{
			Remove(userId);

			base.RemoveUserHandle(userId);
		}

		public void Remove(int userId)
		{
			var user = userRepository.GetUser(userId);

			userRepository.RemoveUsers(user);
		}
	}
}