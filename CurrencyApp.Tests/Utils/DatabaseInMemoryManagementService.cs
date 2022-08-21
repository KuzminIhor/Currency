using System;
using System.Runtime.InteropServices;
using CurrencyApp.Core;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApp.Tests.Utils
{
	public static class DatabaseInMemoryManagementService
	{
		public static DBAppContext Create()
		{
			var options = new DbContextOptionsBuilder<DBAppContext>()
				.UseInMemoryDatabase(databaseName: "CurrencyAppInMemory")
				.Options;

			var context = new DBAppContext(options);

			return context;
		}
	}
}