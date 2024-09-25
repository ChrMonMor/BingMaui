namespace BingMaui.Views;

public partial class ShakeDetectorPage : ContentPage
{
    private int r;
    private int g;
    private int b;
    public ShakeDetectorPage() {
        InitializeComponent();
    }

    protected override void OnAppearing() {
        base.OnAppearing();
        Accelerometer.ShakeDetected += OnShakeDetected;
        StartAccelerometer();
    }

    protected override void OnDisappearing() {
        base.OnDisappearing();
        Accelerometer.ShakeDetected -= OnShakeDetected;
        StopAccelerometer();
    }

    private void OnShakeDetected(object sender, EventArgs e) {
        // Randomly change background color
        Random random = new Random();
        ShakeGrid.BackgroundColor = Color.FromRgb(random.Next(256), random.Next(256), random.Next(256));
    }

    private void StartAccelerometer() {
        try {
            if (!Accelerometer.IsSupported)
                return;
            if (!Accelerometer.IsMonitoring)
                Accelerometer.Start(SensorSpeed.Game); // Game speed for better shake detection 20ms
        } catch (FeatureNotSupportedException) {
            // Handle feature not supported on device
        }
    }

    private void StopAccelerometer() {
        try {
            if (Accelerometer.IsMonitoring)
                Accelerometer.Stop();
        } catch (Exception ex) {
            // Handle exception
        }
    }
}