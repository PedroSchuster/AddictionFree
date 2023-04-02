using AddictionApp.ViewModels;

namespace AddictionApp.Views;

public partial class NotesPage : ContentPage
{
	public NotesPage()
	{
		InitializeComponent();
		BindingContext = new NotesPageVM();
	}
}