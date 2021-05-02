using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MessengerApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Sharpnado.MaterialFrame.Initializer.Initialize(false, false);
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
