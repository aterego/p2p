using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace p2p.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationView : ContentPage
    {
        public RegistrationView()
        {
            Title = "P2P - Registration";
            InitializeComponent();
        }

        private async void CustomerRegisterButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CustomerRegistrationView());
        }

        private async void ProfiRegisterButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfiRegistrationView());
        }


    }
}