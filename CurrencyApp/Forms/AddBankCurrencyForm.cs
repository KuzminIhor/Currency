using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using CurrencyApp.Core;
using CurrencyApp.Model;

namespace CurrencyApp
{
	public partial class AddBankCurrency : Form
	{
		private static AddBankCurrency _addBankCurrencyForm;
		private AddBankCurrency()
		{
			InitializeComponent();
			using (DBAppContext db = new DBAppContext())
			{
				comboBox1.DataSource = db.Currencies.ToList();
				comboBox1.DisplayMember = "CurrencyName";
			}
		}

		public static AddBankCurrency GetInstance()
		{
			if (_addBankCurrencyForm == null)
			{
				_addBankCurrencyForm = new AddBankCurrency();
			}

			return _addBankCurrencyForm;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Form form = BankUserForm.GetInstance();
			this.Hide();
			form.Show();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			label1.Text = "";
			label1.Visible = false;
			var currency = comboBox1.SelectedItem as Currency;
			var convertation = textBox1.Text.Trim();

			if (currency == null)
			{
				label1.Text += "Ви не обрали валюту!\n";
			}

			double result = 0;

			if (string.IsNullOrEmpty(convertation))
			{
				label1.Text += "Ви не ввели курс валюти!\n";
			}
			else if (!Double.TryParse(convertation, out result))
			{
				label1.Text += "Курс валюти невалідний!\n";
			}

			if (!string.IsNullOrEmpty(label1.Text))
			{
				label1.Visible = true;
				return;
			}

			using (DBAppContext db = new DBAppContext())
			{
				var bankInDb = db.Users.Include(u => u.Bank).FirstOrDefault(u => u.Id == CurrentUser.GetInstance().Id)
					.Bank;

				var currencyInDb = db.Currencies.FirstOrDefault(c => c.Id == currency.Id);

				db.BankCurrencies.Add(new BankCurrency()
				{
					Currency = currencyInDb,
					Bank = bankInDb,
					UAHConvertation = result
				});

				db.SaveChanges();
			}

			Form form = BankUserForm.GetInstance();
			this.Hide();
			form.Show();
		}
	}
}
