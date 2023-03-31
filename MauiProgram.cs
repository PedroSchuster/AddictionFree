using SQLite;

namespace AddictionApp;

public static class MauiProgram
{
    private static SQLiteAsyncConnection _database;
    public static SQLiteAsyncConnection DataBase
    {
        get
        {
            if (_database == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "control.db3");
                _database = new SQLiteAsyncConnection(dbPath);
            }
            return _database;
        }

    }


    public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		return builder.Build();
	}
}
