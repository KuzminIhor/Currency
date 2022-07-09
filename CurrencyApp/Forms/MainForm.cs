using System;
using System.Linq;
using System.Security.Authentication;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using CurrencyApp.Core;
using CurrencyApp.Interfaces;
using CurrencyApp.Model;
using NLog;
using CurrencyApp.Services;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace CurrencyApp
{
    public partial class MainForm : Form
    {
	    private static MainForm _mainForm;
	    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

	    private readonly IFormRedirection formRedirection;
	    private readonly IAuthenticationService authenticationService;

	    private MainForm()
	    {
		    formRedirection = ServiceLocator.Get<IFormRedirection>();
		    authenticationService = ServiceLocator.Get<IAuthenticationService>();

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
				var formToRedirect = (Form) authenticationService.Authenticate(userName, password);
				formRedirection.Redirect(this, formToRedirect);

				var userType = CurrentUser.GetInstance().Id == 1 ? "Адмін" : "Банк";
				_logger.Info($"Аутентифікація пройшла успішно. Тип користувача: {userType}");
			}
			catch (AuthenticationException ex)
			{
				label2.Visible = true;
				label2.Text = ex.Message;
				_logger.Error($"ПОМИЛКА: {ex.Message}");
			}
			catch (Exception ex)
			{
				label2.Visible = true;
				label2.Text = "Сталась якась помилка";
				_logger.Error($"ПОМИЛКА: {ex.Message}");
			}
		}

		private void GuestButton_Click(object sender, EventArgs e)
		{
			try
			{
				var formToRedirect = (Form) authenticationService.AuthenticateGuest();
				formRedirection.Redirect(this, formToRedirect);
				_logger.Info("Аутентифікація пройшла успішно. Тип користувача: Гість");
			}
			catch (Exception ex)
			{
				label2.Visible = true;
				label2.Text = "Сталась якась помилка";
				_logger.Error($"ПОМИЛКА: {ex.Message}");
			}
		}
	}
}
