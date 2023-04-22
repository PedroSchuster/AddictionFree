using AddictionApp.Views;

namespace AddictionApp;

public partial class MainPageShell : Shell
{
	public MainPageShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(AchievementsPage), typeof(AchievementsPage));

    }
}
