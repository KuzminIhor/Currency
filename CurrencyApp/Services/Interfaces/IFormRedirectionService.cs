using System.Windows.Forms;
using CurrencyApp.Model.Enums;

namespace CurrencyApp.Interfaces
{
	public interface IFormRedirectionService
	{
		public void Redirect(Form oldForm, FormType newForm);
	}
}