using Prism;
using Prism.Ioc;
using FiscalFacil.ViewModels;
using FiscalFacil.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FiscalFacil.Database;
using System.IO;
using System;
using SQLite;
using FiscalFacil.Services;
using FiscalFacil.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FiscalFacil
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */

        private static SQLiteAsyncConnection Connection;
        private static IDatabase<NotaFiscal> notaRepo;
        private static IDatabase<Produto> produtoRepo;
        private static IDatabase<Local> localRepo;
        private static IDatabase<Preco> precoRepo;


        public static ReceitaConsumer ConsultaAPI;


        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            ConsultaAPI = new ReceitaConsumer();
            Connection = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PhotoSQLite.db3"));
            Connection.CreateTablesAsync<NotaFiscal, Produto, Local, Preco>().Wait();
            await NavigationService.NavigateAsync("MasterPage/HomePage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<ProdutoPage, ProdutoPageViewModel>();
            containerRegistry.RegisterForNavigation<MasterPage, MasterPageViewModel>();
            containerRegistry.RegisterForNavigation<BarcodePage, BarcodePageViewModel>();
        }

        public static IDatabase<NotaFiscal> NotaDatabase
        {
            get
            {
                if (notaRepo == null)
                {
                    notaRepo = new Database<NotaFiscal>(Connection);
                }
                return notaRepo;
            }
        }

        public static IDatabase<Produto> ProdutoDatabase
        {
            get
            {
                if (produtoRepo == null)
                {
                    produtoRepo = new Database<Produto>(Connection);
                }
                return produtoRepo;
            }
        }

        public static IDatabase<Local> LocalDatabase
        {
            get
            {
                if (localRepo == null)
                {
                    localRepo = new Database<Local>(Connection);
                }
                return localRepo;
            }
        }

        public static IDatabase<Preco> PrecoDatabase
        {
            get
            {
                if (precoRepo == null)
                {
                    precoRepo = new Database<Preco>(Connection);
                }
                return precoRepo;
            }
        }
    }
}
