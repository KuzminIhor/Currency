using System.Windows.Forms;
using CurrencyApp.Model.Enums;

namespace CurrencyApp.Interfaces
{
	public interface IFormRedirection
	{
		public void Redirect(Form oldForm, FormType newForm);
	}
}