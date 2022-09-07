namespace CurrencyApp.Model
{
	public class CurrentUser
	{
		private static CurrentUser _currentUser;

		public int Id { get; set; }
		public string UserName { get; set; }
		public bool IsBankUser { get; set; }

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