using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace FiscalFacil.ViewModels
{
    public class BarcodePageViewModel : ViewModelBase
    {
        private bool cameraFlashLightOn = false;
        private bool _isAnalyzing = true;
        private bool _isScanning = true;
        private bool _flashButtonVisible;
        private string _topText = "Text";
        private string _bottomText = "Text";

        ZXingScannerView _zxing = new ZXingScannerView();

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
                // Stop analysis until we navigate away so we don't keep reading barcodes
                IsAnalyzing = false;

                Console.WriteLine(Result.Text);
                // do something with Result.Text


                await NavigationService.GoBackAsync();
            });
        });

        public BarcodePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            ShowFlashButton = true;
        }
    }
}
