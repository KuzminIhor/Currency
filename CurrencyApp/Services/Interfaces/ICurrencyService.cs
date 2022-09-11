namespace CurrencyApp.Interfaces
{
	public interface ICurrencyService
	{
		public void AddCurrency(string currencyName);
		public void UpdateCurrency(int currencyId, string currencyName);
		public void RemoveCurrency(int cucrencyId);
	}
}