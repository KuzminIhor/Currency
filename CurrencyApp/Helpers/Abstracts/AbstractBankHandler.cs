using CurrencyApp.Helpers.Interfaces;

namespace CurrencyApp.Model.Abstracts
{
	public class AbstractBankHandler: IBankHandler
	{
		private IBankHandler _nextHandler;

		public IBankHandler SetNext(IBankHandler handler)
		{
			_nextHandler = handler;
			return handler;
		}

		public virtual void AddBankHandle(string bankName)
		{
			if (_nextHandler != null)
			{
				_nextHandler.AddBankHandle(bankName);
			}
		}

		public virtual void UpdateBankHandle(int bankId, string newBankName)
		{
			if (_nextHandler != null)
			{
				_nextHandler.UpdateBankHandle(bankId, newBankName);
			}
		}

		public virtual void RemoveBankHandle(int bankId)
		{
			if (_nextHandler != null)
			{
				_nextHandler.RemoveBankHandle(bankId);
			}
		}
	}
}