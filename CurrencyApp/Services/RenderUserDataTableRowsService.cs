using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CurrencyApp.Core;
using CurrencyApp.Interfaces;
using CurrencyApp.Repositories.Interfaces;

namespace CurrencyApp.Helpers
{
	public class RenderUserDataTableRowsService: IRenderDataTableRows
	{
		private readonly IUserRepository userRepository;

		public RenderUserDataTableRowsService()
		{
			userRepository = ServiceLocator.Get<IUserRepository>();
		}

		public void GetRows(DataTable dt)
		{

			var users = userRepository.GetUsersExceptAdminAndGuest();
			var rowId = 1;

			foreach (var user in users)
			{
				dt.Rows.Add(rowId++, user.Id, user.LastName, user.FirstName, user.Bank.BankName);
			}
		}
	}
}