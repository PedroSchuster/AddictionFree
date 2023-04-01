using AddictionApp.ViewModels;

namespace AddictionApp.Views;

public partial class ProgressPage : ContentPage
{
	public ProgressPage()
	{
		InitializeComponent();

        BindingContext = new ViewModels.ProgressPage();
	}
}