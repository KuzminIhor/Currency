using System;

namespace CurrencyApp.Model.Exceptions
{
	[Serializable]
	public class UserModifyException: Exception
	{
		public UserModifyException()
		{

		}

		public UserModifyException(string message)
			: base(message)
		{

		}
	}
}