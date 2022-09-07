using System;
using System.Security.Authentication;
using System.Windows.Forms;
using CurrencyApp.Core;
using CurrencyApp.Interfaces;
using CurrencyApp.Model;
using CurrencyApp.Model.Enums;
using NLog;

namespace CurrencyApp
{
    public partial class MainForm : Form
    {
	    private static MainForm _mainForm;
	    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

	    private readonly IFormRedirectionService formRedirectionService;
	    private readonly IAuthenticationService authenticationService;

	    private MainForm()
	    {
		    formRedirectionService = ServiceLocator.Get<IFormRedirectionService>();
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
				var formToRedirect = (FormType) authenticationService.Authenticate(userName, password);
				formRedirectionService.Redirect(this, formToRedirect);

				var userType = CurrentUser.GetInstance().Id == 1 ? "Адмін" : "Банк";
				_logger.Info($"Аутентифікація пройшла успішно. Тип користувача: {userType}. ID користувача: {CurrentUser.GetInstance().Id}");
			}
			catch (AuthenticationException ex)
			{
				label2.Visible = true;
				label2.Text = ex.Message;
				_logger.Error($"ПОМИЛКА під час аутентифікації користувачем: {ex.Message}");
			}
			catch (Exception ex)
			{
				label2.Visible = true;
				label2.Text = "Сталась якась помилка";
				_logger.Error($"ПОМИЛКА під час аутентифікації користувачем: {ex.Message}");
			}
		}

		private void GuestButton_Click(object sender, EventArgs e)
		{
			try
			{
				var formToRedirect = (FormType) authenticationService.AuthenticateGuest();
				formRedirectionService.Redirect(this, formToRedirect);
				_logger.Info("Аутентифікація пройшла успішно. Тип користувача: Гість");
			}
			catch (Exception ex)
			{
				label2.Visible = true;
				label2.Text = "Сталась якась помилка";
				_logger.Error($"ПОМИЛКА під час аутентифікації гостем: {ex.Message}");
			}
		}
	}
}
