using Ninject;
using p2p.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace p2p
{
    public partial class App : Application
    {
        public static IKernel Container { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage()); //new p2p.Views.LoginPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
