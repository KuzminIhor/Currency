using System;
using System.Runtime.InteropServices;
using CurrencyApp.Core;
using CurrencyApp.Repositories;
using CurrencyApp.Tests.Utils;
using Moq;
using Xunit;

namespace CurrencyApp.Tests.Repositories
{
	public class BankCurrenciesRepositoryTests
	{
		private readonly DBAppContext _context;
		private readonly BankCurrenciesRepository bankCurrenciesRepository;

		public BankCurrenciesRepositoryTests()
		{
			ServiceLocatorHelper.CreateUnityContainer();

			_context = DatabaseInMemoryManagementService.Create();
			DatabaseRecordsAddingService.AddTestBanks(_context);
			DatabaseRecordsAddingService.AddTestCurrencies(_context);
			DatabaseRecordsAddingService.AddTestBankCurrencies(_context);

			ServiceLocator.RegisterSingleton(_context);
			bankCurrenciesRepository = new BankCurrenciesRepository(_context);
		}

		[Theory, AutoMoqData]
		public void GetBankCurrenciesInCurrentDateRange_WhenDataNotExistsInDateRange_ThenReturnNoData()
		{
			var result = bankCurrenciesRepository.GetBankCurrenciesInCurrentDateRange(It.IsAny<DateTime>(), It.IsAny<DateTime>());

			Assert.Equal(0, result.Count);
		}

		[Theory, AutoMoqData]
		public void GetBankCurrenciesInCurrentDateRange_WhenDataExistsInDateRange_ThenReturnBankCurrencies()
		{
			var result = bankCurrenciesRepository.GetBankCurrenciesInCurrentDateRange(DateTime.Now, DateTime.Now.AddDays(1));

			Assert.NotEqual(0, result.Count);
		}
	}
}