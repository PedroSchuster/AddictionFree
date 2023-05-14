using AddictionApp.Data;
using AddictionApp.Entidades;
using AddictionApp.ViewModels;

namespace AddictionApp.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
	{
		InitializeComponent();

		BindingContext = new HomePageVM();

        addictionsListView.ItemSelected += async (s, e) => 
        {
            if (e.SelectedItem as Addiction != null)
            {
                await DataContainer.Instance.Initialize(e.SelectedItem as Addiction);
                await Shell.Current.GoToAsync(nameof(ProgressPage), true);
                addictionsListView.SelectedItem = null;
            }
        };
    }


}