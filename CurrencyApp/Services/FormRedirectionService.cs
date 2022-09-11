using System.Windows.Forms;
using CurrencyApp.Interfaces;
using CurrencyApp.Model.Enums;

namespace CurrencyApp.Services
{
	public class FormRedirectionService : IFormRedirectionService
	{
		public void Redirect(Form oldForm, FormType newForm)
		{
			oldForm.Hide();

			switch (newForm)
			{
				case FormType.GuestForm:
					GuestForm.GetInstance().Show();
					break;
				case FormType.AdminForm:
					AdminForm.GetInstance().Show();
					break;
				case FormType.BankUserForm:
					BankUserForm.GetInstance().Show();
					break;
				case FormType.AddBankCurrencyForm:
					AddBankCurrencyForm.GetInstance().Show();
					break;
			}
		}
	}
}