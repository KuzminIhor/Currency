namespace CurrencyApp.Interfaces
{
	public interface IBankService
	{
		public void AddBank(string bankName);
		public void UpdateBank(int bankId, string newBankName);
		public void RemoveBank(int bankId);
	}
}