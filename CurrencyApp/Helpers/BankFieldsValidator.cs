using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Model.Abstracts;
using CurrencyApp.Model.Exceptions;

namespace CurrencyApp.Helpers
{
	public class BankFieldsValidator: AbstractBankHandler, IBankFieldsValidator
	{
		public override void AddBankHandle(string bankName)
		{
			ValidateBankName(bankName);

			base.AddBankHandle(bankName);
		}

		public override void UpdateBankHandle(int bankId, string newBankName)
		{
			ValidateBankName(newBankName);

			base.UpdateBankHandle(bankId, newBankName);
		}

		public void ValidateBankName(string bankName)
		{
			if (string.IsNullOrEmpty(bankName))
			{
				throw new BankModifyException("Ви не ввели банк!");
			}
		}
	}
}