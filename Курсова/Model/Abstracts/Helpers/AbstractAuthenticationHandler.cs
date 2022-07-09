using CurrencyApp.Interfaces;

namespace CurrencyApp.Model.Abstracts
{
	public abstract class AbstractAuthenticationHandler: IAuthenticationHandler
	{
		private IAuthenticationHandler _nextHandler;

		public IAuthenticationHandler SetNext(IAuthenticationHandler handler)
		{
			_nextHandler = handler;
			return handler;
		}

		public virtual object Handle(string userName, string password)
		{
			object result = null;

			if (_nextHandler != null)
			{
				result = _nextHandler.Handle(userName, password);
			}

			return result;
		}
	}
}