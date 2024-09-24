using BingMaui.Controls;
namespace BingMaui.Views;

public partial class GyroskopPage : ContentPage
{
    private bool gyroNext;
	public GyroskopPage()
	{
		InitializeComponent();
        Content = new CubeView();
        gyroNext = true;

    }
    protected override void OnAppearing() {
        base.OnAppearing();
        StartGyroskop();
    }


    private void ToggleGyroskop() {
        if (Gyroscope.IsSupported)
        {
            if (gyroNext) {
                StartGyroskop();
            } else {
                StopGyroskop();
            }
        }
    }

    private void StartGyroskop() {
        try {
            if (!Gyroscope.IsMonitoring) {
                Gyroscope.Default.ReadingChanged += Gyroscope_ReadingChanged;
                gyroNext = false;
                Gyroscope.Start(SensorSpeed.UI); // Game speed for better shake detection 20ms
            }
        } catch (FeatureNotSupportedException) {
            // Handle feature not supported on device
        }
    }

    private void Gyroscope_ReadingChanged(object? sender, GyroscopeChangedEventArgs e) {
        GyroLabel.TextColor = Colors.Green;
        GyroLabel.Text = $"Gyroscope: {e.Reading}";
    }

    private void StopGyroskop() {
        try {
            if (Gyroscope.IsMonitoring) {
                Gyroscope.Default.ReadingChanged -= Gyroscope_ReadingChanged;
                gyroNext = true;
                Gyroscope.Stop();
            }
        } catch (FeatureNotSupportedException) {
            // Handle exception
        }
    }
}