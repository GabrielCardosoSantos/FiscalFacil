using FiscalFacil.Services;
using FiscalFacil.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;

namespace FiscalFacil.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private bool _busy;
        public bool IsBusy { get { return _busy; } set { SetProperty(ref _busy, value); } }

        private bool _isCollection;
        public bool CollectionVisible { get { return _isCollection; } set { SetProperty(ref _isCollection, value); } }

        public IPageDialogService PageDialogService { get; set; }
        public DelegateCommand QRCodeCommand { get; set; }

        private ObservableCollection<NotaFiscalModel> _notas;
        public ObservableCollection<NotaFiscalModel> Notas { get { return _notas; } set { SetProperty(ref _notas, value); } }

        public HomePageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService)
        {
            PageDialogService = pageDialogService;
            Notas = new ObservableCollection<NotaFiscalModel>();
            QRCodeCommand = new DelegateCommand(OpenNota);
        }

    

        private async void OpenQRCode()
        {
            NotaFiscalModel nota = await App.ConsultaAPI.SearchItens("https://www.sefaz.rs.gov.br/NFCE/NFCE-COM.aspx?p=43191187397865002165652070001264771004360527|2|1|1|3B9666B4B19EDA74307BAE1F059795CA425F036A");
            NavigationParameters pairs = new NavigationParameters
            {
                { "nota", nota }
            };
            //await NavigationService.NavigateAsync("BarcodePage");
            await NavigationService.NavigateAsync("ProdutoPage", pairs);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var notas = App.ProdutoDatabase.Get();
            if (parameters.ContainsKey("url"))
            {
                IsBusy = true;
                string url = parameters["url"].ToString();
                NotaFiscalModel nota = await App.ConsultaAPI.SearchItens(url);
                
                if(nota != null)
                    Notas.Add(nota);
                else
                    await PageDialogService.DisplayAlertAsync("QRCode", "Não foi possivel abrir QRCode.", "Ok");

                IsBusy = false;
                //await PageDialogService.DisplayAlertAsync("URL", parameters["url"].ToString(), "Cancel");
            }
            else if (parameters.ContainsKey("nota"))
            {
                
            }
        }

        private void OpenNota()
        {
            OpenQRCode();
            //await NavigationService.NavigateAsync("");
        }
    }
}
