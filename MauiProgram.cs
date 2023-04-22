using AddictionApp.Services;
using AddictionApp.Views;
using SQLite;
using Syncfusion.Maui.Core.Hosting;

namespace AddictionApp;

public static class MauiProgram
{

    public static MauiApp CreateMauiApp()
	{

		SingletonContainer.Init();

		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .ConfigureSyncfusionCore()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        


        return builder.Build();
	}

	
}
