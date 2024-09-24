namespace BingMaui.Views;

public partial class BarometerPage : ContentPage
{
	public BarometerPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing() {
        base.OnAppearing();
        Barometer.Default.ReadingChanged += OnReadingChanged;
        StartBarometer();
    }

    protected override void OnDisappearing() {
        base.OnDisappearing();
        Barometer.Default.ReadingChanged -= OnReadingChanged;
        StopBarometer();
    }

    private void OnReadingChanged(object? sender, BarometerChangedEventArgs e) {
        BarometerLabel.Text = $" Barometer level: {Math.Floor(e.Reading.PressureInHectopascals)}";
    }

    private void StartBarometer() {
        try {
            if (!Barometer.IsMonitoring)
                Barometer.Start(SensorSpeed.UI); // Game speed for better shake detection 20ms
        } catch (FeatureNotSupportedException) {
            // Handle feature not supported on device
        }
    }

    private void StopBarometer() {
        try {
            if (Barometer.IsMonitoring)
                Barometer.Stop();
        } catch (Exception ex) {
            // Handle exception
        }
    }
}