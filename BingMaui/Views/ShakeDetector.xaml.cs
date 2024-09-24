namespace BingMaui.Views;

public partial class ShakeDetector : ContentPage
{
	public ShakeDetector()
	{
		InitializeComponent();
		
	}

    private void EventTrigger_PropertyChanging(object sender, PropertyChangingEventArgs e) {
        ShakeLabel.TextColor = new Color(Random.Shared.Next(256), Random.Shared.Next(256), Random.Shared.Next(256));
        ShakeLabel.Text = $"Shake detected";
    }
}