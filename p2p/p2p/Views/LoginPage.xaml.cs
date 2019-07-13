using p2p.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using p2p.Services;

namespace p2p.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public event EventHandler<SessionEventArgs> OnStartRefresh;

        public LoginPage()
        {
            Title = "P2P";
            var vm = App.Container.GetService<LoginViewModel>();
            //var vm = new LoginViewModel();
            this.BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Login, try again", "OK");

            InitializeComponent();

            MessagingCenter.Subscribe<LoginViewModel, string[]>(this, "logged", (ev, arg) =>
            {
                OnLog(arg);
            });

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


        private void OnLog(string[] args)
        {
            OnStartRefresh?.Invoke(null, new SessionEventArgs {BackendSessionManager =  App.Container.GetService<IBackendSessionManager>() });
        }
    }

    public class SessionEventArgs : EventArgs
    {
        //public string SessionToken { get; set; }
       // public string Username { get; set; }
        public IBackendSessionManager BackendSessionManager { get; set; }
        
    }
}