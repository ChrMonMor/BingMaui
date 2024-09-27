using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingMaui.ViewModels {
    public partial class MagnetometerViewModel : ObservableObject {

        [ObservableProperty]
        string _magnetometerLabel;
        [ObservableProperty]
        string _hasItLabel;

        public MagnetometerViewModel() {
            HasItLabel = "";
            MagnetometerLabel = "";
            ToggleMagnetometer();

        }
        private void ToggleMagnetometer() {
            if (Magnetometer.Default.IsSupported) {
                if (!Magnetometer.Default.IsMonitoring) {
                    // Turn on magnetometer
                    Magnetometer.Default.ReadingChanged += Magnetometer_ReadingChanged; ;
                    Magnetometer.Default.Start(SensorSpeed.UI);
                } else {
                    // Turn off magnetometer
                    Magnetometer.Default.Stop();
                    Magnetometer.Default.ReadingChanged -= Magnetometer_ReadingChanged;
                }
            } else {
                HasItLabel = "doesn't have it";
            }
        }

        private void Magnetometer_ReadingChanged(object sender, MagnetometerChangedEventArgs e) {
            // Update UI Label with magnetometer state
            MagnetometerLabel = $"Magnetometer: \nX: {e.Reading.MagneticField.X}\nY: {e.Reading.MagneticField.Y}\nZ: {e.Reading.MagneticField.Z}";
            var s = e.Reading.MagneticField;
        }
    }
}
