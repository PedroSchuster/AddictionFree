using AddictionApp.ViewModels;

namespace AddictionApp.Views;

public partial class NotesPage : ContentPage
{
    private static NotesPageVM vm;
    public NotesPage()
	{
		InitializeComponent();
        vm = new NotesPageVM();
        BindingContext = vm;

    }
    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Label selectedItem = e.CurrentSelection.FirstOrDefault() as Label;

        collectionViewDates.ScrollTo(selectedItem, null, ScrollToPosition.Center, true);

        selectedItem.IsEnabled = true;

    }

    private void collectionViewDates_Scrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        vm.UpdateMonth(e.CenterItemIndex);
        if ((e.LastVisibleItemIndex - 5) == (vm.Dates.Count - 6))
            vm.UpdateCollectionView(1);
        else if (e.FirstVisibleItemIndex == 0)
        {
            vm.UpdateCollectionView(-1);
        }
    }
}