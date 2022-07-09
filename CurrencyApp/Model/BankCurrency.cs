using System;

namespace CurrencyApp.Model
{
	public class BankCurrency
	{
		public int Id { get; set; }
		public double HryvnaConvertation { get; set; }
		public DateTime CreationDate { get; set; } = DateTime.Now;
		public DateTime ModificationDate { get; set; } = DateTime.Now;
		public Bank Bank { get; set; }
		public Currency Currency { get; set; }
	}
}