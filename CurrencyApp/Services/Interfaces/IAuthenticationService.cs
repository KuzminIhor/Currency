namespace CurrencyApp.Interfaces
{
	public interface IAuthenticationService
	{
		public object Authenticate(string userName, string password);
		public object AuthenticateGuest();
	}
}