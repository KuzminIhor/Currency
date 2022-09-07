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
using CurrencyApp.Interfaces;
using CurrencyApp.Model;
using CurrencyApp.Model.Enums;
using CurrencyApp.Model.Exceptions;
using CurrencyApp.Repositories.Interfaces;
using NLog;

namespace CurrencyApp
{
	public partial class AddBankCurrency : Form
	{
		private static AddBankCurrency _addBankCurrencyForm;
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

		private readonly IFormRedirectionService formRedirectionService;
		private readonly IAddBankCurrencyService addBankCurrencyService;

		private readonly IBankCurrencyRepository bankCurrencyRepository;
		private readonly ICurrencyRepository currencyRepository;

		private AddBankCurrency()
		{
			formRedirectionService = ServiceLocator.Get<IFormRedirectionService>();
			addBankCurrencyService = ServiceLocator.Get<IAddBankCurrencyService>();

			bankCurrencyRepository = ServiceLocator.Get<IBankCurrencyRepository>();
			currencyRepository = ServiceLocator.Get<ICurrencyRepository>();
			
			InitializeComponent();
			
			comboBox1.DataSource = currencyRepository.GetCurrencies();
			comboBox1.DisplayMember = "CurrencyName";
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
			formRedirectionService.Redirect(this, FormType.BankUserForm);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			label1.Text = "";
			label1.Visible = false;
			var currency = comboBox1.SelectedItem as Currency;
			var convertation = textBox1.Text.Trim();

			try
			{
				var formToRedirect = addBankCurrencyService.AddBankCurrency(currency, convertation);
				formRedirectionService.Redirect(this, (FormType) formToRedirect);
			}
			catch (AddBankCurrencyException ex)
			{
				label1.Visible = true;
				label1.Text = ex.Message;
				_logger.Error($"ПОМИЛКА: {ex.Message}");
			}
			catch (Exception ex)
			{
				label1.Visible = true;
				label1.Text = "Сталась якась помилка";
				_logger.Error($"ПОМИЛКА: {ex.Message}");
			}
		}
	}
}
