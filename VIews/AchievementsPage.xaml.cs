using AddictionApp.ViewModels;

namespace AddictionApp.Views;

public partial class AchievementsPage : ContentPage
{
	public AchievementsPage()
	{
		InitializeComponent();
		BindingContext = new AchievementsPageVM();

    }
}