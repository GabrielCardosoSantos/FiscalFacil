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
        public IPageDialogService PageDialogService { get; set; }
        public DelegateCommand QRCodeCommand { get; set; }
        public HomePageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService)
        {
            PageDialogService = pageDialogService;
            QRCodeCommand = new DelegateCommand(OpenNota);
        }

        private async void OpenQRCode()
        {
            await NavigationService.NavigateAsync("BarcodePage");
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("url"))
            {
                await PageDialogService.DisplayAlertAsync("URL", parameters["url"].ToString(), "Cancel");
            }
        }

        private void OpenNota()
        {
            OpenQRCode();
            //await NavigationService.NavigateAsync("");
        }
    }
}
