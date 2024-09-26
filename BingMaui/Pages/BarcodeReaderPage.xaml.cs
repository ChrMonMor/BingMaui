using ZXing.Net.Maui;

namespace BingMaui.Pages;

public partial class BarcodeReaderPage : ContentPage
{
    public BarcodeReaderPage()
    {
        InitializeComponent();
        barcodeReader.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormat.QrCode,
            AutoRotate = true,
            Multiple = false,
            TryHarder = true,
            TryInverted = true,
        };

    }

    private void barcodeReader_BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {

        var bar = e.Results?.FirstOrDefault();

        if (bar is null)
        {
            return;
        }

        Dispatcher.DispatchAsync(async () =>
        {
            await DisplayAlert("Bar Deteced", bar.Value, "Ok");
        });
    }
}