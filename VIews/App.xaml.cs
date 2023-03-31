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
		InitializeComponent();

		MainPage = new AppShell();
	}
}
