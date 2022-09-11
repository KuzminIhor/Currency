namespace CurrencyApp.Helpers.Interfaces
{
	public interface ICurrencyHandler
	{
		public ICurrencyHandler SetNext(ICurrencyHandler currencyHandler);
		public void AddCurrencyHandle(string currencyName);
		public void UpdateCurrencyHandle(int currencyId, string currencyName);
		public void RemoveCurrencyHandle(int currencyId);
	}
}