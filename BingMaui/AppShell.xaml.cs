using BingMaui.Pages;

namespace BingMaui {
    public partial class AppShell : Shell {
        public AppShell() {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ShakeDetectorPage), typeof(ShakeDetectorPage));
        }
    }
}
