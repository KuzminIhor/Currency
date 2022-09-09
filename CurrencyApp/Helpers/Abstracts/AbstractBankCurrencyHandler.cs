using CurrencyApp.Interfaces;

namespace CurrencyApp.Model.Abstracts
{
	public abstract class AbstractBankCurrencyHandler: IBankCurrencyHandler
	{
		private IBankCurrencyHandler _nextHandler;

		public IBankCurrencyHandler SetNext(IBankCurrencyHandler handler)
		{
			_nextHandler = handler;
			return handler;
		}

		public virtual object AddBankCurrencyHandle(Currency currency, string convertation)
		{
			object result = null;

			if (_nextHandler != null)
			{
				result = _nextHandler.AddBankCurrencyHandle(currency, convertation);
			}

			return result;
		}

		public virtual void UpdateBankCurrencyHandle(int bankCurrencyId, string convertation)
		{
			if (_nextHandler != null)
			{
				_nextHandler.UpdateBankCurrencyHandle(bankCurrencyId, convertation);
			}
		}
	}
}