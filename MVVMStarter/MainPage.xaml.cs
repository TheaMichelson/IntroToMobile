using MVVMStarter.Services;
using MVVMStarter.ViewModels;
using MVVMStarter.Views;

namespace MVVMStarter;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        IPlanetService planetService = new PlanetService();
        IStarService starService = new StarService();
        BindingContext = new MainPageViewModel(planetService, starService);
	}

    private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        //null conditional operator to access the SelectedItem property and the ToString to compare it with "Fruits"
        if (sender is ListView lv && lv.SelectedItem?.ToString() == "Planets")
        {
            Navigation.PushAsync(new PlanetsPage());
        }
        else if (sender is ListView lv2 && lv2.SelectedItem?.ToString() == "Stars")
        {
            Navigation.PushAsync(new StarsPage());
        }

    }
}

