using AddictionApp.ViewModels;

namespace AddictionApp.Views;

public partial class RegisterAddictionPage : ContentPage
{
	public RegisterAddictionPage()
	{
		InitializeComponent();


		BindingContext = new RegisterAddictionPageVM();
	}
}