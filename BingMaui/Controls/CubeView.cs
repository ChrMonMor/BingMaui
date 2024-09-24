using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using System;
using Xamarin.Essentials;
using Gyroscope = Microsoft.Maui.Devices.Sensors.Gyroscope;
using GyroscopeChangedEventArgs = Microsoft.Maui.Devices.Sensors.GyroscopeChangedEventArgs;
using SensorSpeed = Microsoft.Maui.Devices.Sensors.SensorSpeed;

namespace BingMaui.Controls {
    public class CubeView : GraphicsView {
        private float _rotationX = 0;
        private float _rotationY = 0;
        private float _rotationZ = 0;

        public CubeView() {
            // Start the gyroscope and handle the updates
            if (Gyroscope.IsMonitoring)
                Gyroscope.Stop();

            Gyroscope.ReadingChanged += Gyroscope_ReadingChanged;
            Gyroscope.Start(SensorSpeed.UI);
        }

        private void Gyroscope_ReadingChanged(object sender, GyroscopeChangedEventArgs e) {
            var data = e.Reading;

            // Update rotation angles based on gyroscope data
            _rotationX += (float)(data.AngularVelocity.X * 10);  // Scale the rotation
            _rotationY += (float)(data.AngularVelocity.Y * 10);
            _rotationZ += (float)(data.AngularVelocity.Z * 10);

            // Trigger a redraw of the view
            Invalidate();
        }

        public void Draw(ICanvas canvas, RectF dirtyRect) {
            canvas.SaveState();

            // Translate to center for rotation
            canvas.Translate(dirtyRect.Width / 2, dirtyRect.Height / 2);

            // Apply rotation transformations
            canvas.Rotate(_rotationX, _rotationY, _rotationZ);

            DrawCube(canvas, 200);  // Draw cube with size 200

            canvas.RestoreState();
        }

        private void DrawCube(ICanvas canvas, float size) {
            // Set up colors and styles
            canvas.StrokeColor = Colors.Black;
            canvas.StrokeSize = 2;

            // Define cube points in 3D space
            var halfSize = size / 2;

            var points = new[]
            {
                new Point3D(-halfSize, -halfSize, -halfSize),
                new Point3D(halfSize, -halfSize, -halfSize),
                new Point3D(halfSize, halfSize, -halfSize),
                new Point3D(-halfSize, halfSize, -halfSize),
                new Point3D(-halfSize, -halfSize, halfSize),
                new Point3D(halfSize, -halfSize, halfSize),
                new Point3D(halfSize, halfSize, halfSize),
                new Point3D(-halfSize, halfSize, halfSize)
            };

            // Define the edges of the cube
            var edges = new[]
            {
                (0, 1), (1, 2), (2, 3), (3, 0), // back face
                (4, 5), (5, 6), (6, 7), (7, 4), // front face
                (0, 4), (1, 5), (2, 6), (3, 7)  // connections between front and back
            };

            // Project 3D points to 2D and draw the cube
            foreach (var (startIdx, endIdx) in edges) {
                var start = ProjectTo2D(points[startIdx]);
                var end = ProjectTo2D(points[endIdx]);

                canvas.DrawLine(start.X, start.Y, end.X, end.Y);
            }
        }

        private PointF ProjectTo2D(Point3D point) {
            // Simple orthogonal projection (can be extended for perspective)
            return new PointF(point.X, point.Y);
        }

        public struct Point3D {
            public float X, Y, Z;
            public Point3D(float x, float y, float z) { X = x; Y = y; Z = z; }
        }
    }
}
