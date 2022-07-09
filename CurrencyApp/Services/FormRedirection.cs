using System.Data.OleDb;
using System.Windows.Forms;
using CurrencyApp.Interfaces;

namespace CurrencyApp.Services
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