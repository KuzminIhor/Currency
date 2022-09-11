using System;

namespace CurrencyApp.Model.Exceptions
{
	[Serializable]
	public class CurrencyModifyException: Exception
	{
		public CurrencyModifyException()
		{

		}

		public CurrencyModifyException(string message)
			: base(message)
		{

		}
	}
}