using System;

namespace CurrencyApp.Model.Exceptions
{
	[Serializable]
	public class BankCurrencyModifyException: Exception
	{
		public BankCurrencyModifyException()
		{

		}

		public BankCurrencyModifyException(string message)
			: base(message)
		{

		}
	}
}