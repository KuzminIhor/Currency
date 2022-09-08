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
using CurrencyApp.Helpers;
using CurrencyApp.Interfaces;
using CurrencyApp.Model;
using CurrencyApp.Services;
using Currency = CurrencyApp.Model.Currency;

namespace CurrencyApp
{
	public partial class AdminForm : Form
	{
		private DataTable usersDataTable = new DataTable();
		private DataTable banksDataTable = new DataTable();
		private DataTable currenciesDataTable = new DataTable();

		private IRenderDataTableRows renderBank;
		private IRenderDataTableRows renderUser;
		private IRenderDataTableRows renderCurrency;

		private static AdminForm _adminForm;

		private AdminForm()
		{
			renderBank = ServiceLocator.Get<IRenderDataTableRows>(nameof(RenderBankDataTableRowsService));
			renderUser = ServiceLocator.Get<IRenderDataTableRows>(nameof(RenderUserDataTableRowsService));
			renderCurrency = ServiceLocator.Get<IRenderDataTableRows>(nameof(RenderCurrencyDataTableRowsService));

			InitializeComponent();

			FillDataTables();
		}

		public static AdminForm GetInstance()
		{
			if (_adminForm == null)
			{
				_adminForm = new AdminForm();
			}

			return _adminForm;
		}

		private void FillDataTables()
		{
			FillUsers();
			FillBanks();
			FillCurrencies();

			renderUser.GetRows(usersDataTable);
			renderCurrency.GetRows(currenciesDataTable);
			renderBank.GetRows(banksDataTable);
		}

		private void FillUsers()
		{
			dataGridView1.Columns.Clear();

			usersDataTable = new DataTable();
			usersDataTable.Columns.Add("RowId");
			usersDataTable.Columns.Add("Id");
			usersDataTable.Columns.Add("Прізвище");
			usersDataTable.Columns.Add("Ім'я");
			usersDataTable.Columns.Add("Банк");

			dataGridView1.DataSource = usersDataTable;

			DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
			btn.HeaderText = "";
			btn.Text = "Оновити";
			btn.Name = "Кнопка1";
			btn.UseColumnTextForButtonValue = true;

			DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
			btn2.HeaderText = "";
			btn2.Text = "Видалити";
			btn2.Name = "Кнопка2";
			btn2.UseColumnTextForButtonValue = true;
			
			dataGridView1.Columns.AddRange(btn, btn2);
		}

		private void FillBanks()
		{
			dataGridView2.Columns.Clear();
			banksDataTable = new DataTable();
			banksDataTable.Columns.Add("RowId");
			banksDataTable.Columns.Add("Id");
			banksDataTable.Columns.Add("Назва банку");

			dataGridView2.DataSource = banksDataTable;

			DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
			btn.HeaderText = "";
			btn.Text = "Оновити";
			btn.Name = "Кнопка1";
			btn.UseColumnTextForButtonValue = true;

			DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
			btn2.HeaderText = "";
			btn2.Text = "Видалити";
			btn2.Name = "Кнопка2";
			btn2.UseColumnTextForButtonValue = true;

			dataGridView2.Columns.AddRange(btn, btn2);
		}

		private void FillCurrencies()
		{
			dataGridView3.Columns.Clear();
			currenciesDataTable = new DataTable();
			currenciesDataTable.Columns.Add("RowId");
			currenciesDataTable.Columns.Add("Id");
			currenciesDataTable.Columns.Add("Назва валюти");

			dataGridView3.DataSource = currenciesDataTable;

			DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
			btn.HeaderText = "";
			btn.Text = "Оновити";
			btn.Name = "Кнопка1";
			btn.UseColumnTextForButtonValue = true;

			DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
			btn2.HeaderText = "";
			btn2.Text = "Видалити";
			btn2.Name = "Кнопка2";
			btn2.UseColumnTextForButtonValue = true;

			dataGridView3.Columns.AddRange(btn, btn2);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			textBox1.Text = "";
			textBox2.Text = "";

			label1.Visible = true;
			label2.Visible = true;
			label8.Visible = true;
			textBox1.Visible = true;
			textBox2.Visible = true;
			button5.Visible = true;
			button10.Visible = true;
			button10.Text = "Додати";

			using (DBAppContext db = new DBAppContext())
			{
				comboBox1.Visible = true;
				comboBox1.DataSource = db.Banks.ToList();
				comboBox1.DisplayMember = "BankName";
			}
		}

