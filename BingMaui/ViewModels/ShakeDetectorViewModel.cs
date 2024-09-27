using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Core;

namespace BingMaui.ViewModels {
    public partial class ShakeDetectorViewModel : ObservableObject {

        [ObservableProperty]
        Color _randomColor;
        [ObservableProperty]
        bool _isBusy;

        public ShakeDetectorViewModel() {
            StartAccelerometer();
            RandomColor = Color.FromRgb(0, 0, 0);
            IsBusy = false;
        }

        public void OnShakeDetected() {
            Random random = new Random();
            RandomColor = Color.FromRgb(random.Next(256), random.Next(256), random.Next(256));
        }
        private void Accelerometer_ShakeDetected(object? sender, EventArgs e) {
            OnShakeDetected();
        }

        private void StartAccelerometer() {
            try {
                if (!Accelerometer.IsSupported)
                    return;
                if (!Accelerometer.IsMonitoring)
                    Accelerometer.Start(SensorSpeed.Game); // Game speed for better shake detection 20ms
                Accelerometer.ShakeDetected += Accelerometer_ShakeDetected; 

            } catch (FeatureNotSupportedException) {
                StopAccelerometer();
            }
        }


        private void StopAccelerometer() {
            try {
                if (Accelerometer.IsMonitoring)
                    Accelerometer.Stop();
                Accelerometer.ShakeDetected -= Accelerometer_ShakeDetected;
            } catch (Exception ex) {
                // Handle exception
            }
        }
    }


    
}
