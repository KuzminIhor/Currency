namespace CurrencyApp.Helpers.Interfaces
{
	public interface IUpdateBankProcess: IBankHandler
	{
		public void Update(int bankId, string newBankName);
	}
}