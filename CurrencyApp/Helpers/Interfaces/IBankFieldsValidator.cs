namespace CurrencyApp.Helpers.Interfaces
{
	public interface IBankFieldsValidator: IBankHandler
	{
		public void ValidateBankName(string bankName);
	}
}