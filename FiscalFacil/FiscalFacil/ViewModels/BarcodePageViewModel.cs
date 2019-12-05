using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace FiscalFacil.ViewModels
{
    public class BarcodePageViewModel : ViewModelBase
    {
        //private bool cameraFlashLightOn = false;
        private bool _isAnalyzing = true;
        private bool _isScanning = true;
        private bool _flashButtonVisible;
        private string _topText = "Text";
        private string _bottomText = "Text";

        public string TopText
        {
            get { return _topText; }
            set { SetProperty(ref _topText, value); }
        }

        public string BottomText
        {
            get { return _bottomText; }
            set { SetProperty(ref _bottomText, value); }
        }

        public bool ShowFlashButton
        {
            get { return _flashButtonVisible; }
            set
            {
                if (!bool.Equals(_flashButtonVisible, value))
                {
                    this._flashButtonVisible = value;
                    SetProperty(ref _flashButtonVisible, value);
                }
            }
        }

        public ZXing.Result Result { get; set; }

        public bool IsAnalyzing
        {
            get { return this._isAnalyzing; }
            set
            {
                if (!bool.Equals(_isAnalyzing, value))
                {
                    _isAnalyzing = value;
                    SetProperty(ref _isAnalyzing, value);
                }
            }
        }

        public bool IsScanning
        {
            get { return _isScanning; }
            set
            {
                if (!bool.Equals(_isScanning, value))
                {
                    this._isScanning = value;
                    SetProperty(ref _isScanning, value);
                }
            }
        }

        public DelegateCommand QRScanResultCommand => new DelegateCommand(() =>
        {
            IsAnalyzing = false;
            IsScanning = false;

            Device.BeginInvokeOnMainThread(async () =>
            {
                IsAnalyzing = false;

                NavigationParameters np = new NavigationParameters();
                np.Add("url", Result.Text);
                
                await NavigationService.GoBackAsync(np);
            });
        });

        public BarcodePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            ShowFlashButton = true;
        }
    }
}
