using System.Data.OleDb;
using System.Windows.Forms;
using CurrencyApp.Interfaces;

namespace Курсова.Services
{
	public class FormRedirection : IFormRedirection
	{
		public FormRedirection()
		{

		}

		public void Redirect(Form oldForm, Form newForm)
		{
			oldForm.Hide();
			newForm.Show();
		}
	}
}