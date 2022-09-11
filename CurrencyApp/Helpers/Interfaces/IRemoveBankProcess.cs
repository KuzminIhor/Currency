namespace CurrencyApp.Helpers.Interfaces
{
	public interface IRemoveBankProcess: IBankHandler
	{
		public void Remove(int bankId);
	}
}