namespace CurrencyApp.Helpers.Interfaces
{
	public interface ICurrencyFieldsValidator: ICurrencyHandler
	{
		public void ValidateCurrencyName(string currencyName);
	}
}