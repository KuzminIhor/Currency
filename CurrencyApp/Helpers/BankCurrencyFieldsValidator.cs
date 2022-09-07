using System;
using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Model;
using CurrencyApp.Model.Abstracts;
using CurrencyApp.Model.Exceptions;

namespace CurrencyApp.Helpers
{
	public class BankCurrencyFieldsValidator: AbstractAddBankCurrencyHandler, IBankCurrencyFieldsValidator
	{
		public override object Handle(Currency currency, string convertation)
		{
			ValidateCurrency(currency);
			ValidateConvertation(convertation);

			return base.Handle(currency, convertation);
		}

		public void ValidateConvertation(string convertation)
		{
			if (string.IsNullOrEmpty(convertation))
			{
				throw new BankCurrencyModifyException("Ви не ввели курс валюти!");
			}
			
			if (!Double.TryParse(convertation, out _))
			{
				throw new BankCurrencyModifyException("Курс валюти невалідний!");
			}
		}

		public void ValidateCurrency(Currency currency)
		{
			if (currency == null)
			{
				throw new BankCurrencyModifyException("Ви не обрали валюту!");
			}
		}
	}
}