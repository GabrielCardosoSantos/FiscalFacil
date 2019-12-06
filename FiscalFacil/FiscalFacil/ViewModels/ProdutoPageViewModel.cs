using FiscalFacil.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FiscalFacil.ViewModels
{
    public class ProdutoPageViewModel : ViewModelBase
    {
        public NotaFiscalModel NotaFiscal { get; set; }

        private ObservableCollection<ProdutoModel> _produtos;
        public ObservableCollection<ProdutoModel> Produtos { get { return _produtos; } set { SetProperty(ref _produtos, value); } }

        private string _dataEmissao;
        public string DataEmissao { get { return _dataEmissao; } set { SetProperty(ref _dataEmissao, value); } }

        private string _nomeMercado;
        public string NomeMercado { get { return _nomeMercado; } set { SetProperty(ref _nomeMercado, value); } }

        public DelegateCommand CancelarCommand { get; set; }
        public DelegateCommand SalvarCommand { get; set; }

        private IPageDialogService PageDialog { get; set; }

        public ProdutoPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService)
        {
            PageDialog = pageDialogService;
            Produtos = new ObservableCollection<ProdutoModel>();
            CancelarCommand = new DelegateCommand(OpenCancelar);
            SalvarCommand = new DelegateCommand(OpenSalvar);
        }

        private async void OpenSalvar()
        {
            if(await PageDialog.DisplayAlertAsync("Deseja salvar a compra?", NomeMercado + Environment.NewLine + Environment.NewLine + DataEmissao, "Salvar", "Cancelar"))
            {
                try
                {       
                    var all = await App.LocalDatabase.Get();
                    var x = all.Find(n => n.Nome == NotaFiscal.GetNomeLocal() && n.Endereco == NotaFiscal.GetEndereco());

                    if(x != null)
                        NotaFiscal.Nota.IdLocal = x.Id;
                    else
                    {
                        NotaFiscal.Nota.IdLocal = NotaFiscal.Local.Id;
                        await App.LocalDatabase.Insert(NotaFiscal.Local);
                    }

                    await App.NotaDatabase.Insert(NotaFiscal.Nota);

                    foreach (ProdutoModel p in NotaFiscal.Produtos)
                    {
                        p.Produto.IdLocal = NotaFiscal.Local.Id;
                        p.Produto.IdNota = NotaFiscal.Nota.Id;

                        var prodRepetido = await App.ProdutoDatabase.Get();

                        var prodfind = prodRepetido.Find(n => n.Descricao == p.Produto.Descricao);

                        if (prodfind != null)
                        {
                            await App.ProdutoDatabase.Insert(p.Produto);
                            p.Preco.IdProduto = prodfind.Id;
                        }
                        
                        await App.PrecoDatabase.Insert(p.Preco);

                    }

                    await PageDialog.DisplayAlertAsync("Salvo!", "Nota salva com sucesso.", "Ok");
                }
                catch (Exception e)
                {
                    await PageDialog.DisplayAlertAsync("Erro!", e.Message, "Ok");
                }

            }
        }

        private async void OpenCancelar()
        {
            await NavigationService.GoBackAsync();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if(parameters.ContainsKey("nota"))
            {
                NotaFiscal = parameters["nota"] as NotaFiscalModel;
                NotaFiscal.GetProdutos().ForEach(n => Produtos.Add(n));
                DataEmissao = NotaFiscal.GetDataEmissao();
                NomeMercado = NotaFiscal.GetNomeLocal();
            }
        }
    }
}
