using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using CurrencyApp.Core;
using CurrencyApp.Model;

namespace CurrencyApp
{
    public partial class BankUserForm: Form
    {
	    private DataTable dt;
	    private static BankUserForm _bankUserForm;

        private BankUserForm()
        {
            InitializeComponent();
			FillTable();
        }

        public static BankUserForm GetInstance()
        {
	        if (_bankUserForm == null)
	        {
		        _bankUserForm = new BankUserForm();
	        }

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

	        using (DBAppContext db = new DBAppContext())
	        {
		        var bankIdInDb = db.Users.Include(u => u.Bank).FirstOrDefault(u => u.Id == CurrentUser.GetInstance().Id)
			        .Bank.Id;
		        var rowId = 1;
		        foreach (var bankCurrencyValue in db.BankCurrencies.Include(bc => bc.Bank).Include(bc => bc.Currency)
			                 .Where(bc => bc.Bank.Id == bankIdInDb))
		        {
			        dt.Rows.Add(rowId++, bankCurrencyValue.Id, bankCurrencyValue.Currency.CurrencyName,
				        bankCurrencyValue.HryvnaConvertation, bankCurrencyValue.CreationDate,
				        bankCurrencyValue.ModificationDate, bankCurrencyValue.Bank.BankName);
		        }

		        dataGridView1.DataSource = dt;
		        DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
		        btn.HeaderText = "";
		        btn.Text = "Оновити курс";
		        btn.Name = "Кнопка1";
		        btn.UseColumnTextForButtonValue = true;
		        dataGridView1.Columns.Add(btn);
		        DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
		        btn2.HeaderText = "";
		        btn2.Text = "Видалити курс";
		        btn2.Name = "Кнопка2";
		        btn2.UseColumnTextForButtonValue = true;
		        dataGridView1.Columns.Add(btn2);
	        }
        }

		private void AddCurrencyButton_Click(object sender, EventArgs e)
		{
			Form form = AddBankCurrency.GetInstance();
            this.Hide();
            form.Show();
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 && e.RowIndex + 1 < dataGridView1.Rows.Count)
            {
                if (e.ColumnIndex == dataGridView1.Columns["Кнопка1"].Index)
                {
                    try
                    {
	                    using (DBAppContext db = new DBAppContext())
	                    {
		                    label1.Visible = true;
		                    textBox1.Visible = true;

		                    DataRow[] dr = dt.Select("ROWID = " + (e.RowIndex + 1));
		                    var bankCurrencyId = Convert.ToInt32(dr[0].ItemArray[1]);
		                    label3.Text = bankCurrencyId.ToString();

		                    textBox1.Text = db.BankCurrencies.FirstOrDefault(d => d.Id == bankCurrencyId).HryvnaConvertation.ToString();
		                    button1.Visible = true;
	                    }

                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                }
                else if (e.ColumnIndex == dataGridView1.Columns["Кнопка2"].Index)
                {
	                using (DBAppContext db = new DBAppContext())
	                {
		                var dr = dt.Select().ToList()[e.RowIndex];
		                var bankCurrencyId = Convert.ToInt32(dr.ItemArray[1]);
		                var bankId = db.Users.Include(u => u.Bank).FirstOrDefault(u => u.Id == CurrentUser.GetInstance().Id).Bank.Id;
		                BankCurrency bankCurrency = db.BankCurrencies.Include(p => p.Currency).Include(p => p.Bank).FirstOrDefault(p => p.Bank.Id == bankId && p.Id == bankCurrencyId);
		                db.BankCurrencies.Remove(bankCurrency);
		                db.SaveChanges();
		                FillTable();
	                }
                }
            }
        }

		private void button1_Click(object sender, EventArgs e)
		{
			var convertation = textBox1.Text.Trim();
			label2.Text = "";

			double result = 0;
			if (string.IsNullOrEmpty(convertation))
			{
				label2.Text += "Ви не ввели курс валюти!\n";
			}
			else if (!Double.TryParse(convertation, out result))
			{
				label2.Text += "Курс валюти невалідний!\n";
			}

			if (!string.IsNullOrEmpty(label2.Text))
			{
				label2.Visible = true;
				return;
			}

			using (DBAppContext db = new DBAppContext())
			{
				var bankCurrency = db.BankCurrencies.Include(bc => bc.Bank).Include(bc => bc.Currency)
					.FirstOrDefault(bc => bc.Id == Convert.ToInt32(label3.Text));

				bankCurrency.HryvnaConvertation = result;
				bankCurrency.ModificationDate = DateTime.Now;
				db.BankCurrencies.Update(bankCurrency);
				db.SaveChanges();
			}

			FillTable();

			label2.Visible = false;
			button1.Visible = false;
			textBox1.Visible = false;
			label1.Visible = false;
		}
	}
}
