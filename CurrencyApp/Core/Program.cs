using System;
using System.Windows.Forms;
using NLog;
using CurrencyApp.AppData.Unity;

namespace CurrencyApp.Core
{
    internal static class Program
    {
	    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

	    [STAThread]
	    static void Main()
	    {
			_logger.Info("Запуск програми");

			UnityConfig.Register();

			_logger.Info("Залежності зареєстровані");

			InitializeDatabase();

			_logger.Info("База даних перевірена на існування");

			Application.EnableVisualStyles();
		    Application.SetCompatibleTextRenderingDefault(false);
		    Application.Run(MainForm.GetInstance());
	    }

	    private static void InitializeDatabase()
	    {
		    DBAppContext db = new DBAppContext();
		    db.Database.EnsureCreated();
	    }
    }
}
