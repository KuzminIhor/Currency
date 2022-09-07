using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using CurrencyApp.Core;
using CurrencyApp.Repositories.Interfaces;
using NLog;

namespace CurrencyApp
{
    public partial class GuestForm : Form
    {
	    private static GuestForm _guestForm;
	    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

	    private readonly IBankCurrencyRepository bankCurrencyRepository;

		private GuestForm()
		{
			this.bankCurrencyRepository = ServiceLocator.Get<IBankCurrencyRepository>();

            InitializeComponent();
        }

        public static GuestForm GetInstance()
        {
	        if (_guestForm == null)
	        {
		        _guestForm = new GuestForm();
	        }

	        return _guestForm;
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
	        var dateFrom = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month,
		        dateTimePicker1.Value.Day, 0, 0, 0);
	        var dateTo = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month,
		        dateTimePicker2.Value.Day, 23, 59, 59);

	        dataGridView1.Visible = true;

	        DataTable dt = new DataTable();
	        dt.Columns.Add("RowId");
	        dt.Columns.Add("Id");
	        dt.Columns.Add("Назва валюти");
	        dt.Columns.Add("Курс");
	        dt.Columns.Add("Додана до курсу валют у");
	        dt.Columns.Add("Змінена у курсі валют у");
	        dt.Columns.Add("Банк");

	        var rowId = 1;

	        foreach (var bankCurrencyValue in bankCurrencyRepository.GetBankCurrenciesInCurrentDateRange(dateFrom, dateTo))
	        {
		        dt.Rows.Add(rowId++, bankCurrencyValue.Id, bankCurrencyValue.Currency.CurrencyName,
			        bankCurrencyValue.UAHConvertation, bankCurrencyValue.CreationDate,
			        bankCurrencyValue.ModificationDate, bankCurrencyValue.Bank.BankName);
	        }

	        dataGridView1.DataSource = dt;
        }
    }
}
