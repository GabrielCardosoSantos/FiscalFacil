using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Prism;
using Prism.Ioc;
using SuaveControls.FloatingActionButton.Droid.Renderers;
using ZXing.Mobile;

namespace FiscalFacil.Droid
{
    [Activity(Label = "FiscalFacil", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            Window.SetStatusBarColor(Color.Black);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            global::ZXing.Net.Mobile.Forms.Android.Platform.Init();
            MobileBarcodeScanner.Initialize(this.Application); //  <<-- Adicione essa linha. 
            FloatingActionButtonRenderer.Initialize();
            LoadApplication(new App(new AndroidInitializer()));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

