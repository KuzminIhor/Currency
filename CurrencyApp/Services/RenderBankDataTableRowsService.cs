using System.Data;
using CurrencyApp.Core;
using CurrencyApp.Interfaces;
using CurrencyApp.Repositories.Interfaces;

namespace CurrencyApp.Services
{
	public class RenderBankDataTableRowsService: IRenderDataTableRows
	{
		private readonly IBankRepository bankRepository;

		public RenderBankDataTableRowsService()
		{
			bankRepository = ServiceLocator.Get<IBankRepository>();
		}

		public void GetRows(DataTable dt)
		{
			var banks = bankRepository.GetBanks();
			var rowId = 1;

			foreach (var bank in banks)
			{
				dt.Rows.Add(rowId++, bank.Id, bank.BankName);
			}
		}
	}
}