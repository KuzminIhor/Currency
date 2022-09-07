using System;

namespace CurrencyApp.Model.Exceptions
{
	[Serializable]
	public class AddBankCurrencyException: Exception
	{
		public AddBankCurrencyException()
		{

		}

		public AddBankCurrencyException(string message)
			: base(message)
		{

		}
	}
}