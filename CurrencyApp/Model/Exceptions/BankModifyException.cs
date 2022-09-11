using System;

namespace CurrencyApp.Model.Exceptions
{
	[Serializable]
	public class BankModifyException: Exception
	{
		public BankModifyException()
		{

		}

		public BankModifyException(string message)
			: base(message)
		{

		}
	}
}