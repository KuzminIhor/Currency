using System.Data;

namespace CurrencyApp.Interfaces
{
	public interface IRenderCurrency
	{
		public void GetRows(DataTable dt);
	}
}