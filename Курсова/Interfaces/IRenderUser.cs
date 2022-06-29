using System.Data;

namespace CurrencyApp.Interfaces
{
	public interface IRenderUser
	{
		public void GetRows(DataTable dt);
	}
}