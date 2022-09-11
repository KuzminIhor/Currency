namespace CurrencyApp.Helpers.Interfaces
{
	public interface IRemoveCurrencyProcess: ICurrencyHandler
	{
		public void Remove(int currencyId);
	}
}