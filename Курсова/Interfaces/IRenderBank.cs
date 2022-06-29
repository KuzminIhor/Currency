using System.Data;

namespace CurrencyApp.Interfaces
{
	public interface IRenderBank
	{
		public void GetRows(DataTable dt);
	}
}