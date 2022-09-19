using System;
using System.Windows.Forms;
using CurrencyApp.Core;
using CurrencyApp.Interfaces;
using CurrencyApp.Model;
using CurrencyApp.Model.Enums;
using CurrencyApp.Model.Exceptions;
using CurrencyApp.Repositories.Interfaces;
using NLog;

namespace CurrencyApp
{
	public partial class AddBankCurrencyForm : Form
	{
		private static AddBankCurrencyForm _addBankCurrencyForm;
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

		private readonly IBankCurrencyService bankCurrencyService;
		private readonly IFormRedirectionService formRedirectionService;

		private readonly ICurrencyRepository currencyRepository;

		private AddBankCurrencyForm()
		{
			bankCurrencyService = ServiceLocator.Get<IBankCurrencyService>();
			formRedirectionService = ServiceLocator.Get<IFormRedirectionService>();

			currencyRepository = ServiceLocator.Get<ICurrencyRepository>();

			InitializeComponent();

			comboBox1.DataSource = currencyRepository.GetCurrencies();
			comboBox1.DisplayMember = "CurrencyName";
		}

		public static AddBankCurrencyForm GetInstance()
		{
			if (_addBankCurrencyForm == null)
			{
				_addBankCurrencyForm = new AddBankCurrencyForm();
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
				var formToRedirect = bankCurrencyService.AddBankCurrency(currency, convertation);
				formRedirectionService.Redirect(this, (FormType) formToRedirect);

				_logger.Info($"Курс валюти успішно доданий користувачем {CurrentUser.GetInstance().Id}.");
			}
			catch (BankCurrencyModifyException ex)
			{
				label1.Visible = true;
				label1.Text = ex.Message;

				_logger.Error($"ПОМИЛКА під час додавання курсу валюти користувачем {CurrentUser.GetInstance().Id}: {ex.Message}");
			}
			catch (Exception ex)
			{
				label1.Visible = true;
				label1.Text = "Сталась якась помилка";

				_logger.Error($"ПОМИЛКА під час додавання курсу валюти користувачем {CurrentUser.GetInstance().Id}: {ex.Message}");
			}
		}
	}
}
