namespace CurrencyApp.Model
{
	public class CurrentUser: User
	{
		private static CurrentUser _currentUser;

		private CurrentUser()
		{
		}

		public static CurrentUser GetInstance()
		{
			if (_currentUser == null)
			{
				_currentUser = new CurrentUser();
			}

			return _currentUser;
		}
	}
}