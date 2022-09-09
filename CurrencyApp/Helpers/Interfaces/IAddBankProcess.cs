namespace CurrencyApp.Helpers.Interfaces
{
	public interface IAddBankProcess: IBankHandler
	{
		public void Add(string bankName);
	}
}