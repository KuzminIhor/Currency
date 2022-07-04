using System.Data.OleDb;
using System.Windows.Forms;

namespace Курсова.Services
{
	public static class FormRedirection
	{
		public static void Redirect(Form oldForm, Form newForm)
		{
			oldForm.Hide();
			newForm.Show();
		}
	}
}