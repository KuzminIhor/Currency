using System;
using System.Linq;
using System.Security.Authentication;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using CurrencyApp.Core;
using CurrencyApp.Model;
using Курсова.Services;

namespace CurrencyApp
{
    public partial class MainForm : Form
    {
	    private static MainForm _mainForm;

        private MainForm()
        {
            InitializeComponent();
        }

        public static MainForm GetInstance()
        {
	        if (_mainForm == null)
	        {
		        _mainForm = new MainForm();
	        }

	        return _mainForm;
        }

		private void LoginButton_Click(object sender, EventArgs e)
		{
			label2.Visible = false;
			label2.Text = "";
			var userName = LoginTextBox.Text.Trim();
			var password = PasswordTextBox.Text.Trim();

			try
			{
				var formToRedirect = (Form) AuthenticationService.Authenticate(userName, password);
				FormRedirection.Redirect(this, formToRedirect);
			}
			catch (AuthenticationException ex)
			{
				label2.Visible = true;
				label2.Text = ex.Message;
			}
		}

		private void GuestButton_Click(object sender, EventArgs e)
		{
			try
			{
				var formToRedirect = (Form) AuthenticationService.AuthenticateGuest();
				FormRedirection.Redirect(this, formToRedirect);
			}
			catch (AuthenticationException ex)
			{
				label2.Visible = true;
				label2.Text = ex.Message;
			}
		}
	}
}
