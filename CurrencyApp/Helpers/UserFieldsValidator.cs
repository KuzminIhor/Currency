using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Model;
using CurrencyApp.Model.Abstracts;
using CurrencyApp.Model.Exceptions;

namespace CurrencyApp.Helpers
{
	public class UserFieldsValidator: AbstractUserHandler, IUserFieldsValidator
	{
		public override void AddUserHandle(string firstName, string lastName, Bank bank)
		{
			ValidateLastName(lastName);
			ValidateFirstName(firstName);
			ValidateBank(bank);

			base.AddUserHandle(firstName, lastName, bank);
		}

		public override void UpdateUserHandle(int userId, string firstName, string lastName, Bank bank)
		{
			ValidateLastName(lastName);
			ValidateFirstName(firstName);
			ValidateBank(bank);

			base.UpdateUserHandle(userId, firstName, lastName, bank);
		}

		public void ValidateFirstName(string firstName)
		{
			if (string.IsNullOrEmpty(firstName))
			{
				throw new UserModifyException("Ви не ввели ім'я!");
			}
		}

		public void ValidateLastName(string lastName)
		{
			if (string.IsNullOrEmpty(lastName))
			{
				throw new UserModifyException("Ви не ввели прізвище!");
			}
		}

		public void ValidateBank(Bank bank)
		{
			if (bank == null)
			{
				throw new UserModifyException("Ви не обрали банк!");
			}
		}
	}
}