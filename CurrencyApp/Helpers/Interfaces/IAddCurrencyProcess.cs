namespace CurrencyApp.Helpers.Interfaces
{
	public interface IAddCurrencyProcess: ICurrencyHandler
	{
		public void Add(string currencyName);
	}
}