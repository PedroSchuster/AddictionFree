using AddictionApp.ViewModels;

namespace AddictionApp.Views;

public partial class ProgressPage : ContentPage
{
    ProgressPageVM vm;

    public ProgressPage()
	{
		InitializeComponent();

	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        vm = new ProgressPageVM();
        BindingContext = vm;

        vm.UpdateData.Start();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        vm.Source.Cancel();
    }
}