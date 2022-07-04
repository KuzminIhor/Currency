namespace CurrencyApp.Interfaces
{
	public interface IAuthenticationHandler
	{
		public IAuthenticationHandler SetNext(IAuthenticationHandler handler);
		public object Handle(string userName, string password);
	}
}