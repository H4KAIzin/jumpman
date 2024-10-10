namespace pintopulante;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}
public void Starting(object sender, EventArgs e)
{
	Navigation.PushAsync(new GamePage());
}
	

}

