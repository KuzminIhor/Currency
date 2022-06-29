using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using CurrencyApp.Core;
using CurrencyApp.Model;

namespace CurrencyApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

		private void LoginButton_Click(object sender, EventArgs e)
		{
			label2.Visible = false;
			label2.Text = "";
			var userName = LoginTextBox.Text.Trim();
			var password = PasswordTextBox.Text.Trim();

			if (string.IsNullOrEmpty(userName))
			{
				label2.Text += "Ви не ввели логін!\n";
			}
			if (string.IsNullOrEmpty(password))
			{
				label2.Text += "Ви не ввели пароль!\n";
			}

			if (!string.IsNullOrEmpty(label2.Text))
			{
				label2.Visible = true;
				return;
			}

			using (DBAppContext db = new DBAppContext())
			{
				User user = db.Users.FirstOrDefault(u => u.UserName.Equals(userName));
				if (user == null)
				{
					label2.Visible = true;
					label2.Text = "Даного користувача не існує";
					return;
				}
				else if (!user.Password.Equals(password))
				{
					label2.Visible = true;
					label2.Text = "Уведений неправильний пароль";
					return;
				}

				CurrentUser currentUser1 = CurrentUser.GetInstance();
				currentUser1.Id = user.Id;
				currentUser1.IsBankUser = user.IsBankUser;

				Form form = null;

				if (user.UserName.Equals("admin"))
				{
					form = new AdminForm();
				} else if (user.IsBankUser)
				{
					form = new BankUserForm();
				}

				this.Hide();
				form.Show();
			}
		}

		private void GuestButton_Click(object sender, EventArgs e)
		{
			CurrentUser currentUser = CurrentUser.GetInstance();
			using (DBAppContext db = new DBAppContext())
			{
				currentUser.Id = db.Users.Include(u => u.Bank).FirstOrDefault(u => u.UserName == null).Id;
				currentUser.IsBankUser = false;
			}

			Form form = new GuestForm();
			this.Hide();
			form.Show();
		}
	}
}
