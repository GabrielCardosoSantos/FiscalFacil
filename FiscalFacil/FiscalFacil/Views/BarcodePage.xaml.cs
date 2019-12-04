using System;
using Xamarin.Forms;

namespace FiscalFacil.Views
{
    public partial class BarcodePage : ContentPage
    {
        public BarcodePage()
        {
            InitializeComponent();
        }

        public void FlashButtonClicked(object sender, EventArgs e)
        {
            scanner.ToggleTorch();
        }
    }
}
