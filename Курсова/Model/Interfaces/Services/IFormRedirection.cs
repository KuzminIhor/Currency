using System.Windows.Forms;

namespace CurrencyApp.Interfaces
{
	public interface IFormRedirection
	{
		public void Redirect(Form oldForm, Form newForm);
	}
}