

namespace BingMaui.Pages;

public partial class FlashlightPage : ContentPage
{
    private bool isToggled;
	public FlashlightPage()
	{
		InitializeComponent();
        isToggled = false;
    }
    protected override void OnAppearing() {
        base.OnAppearing();
        StartFlashlightAsync();
    }

    private async void StartFlashlightAsync() {
        try {
            if (isToggled) {
                return;
            }
            if (Flashlight.IsSupportedAsync().Result) {
                await Flashlight.Default.TurnOnAsync();
                isToggled = true;
            }
        } catch (FeatureNotSupportedException ex) {
            // Handle not supported on device exception
        } catch (PermissionException ex) {
            // Handle permission exception
        } catch (Exception ex) {
            // Unable to turn on/off flashlight
        }
    }

    protected override void OnDisappearing() {
        base.OnDisappearing();
        StopFlashlightAsync();
    }

    private async void StopFlashlightAsync() {
        try {
            if (!isToggled) {
                return;
            }
            if (Flashlight.IsSupportedAsync().Result) {
                await Flashlight.Default.TurnOffAsync();
                isToggled = false;
            }
        } catch (Exception) {
            throw;
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {
        if (isToggled) {
            StopFlashlightAsync();
        } else {
            StartFlashlightAsync();
        }
    }
}