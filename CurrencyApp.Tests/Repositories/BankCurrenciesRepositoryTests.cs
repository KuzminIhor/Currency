using System;
using System.Runtime.InteropServices;
using CurrencyApp.Core;
using CurrencyApp.Repositories;
using CurrencyApp.Tests.Utils;
using Moq;
using Xunit;

namespace CurrencyApp.Tests.Repositories
{
	public class BankCurrencyRepositoryTests
	{
		private readonly DBAppContext _context;
		private readonly BankCurrencyRepository bankCurrencyRepository;

		public BankCurrencyRepositoryTests()
		{
			ServiceLocatorHelper.CreateUnityContainer();

			_context = DatabaseInMemoryManagementService.Create();
			DatabaseRecordsAddingService.AddTestBanks(_context);
			DatabaseRecordsAddingService.AddTestCurrencies(_context);
			DatabaseRecordsAddingService.AddTestBankCurrencies(_context);

			ServiceLocator.RegisterSingleton(_context);
			bankCurrencyRepository = new BankCurrencyRepository(_context);
		}

		[Theory, AutoMoqData]
		public void GetBankCurrenciesCollectionInDateRange_WhenDataNotExistsInDateRange_ThenReturnNoData()
		{
			var result = bankCurrencyRepository.GetBankCurrenciesCollectionInDateRange(It.IsAny<DateTime>(), It.IsAny<DateTime>());

			Assert.Equal(0, result.Count);
		}

		[Theory, AutoMoqData]
		public void GetBankCurrenciesCollectionInDateRange_WhenDataExistsInDateRange_ThenReturnBankCurrencies()
		{
			var result = bankCurrencyRepository.GetBankCurrenciesCollectionInDateRange(DateTime.Now, DateTime.Now.AddDays(1));

			Assert.NotEqual(0, result.Count);
		}
	}
}