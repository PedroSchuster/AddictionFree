using AddictionApp.Data;
using AddictionApp.Entidades;
using AddictionApp.ViewModels;

namespace AddictionApp.Views;

public partial class HomePage : ContentPage
{
    ViewCell lastCell;

    public HomePage()
	{
		InitializeComponent();

		BindingContext = new HomePageVM();

        addictionsListView.ItemSelected += async (s, e) => 
        {
            if (e.SelectedItem as Addiction != null)
            {
                DataContainer.Instance.Initialize(e.SelectedItem as Addiction);
                await Navigation.PushAsync(new AppShell());
                addictionsListView.SelectedItem = null;
            }
        };
    }


}