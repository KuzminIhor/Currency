using CurrencyApp.Helpers.Interfaces;

namespace CurrencyApp.Model.Abstracts
{
	public class AbstractUserHandler: IUserHandler
	{
		private IUserHandler _nextHandler;

		public IUserHandler SetNext(IUserHandler handler)
		{
			_nextHandler = handler;
			return handler;
		}

		public virtual void AddUserHandle(string firstName, string lastName, Bank bank)
		{
			if (_nextHandler != null)
			{
				_nextHandler.AddUserHandle(firstName, lastName, bank);
			}
		}

		public virtual void UpdateUserHandle(int userId, string firstName, string lastName, Bank bank)
		{
			if (_nextHandler != null)
			{
				_nextHandler.UpdateUserHandle(userId, firstName, lastName, bank);
			}
		}

		public virtual void RemoveUserHandle(int userId)
		{
			if (_nextHandler != null)
			{
				_nextHandler.RemoveUserHandle(userId);
			}
		}
	}
}