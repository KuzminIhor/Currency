namespace CurrencyApp.Helpers.Interfaces
{
	public interface IRemoveBankCurrenciesBelongedToBankProcess: IBankHandler
	{
		public void Remove(int bankId);
	}
}