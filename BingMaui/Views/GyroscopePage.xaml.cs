using Microsoft.Maui.Controls;
using System;

namespace BingMaui.Views {
    public partial class GyroscopePage : ContentPage {
        double squareX = 0;
        double squareY = 0;

        public GyroscopePage() {
            InitializeComponent();
        }

        // Gyroscope reading event handler
        void Gyroscope_ReadingChanged(object sender, GyroscopeChangedEventArgs e) {
            var data = e.Reading;

            double xVelocity = data.AngularVelocity.X;
            double yVelocity = data.AngularVelocity.Y;
            double zVelocity = data.AngularVelocity.Z;

            double deltaX = xVelocity * 50; 
            double deltaY = yVelocity * 50; 

            // Update the position of the square
            squareX += deltaX;
            squareY += deltaY;

            // Ensure the square stays within bounds of the screen
            squareX = Math.Clamp(squareX, 0, this.Height - rotatingSquare.Height);
            squareY = Math.Clamp(squareY, 0, this.Width - rotatingSquare.Width);

            // Move the square using AbsoluteLayout
            AbsoluteLayout.SetLayoutBounds(rotatingSquare, new Rect(squareY, squareX, 150, 150));

            // Apply Y-axis angular velocity to rotation (convert radians to degrees)
            double rotationAngle = zVelocity * 180 / Math.PI; 
            rotatingSquare.Rotation = rotationAngle;

            gyrolabel.Text = $"x: {xVelocity}\ny: {yVelocity}\nz: {zVelocity}";
        }

        void StartGyroscope() {
            try {
                if (Gyroscope.Default.IsSupported) {
                    Gyroscope.Default.ReadingChanged += Gyroscope_ReadingChanged;
                    Gyroscope.Default.Start(SensorSpeed.Game);
                } else {
                    DisplayAlert("Error", "Gyroscope not supported on this device", "OK");
                }
            } catch (FeatureNotSupportedException) {}
        }

        void StopGyroscope() {
            try {
                if (Gyroscope.Default.IsSupported && Gyroscope.Default.IsMonitoring) {
                    Gyroscope.Default.Stop();
                    Gyroscope.Default.ReadingChanged -= Gyroscope_ReadingChanged;
                }
            } catch (Exception) {
                throw;
            }
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            StartGyroscope();
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
            StopGyroscope();
        }
    }
}
