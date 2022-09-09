namespace CurrencyApp.Helpers.Interfaces
{
	public interface IBankHandler
	{
		public IBankHandler SetNext(IBankHandler handler);
		public void AddBankHandle(string bankName);
		public void UpdateBankHandle(int bankId, string newBankName);
		public void RemoveBankHandle(int bankId);
	}
}