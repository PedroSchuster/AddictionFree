using System.Collections.ObjectModel;
using AddictionApp.Entidades;
using AddictionApp.Services;
using AddictionApp.Views;
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
                await DataBase.CreateTableAsync<AddictionNote>();
                await DataBase.CreateTableAsync<ResetDate>();
                await DataBase.CreateTableAsync<Achievement>();
            });

        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }

        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt+QHFqVkNrXVNbdV5dVGpAd0N3RGlcdlR1fUUmHVdTRHRcQlljSHxTd0RjUXZadH0=;Mgo+DSMBPh8sVXJ1S0d+X1RPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSXpTd0djWH5eeH1QQWk=;ORg4AjUWIQA/Gnt2VFhhQlJBfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5XdkRhWX5fcHxcQWhV;MTU4NDAxOEAzMjMxMmUzMTJlMzMzNWV3UlZWeFJldEFBYmlXait6WExORGhTS1hlZE5UQVNQZThzTUhzSnRrODA9;MTU4NDAxOUAzMjMxMmUzMTJlMzMzNWxQWElzYlIyaTN5WUhNOFhmOURmRklmc1RWaHc3aG5jZUsxdjM4a1Y3QlU9;NRAiBiAaIQQuGjN/V0d+XU9Hc1RDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS31TckViW39fcXRcRmVcWA==;MTU4NDAyMUAzMjMxMmUzMTJlMzMzNWVjYkhGUjloN3pPemM1ZWpWTWt5cnJ5eVB3Zkp0anV1aXVxalVLWmVmRmc9;MTU4NDAyMkAzMjMxMmUzMTJlMzMzNVcvRlNLUy92THNndHRieUIxSXZwYzNRRHJId0Npa3lvbWVjMVZ3ZVg0UkE9;Mgo+DSMBMAY9C3t2VFhhQlJBfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5XdkRhWX5fcH1UR2lV;MTU4NDAyNEAzMjMxMmUzMTJlMzMzNVF5cFlWUFZVS05wTTBBVTQxeGFTeVN5eFlwSUc2U3V0SVFBbnBkUXgrVUU9;MTU4NDAyNUAzMjMxMmUzMTJlMzMzNVdiVE0xN0xxZVVBZm1mbGhRazZJR1VzMDdYU2Z2UElRQU94SVJTVTlVT289;MTU4NDAyNkAzMjMxMmUzMTJlMzMzNWVjYkhGUjloN3pPemM1ZWpWTWt5cnJ5eVB3Zkp0anV1aXVxalVLWmVmRmc9");

        InitializeComponent();


		MainPage = new MainPageShell();
    }
}
