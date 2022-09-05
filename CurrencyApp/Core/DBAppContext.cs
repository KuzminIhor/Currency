using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CurrencyApp.Model;

namespace CurrencyApp.Core
{
	public class DBAppContext: DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Currency> Currencies { get; set; }
		public DbSet<BankCurrency> BankCurrencies { get; set; }
		public DbSet<Bank> Banks { get; set; }

		public DBAppContext()
		{
			this.Database.EnsureCreated();
		}

		public DBAppContext(DbContextOptions dbContextOptions)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CurrencyApp;Trusted_Connection=True;");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasData(new List<User>()
			{
				new User() {Id = 1, UserName = "admin", Password = "admin"},
				new User() {Id = 2, UserName = "guest"}
			});

			modelBuilder.Entity<Bank>().HasData(new List<Bank>()
			{
				new Bank() {Id = 1, BankName= "Mono"},
				new Bank() {Id = 2, BankName= "Visa"}
			});

			modelBuilder.Entity<Currency>().HasData(new List<Currency>()
			{
				new Currency() {Id = 1, CurrencyName = "Dollar"},
				new Currency() {Id = 2, CurrencyName = "Euro"},
			});
		}
    }
}
