using System.Collections.ObjectModel;
using AddictionApp.Entidades;
using AddictionApp.Services;
using SQLite;

namespace AddictionApp;

public partial class App : Application
{
    private static SQLiteAsyncConnection _database;
    public static SQLiteAsyncConnection DataBase
    {
        get
        {
            if (_database == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "addictionApp.db3");
                _database = new SQLiteAsyncConnection(dbPath);
            }
            return _database;
        }

    }

    public App()
	{
        try
        {
            AddictionService a = new AddictionService();

            Task.Run(async () =>

            {
                await DataBase.CreateTableAsync<Addiction>();


            });

        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }

		InitializeComponent();

		MainPage = new AppShell();
	}
}
