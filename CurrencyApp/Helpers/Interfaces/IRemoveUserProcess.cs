namespace CurrencyApp.Helpers.Interfaces
{
	public interface IRemoveUserProcess: IUserHandler
	{
		public void Remove(int userId);
	}
}