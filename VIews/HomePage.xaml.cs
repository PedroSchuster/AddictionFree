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

        addictionsListView.ItemSelected += (s, e) =>
        {
            if (e.SelectedItem as Addiction != null)
            {
                DataContainer.Instance.Initialize(e.SelectedItem as Addiction);
                Navigation.PushAsync(new ProgressPage());
                addictionsListView.SelectedItem = null;
            }
        };
    }


}