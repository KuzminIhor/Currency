using CurrencyApp.Interfaces;

namespace CurrencyApp.Model.Abstracts
{
	public abstract class AbstractAddBankCurrencyHandler: IAddBankCurrencyHandler
	{
		private IAddBankCurrencyHandler _nextHandler;

		public IAddBankCurrencyHandler SetNext(IAddBankCurrencyHandler handler)
		{
			_nextHandler = handler;
			return handler;
		}

		public virtual object Handle(Currency currency, string convertation)
		{
			object result = null;

			if (_nextHandler != null)
			{
				result = _nextHandler.Handle(currency, convertation);
			}

			return result;
		}
	}
}