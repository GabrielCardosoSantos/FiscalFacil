using FiscalFacil.Services;
using FiscalFacil.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace FiscalFacil.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public DelegateCommand QRCodeCommand { get; set; }
        ZXingScannerPage scanPage;
        public HomePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            QRCodeCommand = new DelegateCommand(OpenNota);
        }

        private async void OpenQRCode()
        {
            await NavigationService.NavigateAsync("BarcodePage");

            
            //var customOverlay = new StackLayout
            //{
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    VerticalOptions = LayoutOptions.FillAndExpand
            //};
            //var torch = new Button
            //{
            //    Text = "Toggle Torch"
            //};
            //torch.Clicked += delegate {
            //    scanPage.ToggleTorch();
            //};
            //customOverlay.Children.Add(torch);

            //scanPage = new ZXingScannerPage(customOverlay: customOverlay);
            //scanPage.OnScanResult += (result) => {
            //    scanPage.IsScanning = false;

            //    Device.BeginInvokeOnMainThread(() =>
            //    {
            //        NavigationService.GoBackAsync();
            //        Console.WriteLine(result.Text);
            //        //DisplayAlert("Scanned Barcode", result.Text, "OK");
            //    });
            //};

            //await Navigation.PushAsync(scanPage);
        }

        void ZXingView_BarcodeReaded(object sender, string e)
        {
            Console.WriteLine(e);
        }

        private void OpenNota()
        {
            OpenQRCode();
            //await NavigationService.NavigateAsync("");
        }
    }
}