		private void button10_Click(object sender, EventArgs e)
		{
			var lastName = textBox1.Text.Trim();
			var firstName = textBox2.Text.Trim();
			var bank = comboBox1.SelectedItem as Bank;

			if (string.IsNullOrEmpty(lastName))
			{
				MessageBox.Show("Ви не ввели ім'я!");
				return;
			}

			if (string.IsNullOrEmpty(firstName))
			{
				MessageBox.Show("Ви не ввели прізвище!");
				return;
			}

			if (bank == null)
			{
				MessageBox.Show("Ви не обрали банк!");
				return;
			}

			using (DBAppContext db = new DBAppContext())
			{
				var bankInDb = db.Banks.FirstOrDefault(b => b.Id == bank.Id);

				if (button10.Text.Equals("Додати"))
				{
					var chars = "QWERTYUIOP[]qwertyuiop1234567890";
					var stringChars = new char[6];
					var random = new Random();

					for (int i = 0; i < stringChars.Length; i++)
					{
						stringChars[i] = chars[random.Next(chars.Length)];
					}

					db.Users.Add(new User()
					{
						FirstName = firstName,
						LastName = lastName,
						UserName = $"{firstName}{lastName}",
						Bank = bankInDb,
						Password = new string(stringChars),
						IsBankUser = true
					});
				} else if (button10.Text.Equals("Оновити"))
				{
					var user = db.Users.Include(u => u.Bank).FirstOrDefault(u => u.Id == Convert.ToInt32(label5.Text));
					user.Bank = bankInDb;
					user.FirstName = firstName;
					user.LastName = lastName;

					db.Users.Update(user);
				}
				
				db.SaveChanges();
			}

			label1.Visible = false;
			label2.Visible = false;
			label8.Visible = false;
			textBox1.Visible = false;
			textBox2.Visible = false;
			button5.Visible = false;
			comboBox1.Visible = false;
			button10.Visible = false;

			FillDataTables();
		}

		private void button5_Click(object sender, EventArgs e)
		{
			label1.Visible = false;
			label2.Visible = false;
			label8.Visible = false;
			textBox1.Visible = false;
			textBox2.Visible = false;
			button5.Visible = false;
			comboBox1.Visible = false;
			button10.Visible = false;
		}

		private void button4_Click(object sender, EventArgs e)
		{
			label4.Visible = true;
			textBox4.Visible = true;
			textBox4.Text = "";
			button6.Visible = true;
			button6.Text = "Додати";
			button2.Visible = true;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			label4.Visible = false;
			textBox4.Visible = false;
			button6.Visible = false;
			button2.Visible = false;
		}

		private void button6_Click(object sender, EventArgs e)
		{
			var bankName = textBox4.Text.Trim();

			if (string.IsNullOrEmpty(bankName))
			{
				MessageBox.Show("Ви не ввели банк!");
				return;
			}

			using (DBAppContext db = new DBAppContext())
			{
				if (button6.Text.Equals("Додати"))
				{
					db.Banks.Add(new Bank()
					{
						BankName = bankName
					});
				} else if (button6.Text.Equals("Оновити"))
				{
					var bank = db.Banks.FirstOrDefault(u => u.Id == Convert.ToInt32(label6.Text));
					bank.BankName = bankName;

					db.Banks.Update(bank);
				}

				db.SaveChanges();
			}

			label4.Visible = false;
			textBox4.Visible = false;
			button6.Visible = false;
			button2.Visible = false;

			FillDataTables();
		}

		private void button7_Click(object sender, EventArgs e)
		{
			label3.Visible = true;
			textBox3.Text = "";
			button8.Text = "Додати";
			textBox3.Visible = true;
			button3.Visible = true;
			button8.Visible = true;
		}

		private void button8_Click(object sender, EventArgs e)
		{
			var currencyName = textBox3.Text.Trim();

			if (string.IsNullOrEmpty(currencyName))
			{
				MessageBox.Show("Ви не ввели валюту!");
				return;
			}

			using (DBAppContext db = new DBAppContext())
			{
				if (button8.Text.Equals("Додати"))
				{
					db.Currencies.Add(new Currency()
					{
						CurrencyName = currencyName
					});
				} else if (button8.Text.Equals("Оновити"))
				{
					var currency = db.Currencies.FirstOrDefault(u => u.Id == Convert.ToInt32(label7.Text));
					currency.CurrencyName = currencyName;

					db.Currencies.Update(currency);
				}
				
				db.SaveChanges();
			}

			FillDataTables();

			label3.Visible = false;
			textBox3.Visible = false;
			button3.Visible = false;
			button8.Visible = false;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			label3.Visible = false;
			textBox3.Visible = false;
			button3.Visible = false;
			button8.Visible = false;
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 &&
			    e.RowIndex + 1 < dataGridView1.Rows.Count)
			{
				if (e.ColumnIndex == dataGridView1.Columns["Кнопка2"].Index)
				{
					DialogResult dr1 = MessageBox.Show("Ви хочете видалити користувача?", "Підтвердження", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
					if (dr1 == DialogResult.Yes)
					{
						using (DBAppContext db = new DBAppContext())
						{
							DataRow[] dr = usersDataTable.Select("ROWID = " + (e.RowIndex + 1));
							var userId = Convert.ToInt32(dr[0].ItemArray[1]);
							var user = db.Users.Include(p => p.Bank).ToList().FirstOrDefault(p => p.Id == userId);

							db.Users.Remove(user);
							db.SaveChanges();

							FillDataTables();
						}
					}
				} else if (e.ColumnIndex == dataGridView1.Columns["Кнопка1"].Index)
				{
					label1.Visible = true;
					label2.Visible = true;
					label8.Visible = true;
					textBox1.Visible = true;
					textBox2.Visible = true;
					button5.Visible = true;
					button10.Visible = true;

					using (DBAppContext db = new DBAppContext())
					{
						DataRow[] dr = usersDataTable.Select("ROWID = " + (e.RowIndex + 1));
						var userId = Convert.ToInt32(dr[0].ItemArray[1]);
						var user = db.Users.Include(p => p.Bank).ToList().FirstOrDefault(p => p.Id == userId);

						label5.Text = user.Id.ToString();
						textBox1.Text = user.LastName;
						textBox2.Text = user.FirstName;
						button10.Text = "Оновити";

						comboBox1.Visible = true;
						comboBox1.DataSource = db.Banks.ToList();
						comboBox1.SelectedItem = user.Bank;
						comboBox1.DisplayMember = "BankName";
					}
				}
			}
		}

