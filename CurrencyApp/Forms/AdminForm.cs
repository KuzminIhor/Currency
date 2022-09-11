using System;
using System.Data;
using System.Windows.Forms;
using CurrencyApp.Core;
using CurrencyApp.Helpers;
using CurrencyApp.Interfaces;
using CurrencyApp.Model;
using CurrencyApp.Model.Exceptions;
using CurrencyApp.Repositories.Interfaces;
using CurrencyApp.Services;
using NLog;

namespace CurrencyApp
{
	public partial class AdminForm : Form
	{
		private DataTable usersDataTable = new DataTable();
		private DataTable banksDataTable = new DataTable();
		private DataTable currenciesDataTable = new DataTable();

		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

		private readonly IBankService bankService;
		private readonly ICurrencyService currencyService;
		private readonly IUserService userService;

		private readonly IRenderDataTableRows renderBank;
		private readonly IRenderDataTableRows renderUser;
		private readonly IRenderDataTableRows renderCurrency;

		private readonly IBankRepository bankRepository;
		private readonly IUserRepository userRepository;
		private readonly ICurrencyRepository currencyRepository;

		private static AdminForm _adminForm;

		private AdminForm()
		{
			bankService = ServiceLocator.Get<IBankService>();
			currencyService = ServiceLocator.Get<ICurrencyService>();
			userService = ServiceLocator.Get<IUserService>();

			renderBank = ServiceLocator.Get<IRenderDataTableRows>(nameof(RenderBankDataTableRowsService));
			renderUser = ServiceLocator.Get<IRenderDataTableRows>(nameof(RenderUserDataTableRowsService));
			renderCurrency = ServiceLocator.Get<IRenderDataTableRows>(nameof(RenderCurrencyDataTableRowsService));

			bankRepository = ServiceLocator.Get<IBankRepository>();
			userRepository = ServiceLocator.Get<IUserRepository>();
			currencyRepository = ServiceLocator.Get<ICurrencyRepository>();

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
			btn.Name = "Оновити";
			btn.UseColumnTextForButtonValue = true;

			DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
			btn2.HeaderText = "";
			btn2.Text = "Видалити";
			btn2.Name = "Видалити";
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
			btn.Name = "Оновити";
			btn.UseColumnTextForButtonValue = true;

			DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
			btn2.HeaderText = "";
			btn2.Text = "Видалити";
			btn2.Name = "Видалити";
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
			btn.Name = "Оновити";
			btn.UseColumnTextForButtonValue = true;

			DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
			btn2.HeaderText = "";
			btn2.Text = "Видалити";
			btn2.Name = "Видалити";
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

			comboBox1.Visible = true;
			comboBox1.DataSource = bankRepository.GetBanks();
			comboBox1.DisplayMember = "BankName";
		}

