using System;
using System.Data;
using System.Linq;
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
    public partial class BankUserForm: Form
    {
	    private DataTable dt;
	    private static BankUserForm _bankUserForm;
	    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

		private readonly IFormRedirectionService formRedirectionService;
	    private readonly IBankCurrencyService bankCurrencyService;

		private readonly IBankRepository bankRepository;
	    private readonly IBankCurrencyRepository bankCurrencyRepository;

        private BankUserForm()
        {
	        formRedirectionService = ServiceLocator.Get<IFormRedirectionService>();
	        bankCurrencyService = ServiceLocator.Get<IBankCurrencyService>();
		        
			bankRepository = ServiceLocator.Get<IBankRepository>();
	        bankCurrencyRepository = ServiceLocator.Get<IBankCurrencyRepository>();

            InitializeComponent();
        }

        public static BankUserForm GetInstance()
        {
	        if (_bankUserForm == null)
	        {
		        _bankUserForm = new BankUserForm();
	        }

	        _bankUserForm.FillTable();

			return _bankUserForm;
        }

        private void FillTable()
        {
	        dataGridView1.Columns.Clear();

	        dt = new DataTable();
	        dt.Columns.Add("RowId");
	        dt.Columns.Add("Id");
	        dt.Columns.Add("Назва валюти");
	        dt.Columns.Add("Курс");
	        dt.Columns.Add("Додана до курсу валют у");
	        dt.Columns.Add("Змінена у курсі валют у");
	        dt.Columns.Add("Ваш Банк");

	        var bankIdInDb = bankRepository.GetBankByUserId(CurrentUser.GetInstance().Id).Id;
	        var rowId = 1;

	        foreach (var bankCurrencyValue in bankCurrencyRepository.GetBankCurrenciesCollection(bankIdInDb))
	        {
		        dt.Rows.Add(rowId++, bankCurrencyValue.Id, bankCurrencyValue.Currency.CurrencyName,
			        bankCurrencyValue.UAHConvertation, bankCurrencyValue.CreationDate,
			        bankCurrencyValue.ModificationDate, bankCurrencyValue.Bank.BankName);
	        }

	        dataGridView1.DataSource = dt;

	        DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
	        btn.HeaderText = "";
	        btn.Text = "Оновити курс";
	        btn.Name = "Оновити";
	        btn.UseColumnTextForButtonValue = true;
	        dataGridView1.Columns.Add(btn);

	        DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
	        btn2.HeaderText = "";
	        btn2.Text = "Видалити курс";
	        btn2.Name = "Видалити";
	        btn2.UseColumnTextForButtonValue = true;
	        dataGridView1.Columns.Add(btn2);
        }

        private void AddCurrencyButton_Click(object sender, EventArgs e)
		{
			formRedirectionService.Redirect(this, FormType.AddBankCurrency);
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 && e.RowIndex + 1 < dataGridView1.Rows.Count)
            {
                if (e.ColumnIndex == dataGridView1.Columns["Оновити"].Index)
                {
	                try
	                {
		                label1.Visible = true;
		                textBox1.Visible = true;

		                DataRow[] dr = dt.Select("ROWID = " + (e.RowIndex + 1));
		                var bankCurrencyId = Convert.ToInt32(dr[0].ItemArray[1]);
		                label3.Text = bankCurrencyId.ToString();

		                textBox1.Text = bankCurrencyRepository.GetBankCurrency(bankCurrencyId)
			                .UAHConvertation.ToString();

		                button1.Visible = true;
	                }
	                catch (Exception exception)
	                {
						_logger.Error($"ПОМИЛКА під час відображення блоку Оновлення курсу валюти: {exception.Message}");
					}
                }
                else if (e.ColumnIndex == dataGridView1.Columns["Видалити"].Index)
                {
	                var dr = dt.Select().ToList()[e.RowIndex];
	                var bankCurrencyId = Convert.ToInt32(dr.ItemArray[1]);

	                try
	                {
		                bankCurrencyService.RemoveBankCurrency(bankCurrencyId);
						_logger.Info($"Курс валюти з ID {bankCurrencyId} був успішно видалений користувачем {CurrentUser.GetInstance().Id}");
					}
	                catch (Exception ex)
	                {
		                _logger.Error($"ПОМИЛКА при видаленні курсу валюти з ID {bankCurrencyId} користувачем {CurrentUser.GetInstance().Id}: {ex.Message}");
					}

	                FillTable();
                }
            }
        }

		private void button1_Click(object sender, EventArgs e)
		{
			var convertation = textBox1.Text.Trim();
			var bankCurrencyId = Convert.ToInt32(label3.Text);
			label2.Text = "";

			try
			{
				bankCurrencyService.UpdateBankCurrency(convertation, bankCurrencyId);
				_logger.Info($"Курс валюти з ID {bankCurrencyId} було оновлено користувачем {CurrentUser.GetInstance().Id}");
			}
			catch (BankCurrencyModifyException ex)
			{
				label2.Visible = true;
				label2.Text = ex.Message;
				_logger.Error($"ПОМИЛКА при оновлені курсу валюти з ID {bankCurrencyId} користувачем {CurrentUser.GetInstance().Id}: {ex.Message}");
			}
			catch (Exception ex)
			{
				label2.Visible = true;
				label2.Text = "Сталась якась помилка";
				_logger.Error($"ПОМИЛКА при оновлені курсу валюти з ID {bankCurrencyId} користувачем {CurrentUser.GetInstance().Id}: {ex.Message}");
			}

			FillTable();

			label2.Visible = false;
			button1.Visible = false;
			textBox1.Visible = false;
			label1.Visible = false;
		}
	}
}
