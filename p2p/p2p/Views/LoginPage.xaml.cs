using p2p.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace p2p.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            Title = "P2P";
            var vm = App.Container.GetService<LoginViewModel>();
            //var vm = new LoginViewModel();
            this.BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Login, try again", "OK");
            InitializeComponent();

            Email.Completed += (object sender, EventArgs e) =>
            {
                vm.Email = Email.Text;
                Password.Focus();
            };

            Password.Completed += (object sender, EventArgs e) =>
            {
                vm.Password = Password.Text;
                vm.SubmitCommand.Execute(null);
            };
        }
    }
}