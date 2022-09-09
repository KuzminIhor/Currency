using CurrencyApp.Interfaces;

namespace CurrencyApp.Helpers.Interfaces
{
	public interface IUpdateBankCurrencyProcess: IBankCurrencyHandler
	{
		public void Update(int bankCurrencyId, string convertation);
	}
}