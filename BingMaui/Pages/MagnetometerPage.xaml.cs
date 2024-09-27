using BingMaui.ViewModels;

namespace BingMaui.Pages;

public partial class MagnetometerPage : ContentPage
{
    public MagnetometerPage(MagnetometerViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;

    }
    /*
    private void ToggleMagnetometer() {
        if (Magnetometer.Default.IsSupported) {
            if (!Magnetometer.Default.IsMonitoring) {
                // Turn on magnetometer
                Magnetometer.Default.ReadingChanged += Magnetometer_ReadingChanged;
                Magnetometer.Default.Start(SensorSpeed.UI);
            } else {
                // Turn off magnetometer
                Magnetometer.Default.Stop();
                Magnetometer.Default.ReadingChanged -= Magnetometer_ReadingChanged;
            }
        }
        else {
            hasItLabel.Text = "doesn't have it";
        }
    }

    private void Magnetometer_ReadingChanged(object sender, MagnetometerChangedEventArgs e) {
        // Update UI Label with magnetometer state
        MagnetometerLabel.TextColor = Colors.Green;
        MagnetometerLabel.Text = $"Magnetometer: \nX: {e.Reading.MagneticField.X}\nY: {e.Reading.MagneticField.Y}\nZ: {e.Reading.MagneticField.Z}";
        var s = e.Reading.MagneticField;
    } */
}