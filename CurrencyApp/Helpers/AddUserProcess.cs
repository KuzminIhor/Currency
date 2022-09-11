using System;
using CurrencyApp.Core;
using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Model;
using CurrencyApp.Model.Abstracts;
using CurrencyApp.Repositories.Interfaces;

namespace CurrencyApp.Helpers
{
	public class AddUserProcess: AbstractUserHandler, IAddUserProcess
	{
		private readonly IUserRepository userRepository;
		private readonly IBankRepository bankRepository;

		public AddUserProcess()
		{
			userRepository = ServiceLocator.Get<IUserRepository>();
			bankRepository = ServiceLocator.Get<IBankRepository>();
		}

		public override void AddUserHandle(string firstName, string lastName, Bank bank)
		{
			Add(firstName, lastName, bank);

			base.AddUserHandle(firstName, lastName, bank);
		}

		public void Add(string firstName, string lastName, Bank bank)
		{
			var bankInDb = bankRepository.GetBank(bank.Id);

			var chars = "QWERTYUIOP[]qwertyuiop1234567890";
			var stringChars = new char[6];
			var random = new Random();

			for (int i = 0; i < stringChars.Length; i++)
			{
				stringChars[i] = chars[random.Next(chars.Length)];
			}

			userRepository.AddUser(new User()
			{
				FirstName = firstName,
				LastName = lastName,
				UserName = $"{firstName}{lastName}",
				Bank = bankInDb,
				Password = new string(stringChars),
				IsBankUser = true
			});
		}
	}
}