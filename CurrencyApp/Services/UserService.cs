using CurrencyApp.Core;
using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Interfaces;
using CurrencyApp.Model;

namespace CurrencyApp.Services
{
	public class UserService: IUserService
	{
		private readonly IUserFieldsValidator validator;
		private readonly IAddUserProcess addUserProcess;
		private readonly IUpdateUserProcess updateUserProcess;
		private readonly IRemoveUserProcess removeUserProcess;

		public UserService()
		{
			validator = ServiceLocator.Get<IUserFieldsValidator>();
			addUserProcess = ServiceLocator.Get<IAddUserProcess>();
			updateUserProcess = ServiceLocator.Get<IUpdateUserProcess>();
			removeUserProcess = ServiceLocator.Get<IRemoveUserProcess>();
		}

		public void AddUser(string firstName, string lastName, Bank bank)
		{
			validator.SetNext(addUserProcess);

			validator.AddUserHandle(firstName, lastName, bank);
		}

		public void UpdateUser(int userId, string firstName, string lastName, Bank bank)
		{
			validator.SetNext(updateUserProcess);

			validator.UpdateUserHandle(userId, firstName, lastName, bank);
		}

		public void RemoveUser(int userId)
		{
			removeUserProcess.RemoveUserHandle(userId);
		}
	}
}