using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;

namespace FiscalFacil.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]

    public class SplashActivity : Activity
    {
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { Startup(); });
            startupWork.Start();
        }

        private void Startup()
        {
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}