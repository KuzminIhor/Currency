using System;
using System.Collections.Generic;
using System.Linq;
using CurrencyApp.Core;
using CurrencyApp.Model;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApp.Tests.Utils
{
	public static class DatabaseRecordsAddingService
	{
		public static void AddTestUsers(DBAppContext dbContext)
		{
			dbContext.Users.AddRange(new List<User>()
			{
				new User()
				{
					FirstName = "admin", 
					LastName = "admin", 
					IsBankUser = false, 
					UserName = "admin", 
					Password = "admin"
				},
				new User()
				{
					FirstName = "guest", 
					LastName = "guest", 
					IsBankUser = false, 
					UserName = "guest"
				},
				new User()
				{
					FirstName = "test", 
					LastName = "test", 
					IsBankUser = false, 
					UserName = "testtest", 
					Password = "12345"
				},
				new User()
				{
					FirstName = "test2", 
					LastName = "test2", 
					IsBankUser = true, 
					UserName = "test2test2", 
					Password = "12345"
				}
			});

			dbContext.SaveChanges();
		}

		public static void AddTestCurrencies(DBAppContext dbContext)
		{
			dbContext.Currencies.AddRange(new List<Currency>()
			{
				new Currency() { CurrencyName = "test"}
			});

			dbContext.SaveChanges();
		}

		public static void AddTestBanks(DBAppContext dbContext)
		{
			dbContext.Banks.AddRange(new List<Bank>()
			{
				new Bank() { BankName = "testBank" }
			});

			dbContext.SaveChanges();
		}

		public static void AddTestBankCurrencies(DBAppContext dbContext)
		{
			dbContext.BankCurrencies.AddRange(new List<BankCurrency>()
			{
				new BankCurrency() 
				{ 
					Bank = dbContext.Banks.FirstOrDefault(), 
					Currency = dbContext.Currencies.FirstOrDefault(), 
					HryvnaConvertation = 15.0, 
					CreationDate = DateTime.Now, 
					ModificationDate = DateTime.Now
				},
				new BankCurrency()
				{
					Bank = dbContext.Banks.FirstOrDefault(), 
					Currency = dbContext.Currencies.FirstOrDefault(), 
					HryvnaConvertation = 10.0, 
					CreationDate = DateTime.Now.AddDays(1), 
					ModificationDate = DateTime.Now.AddDays(1)
				},
			});

			dbContext.SaveChanges();
		}
	}
}