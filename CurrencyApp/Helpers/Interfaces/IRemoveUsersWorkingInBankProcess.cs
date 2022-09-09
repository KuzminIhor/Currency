namespace CurrencyApp.Helpers.Interfaces
{
	public interface IRemoveUsersWorkingInBankProcess: IBankHandler
	{
		public void Remove(int bankId);
	}
}