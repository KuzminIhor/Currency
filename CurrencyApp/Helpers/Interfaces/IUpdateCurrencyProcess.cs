namespace CurrencyApp.Helpers.Interfaces
{
	public interface IUpdateCurrencyProcess: ICurrencyHandler
	{
		public void Update(int currencyId, string currencyName);
	}
}