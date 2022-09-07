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
			Validate(currency, convertation);

			return base.Handle(currency, convertation);
		}

		public void Validate(Currency currency, string convertation)
		{
			if (currency == null)
			{
				throw new AddBankCurrencyException("Ви не обрали валюту!");
			}

			if (string.IsNullOrEmpty(convertation))
			{
				throw new AddBankCurrencyException("Ви не ввели курс валюти!");
			}
			
			if (!Double.TryParse(convertation, out _))
			{
				throw new AddBankCurrencyException("Курс валюти невалідний!");
			}
		}
	}
}