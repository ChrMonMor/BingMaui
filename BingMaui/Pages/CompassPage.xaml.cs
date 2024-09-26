namespace BingMaui.Pages;

public partial class CompassPage : ContentPage
{
    public CompassPage() {
        InitializeComponent();
    }

    protected override void OnAppearing() {
        base.OnAppearing();
        if (Compass.IsSupported) {
            Compass.ReadingChanged += OnCompassReadingChanged;
            Compass.Start(SensorSpeed.UI);
        } else {
            CompassLabel.Text = "Compass not supported on this device.";
        }
    }

    protected override void OnDisappearing() {
        base.OnDisappearing();
        if (Compass.IsSupported) {
            Compass.Stop();
            Compass.ReadingChanged -= OnCompassReadingChanged;
        }
    }

    private void OnCompassReadingChanged(object sender, CompassChangedEventArgs e) {
        var heading = e.Reading.HeadingMagneticNorth;
        CompassLabel.Text = $"Heading: {heading:F1}°";
        CompassImage.Rotation = -heading;
    }
}