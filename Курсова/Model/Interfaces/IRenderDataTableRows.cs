using System.Data;

namespace CurrencyApp.Interfaces
{
	public interface IRenderDataTableRows
	{
		public void GetRows(DataTable dt);
	}
}