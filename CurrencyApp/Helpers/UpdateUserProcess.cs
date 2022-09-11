using CurrencyApp.Core;
using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Model;
using CurrencyApp.Model.Abstracts;
using CurrencyApp.Repositories.Interfaces;

namespace CurrencyApp.Helpers
{
	public class UpdateUserProcess: AbstractUserHandler, IUpdateUserProcess
	{
		private readonly IUserRepository userRepository;
		private readonly IBankRepository bankRepository;

		public UpdateUserProcess()
		{
			userRepository = ServiceLocator.Get<IUserRepository>();
			bankRepository = ServiceLocator.Get<IBankRepository>();
		}

		public override void UpdateUserHandle(int userId, string firstName, string lastName, Bank bank)
		{
			Update(userId, firstName, lastName, bank);

			base.UpdateUserHandle(userId, firstName, lastName, bank);
		}

		public void Update(int userId, string firstName, string lastName, Bank bank)
		{
			var bankInDb = bankRepository.GetBank(bank.Id);
			var user = userRepository.GetUser(userId);

			user.FirstName = firstName;
			user.LastName = lastName;
			user.Bank = bankInDb;

			userRepository.UpdateUser(user);
		}
	}
}