		private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (dataGridView2.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 &&
			    e.RowIndex + 1 < dataGridView2.Rows.Count)
			{
				if (e.ColumnIndex == dataGridView2.Columns["Кнопка2"].Index)
				{
					DialogResult dr1 = MessageBox.Show("Ви хочете видалити банк?\nУВАГА: При видаленні банку будуть видалені всі користувачі та курси валют, які до нього відносяться", "Підтвердження", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
					if (dr1 == DialogResult.Yes)
					{
						using (DBAppContext db = new DBAppContext())
						{
							DataRow[] dr = banksDataTable.Select("ROWID = " + (e.RowIndex + 1));
							var bankId = Convert.ToInt32(dr[0].ItemArray[1]);
							var bank = db.Banks.ToList().FirstOrDefault(p => p.Id == bankId);

							var user = db.Users.Include(bc => bc.Bank)
								.Where(bc => bc.Bank.Id == bankId).ToList();
							db.Users.RemoveRange(user);

							var bankCurrencies = db.BankCurrencies.Include(bc => bc.Currency).Include(bc => bc.Bank)
								.Where(bc => bc.Bank.Id == bankId).ToList();
							db.BankCurrencies.RemoveRange(bankCurrencies);

							db.Banks.Remove(bank);
							db.SaveChanges();

							FillDataTables();
						}
					}
				} else if (e.ColumnIndex == dataGridView2.Columns["Кнопка1"].Index)
				{
					label4.Visible = true;
					textBox4.Visible = true;
					button6.Visible = true;
					button2.Visible = true;

					using (DBAppContext db = new DBAppContext())
					{
						DataRow[] dr = banksDataTable.Select("ROWID = " + (e.RowIndex + 1));
						var bankId = Convert.ToInt32(dr[0].ItemArray[1]);
						var bank = db.Banks.ToList().FirstOrDefault(p => p.Id == bankId);

						label6.Text = bankId.ToString();
						textBox4.Text = bank.BankName;
						button6.Text = "Оновити";
					}
				}
			}
		}

		private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (dataGridView3.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 &&
			    e.RowIndex + 1 < dataGridView3.Rows.Count)
			{
				if (e.ColumnIndex == dataGridView3.Columns["Кнопка2"].Index)
				{
					DialogResult dr1 = MessageBox.Show("Ви хочете видалити валюту?\nУВАГА: При видаленні валюти будуть видалені всі курси валют, які до неї відносяться", "Підтвердження", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
					if (dr1 == DialogResult.Yes)
					{
						using (DBAppContext db = new DBAppContext())
						{
							DataRow[] dr = currenciesDataTable.Select("ROWID = " + (e.RowIndex + 1));
							var currencyId = Convert.ToInt32(dr[0].ItemArray[1]);
							var currency = db.Currencies.ToList().FirstOrDefault(p => p.Id == currencyId);

							var bankCurrencies = db.BankCurrencies.Include(bc => bc.Currency).Include(bc => bc.Bank)
								.Where(bc => bc.Currency.Id == currencyId).ToList();
							db.BankCurrencies.RemoveRange(bankCurrencies);

							db.Currencies.Remove(currency);
							db.SaveChanges();

							FillDataTables();
						}
					}
				} else if (e.ColumnIndex == dataGridView3.Columns["Кнопка1"].Index)
				{
					using (DBAppContext db = new DBAppContext())
					{
						DataRow[] dr = currenciesDataTable.Select("ROWID = " + (e.RowIndex + 1));
						var currencyId = Convert.ToInt32(dr[0].ItemArray[1]);
						var currency = db.Currencies.ToList().FirstOrDefault(p => p.Id == currencyId);

						label3.Visible = true;
						textBox3.Visible = true;
						button8.Visible = true;
						button3.Visible = true;

						label6.Text = currencyId.ToString();
						textBox3.Text = currency.CurrencyName;
						label7.Text = currencyId.ToString();
						button8.Text = "Оновити";
					}
				}
			}
		}
	}
}
