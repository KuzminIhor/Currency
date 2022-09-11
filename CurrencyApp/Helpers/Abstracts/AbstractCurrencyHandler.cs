using CurrencyApp.Helpers.Interfaces;

namespace CurrencyApp.Model.Abstracts
{
	public class AbstractCurrencyHandler: ICurrencyHandler
	{
		private ICurrencyHandler _nextHandler;

		public ICurrencyHandler SetNext(ICurrencyHandler handler)
		{
			_nextHandler = handler;
			return handler;
		}

		public virtual void AddCurrencyHandle(string currencyName)
		{
			if (_nextHandler != null)
			{
				_nextHandler.AddCurrencyHandle(currencyName);
			}
		}

		public virtual void UpdateCurrencyHandle(int currencyId, string currencyName)
		{
			if (_nextHandler != null)
			{
				_nextHandler.UpdateCurrencyHandle(currencyId, currencyName);
			}
		}

		public virtual void RemoveCurrencyHandle(int currencyId)
		{
			if (_nextHandler != null)
			{
				_nextHandler.RemoveCurrencyHandle(currencyId);
			}
		}
	}
}