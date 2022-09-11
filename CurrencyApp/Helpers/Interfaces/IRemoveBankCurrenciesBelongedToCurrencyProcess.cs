namespace CurrencyApp.Helpers.Interfaces
{
	public interface IRemoveBankCurrenciesBelongedToCurrencyProcess: ICurrencyHandler
	{
		public void Remove(int currencyId);
	}
}