		private void button10_Click(object sender, EventArgs e)
		{
			var lastName = textBox1.Text.Trim();
			var firstName = textBox2.Text.Trim();
			var bank = comboBox1.SelectedItem as Bank;

			if (button10.Text.Equals("Додати"))
			{
				try
				{
					userService.AddUser(firstName, lastName, bank);

					_logger.Info(
						$"Користувач був доданий адміністратором. Ім'я: {firstName}, Прізвище: {lastName}, ID банку: {bank?.Id}");
				}
				catch (UserModifyException ex)
				{
					MessageBox.Show(ex.Message);

					_logger.Error(
						$"ПОМИЛКА при додаванні користувача з іменем {firstName}, прізвищем {lastName}, ID банку {bank?.Id}: {ex.Message}");

					return;
				}
				catch (Exception ex)
				{
					_logger.Error(
						$"ПОМИЛКА при додаванні користувача з іменем {firstName}, прізвищем {lastName}, ID банку {bank?.Id}: {ex.Message}");
				}
			}
			else if (button10.Text.Equals("Оновити"))
			{
				try
				{
					userService.UpdateUser(Convert.ToInt32(label5.Text), firstName, lastName, bank);

					_logger.Info(
						$"Користувач з ID {Convert.ToInt32(label5.Text)} був змінений адміністратором. Ім'я: {firstName}, Прізвище: {lastName}, ID банку: {bank?.Id}");
				}
				catch (UserModifyException ex)
				{
					MessageBox.Show(ex.Message);

					_logger.Error(
						$"ПОМИЛКА при оновленні адміністратором користувача з ID {Convert.ToInt32(label5.Text)}, іменем {firstName}, прізвищем {lastName}, ID банку {bank?.Id}: {ex.Message}");

					return;
				}
				catch (Exception ex)
				{
					_logger.Error(
						$"ПОМИЛКА при оновленні адміністратором користувача з ID {Convert.ToInt32(label5.Text)}, іменем {firstName}, прізвищем {lastName}, ID банку {bank?.Id}: {ex.Message}");
				}
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
			
			if (button6.Text.Equals("Додати"))
			{
				try
				{
					bankService.AddBank(bankName);
					_logger.Info($"Банк з іменем {bankName} було додано адміністратором.");
				}
				catch (BankModifyException ex)
				{
					MessageBox.Show(ex.Message);
					_logger.Error($"ПОМИЛКА при додаванні Банку з іменем {bankName} адміністратором: {ex.Message}");
					return;
				}
				catch (Exception ex)
				{
					_logger.Error($"ПОМИЛКА при додаванні Банку з іменем {bankName} адміністратором: {ex.Message}");
				}
			}
			else if (button6.Text.Equals("Оновити"))
			{
				var bankId = Convert.ToInt32(label6.Text);

				try
				{
					bankService.UpdateBank(bankId, bankName);
					_logger.Info($"Банк з ID {bankId} було оновлено адміністратором. Нове ім'я Банку - {bankName}");
				}
				catch (BankModifyException ex)
				{
					MessageBox.Show(ex.Message);
					_logger.Error($"ПОМИЛКА при оновленні Банку з ID {bankId} адміністратором: {ex.Message}");
					return;
				}
				catch (Exception ex)
				{
					_logger.Error($"ПОМИЛКА при оновленні Банку з ID {bankId} адміністратором: {ex.Message}");
					return;
				}
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

			if (button8.Text.Equals("Додати"))
			{
				try
				{
					currencyService.AddCurrency(currencyName);

					_logger.Info($"Валюта з іменем {currencyName} була додана адміністратором.");
				}
				catch (CurrencyModifyException ex)
				{
					MessageBox.Show(ex.Message);

					_logger.Error(
						$"ПОМИЛКА під час додавання валюти з іменем {currencyName} адміністратором: {ex.Message}");

					return;
				}
				catch (Exception ex)
				{
					_logger.Error(
						$"ПОМИЛКА під час додавання валюти з іменем {currencyName} адміністратором: {ex.Message}");
				}
			}
			else if (button8.Text.Equals("Оновити"))
			{
				try
				{
					currencyService.UpdateCurrency(Convert.ToInt32(label7.Text), currencyName);

					_logger.Info(
						$"Валюта з ID {Convert.ToInt32(label7.Text)} була оновлена адміністратором. Нове ім'я - {currencyName}.");
				}
				catch (CurrencyModifyException ex)
				{
					MessageBox.Show(ex.Message);

					_logger.Error(
						$"ПОМИЛКА під час оновлення валюти з ID {Convert.ToInt32(label7.Text)} адміністратором: {ex.Message}");

					return;
				}
				catch (Exception ex)
				{
					_logger.Error(
						$"ПОМИЛКА під час оновлення валюти з ID {Convert.ToInt32(label7.Text)} адміністратором: {ex.Message}");
				}
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
				if (e.ColumnIndex == dataGridView1.Columns["Видалити"].Index)
				{
					DialogResult dr1 = MessageBox.Show("Ви хочете видалити користувача?", "Підтвердження",
						MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

					if (dr1 == DialogResult.Yes)
					{
						DataRow[] dr = usersDataTable.Select("ROWID = " + (e.RowIndex + 1));
						var userId = Convert.ToInt32(dr[0].ItemArray[1]);

						try
						{
							userService.RemoveUser(userId);
						}
						catch (Exception ex)
						{
							_logger.Error($"ПОМИЛКА при видаленні користувача з ID {userId}: {ex.Message}");
						}

						FillDataTables();
					}
				}
				else if (e.ColumnIndex == dataGridView1.Columns["Оновити"].Index)
				{
					label1.Visible = true;
					label2.Visible = true;
					label8.Visible = true;
					textBox1.Visible = true;
					textBox2.Visible = true;
					button5.Visible = true;
					button10.Visible = true;


					DataRow[] dr = usersDataTable.Select("ROWID = " + (e.RowIndex + 1));
					var userId = Convert.ToInt32(dr[0].ItemArray[1]);
					var user = userRepository.GetUser(userId);

					label5.Text = user.Id.ToString();
					textBox1.Text = user.LastName;
					textBox2.Text = user.FirstName;
					button10.Text = "Оновити";

					comboBox1.Visible = true;
					comboBox1.DataSource = bankRepository.GetBanks();
					comboBox1.SelectedItem = user.Bank;
					comboBox1.DisplayMember = "BankName";
				}
			}
		}

		private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (dataGridView2.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 &&
			    e.RowIndex + 1 < dataGridView2.Rows.Count)
			{
				if (e.ColumnIndex == dataGridView2.Columns["Видалити"].Index)
				{
					DialogResult dr1 = MessageBox.Show(
						"Ви хочете видалити банк?\nУВАГА: При видаленні банку будуть видалені всі користувачі та курси валют, які до нього відносяться",
						"Підтвердження", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

					if (dr1 == DialogResult.Yes)
					{
						DataRow[] dr = banksDataTable.Select("ROWID = " + (e.RowIndex + 1));
						var bankId = Convert.ToInt32(dr[0].ItemArray[1]);

						try
						{
							bankService.RemoveBank(bankId);

							_logger.Info(
								$"Банк з ID {bankId} було видалено адміністратором разом із курсами валют та працівниками, що до нього належали.");
						}
						catch (Exception ex)
						{
							_logger.Error($"ПОМИЛКА при видаленні Банку з ID {bankId}: {ex.Message}");
						}

						FillDataTables();
					}
				}
				else if (e.ColumnIndex == dataGridView2.Columns["Оновити"].Index)
				{
					label4.Visible = true;
					textBox4.Visible = true;
					button6.Visible = true;
					button2.Visible = true;

					DataRow[] dr = banksDataTable.Select("ROWID = " + (e.RowIndex + 1));
					var bankId = Convert.ToInt32(dr[0].ItemArray[1]);

					var bank = bankRepository.GetBank(bankId);

					label6.Text = bankId.ToString();
					textBox4.Text = bank.BankName;
					button6.Text = "Оновити";
				}
			}
		}

		private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (dataGridView3.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 &&
			    e.RowIndex + 1 < dataGridView3.Rows.Count)
			{
				if (e.ColumnIndex == dataGridView3.Columns["Видалити"].Index)
				{
					DialogResult dr1 =
						MessageBox.Show(
							"Ви хочете видалити валюту?\nУВАГА: При видаленні валюти будуть видалені всі курси валют, які до неї відносяться",
							"Підтвердження", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

					if (dr1 == DialogResult.Yes)
					{
						DataRow[] dr = currenciesDataTable.Select("ROWID = " + (e.RowIndex + 1));
						var currencyId = Convert.ToInt32(dr[0].ItemArray[1]);

						try
						{
							currencyService.RemoveCurrency(currencyId);

							_logger.Info(
								$"Валюта з ID {currencyId} була видалена адміністратором разом із курсами валют, які до неї відносяться.");
						}
						catch (Exception ex)
						{
							_logger.Error($"ПОМИЛКА при видаленні валюти з ID {currencyId}: {ex.Message}");
						}

						FillDataTables();
					}
				}
				else if (e.ColumnIndex == dataGridView3.Columns["Оновити"].Index)
				{

					DataRow[] dr = currenciesDataTable.Select("ROWID = " + (e.RowIndex + 1));
					var currencyId = Convert.ToInt32(dr[0].ItemArray[1]);
					var currency = currencyRepository.GetCurrency(currencyId);

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
