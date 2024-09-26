using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Core;

namespace BingMaui.ViewModels {
    public partial class ShakeDetectorViewModel : ObservableObject {

        [ObservableProperty]
        Color _randomColor;

        public ShakeDetectorViewModel() {
            StartAccelerometer();
        }

        [RelayCommand]
        public async Task OnShakeDetected() {
            Random random = new Random();
            RandomColor = Color.FromRgb(random.Next(256), random.Next(256), random.Next(256));
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


    
}
