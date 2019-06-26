using p2p.Models;
using p2p.Services;
using p2p.Views;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
namespace p2p.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string email;
        

        private IBackendProxy _backendProxy;
        private IBackendSessionManager _backendSessionManager;
       
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        public ICommand SubmitCommand { protected set; get; }
        public ICommand RegisterCommand { protected set; get; }
        public LoginViewModel(IBackendProxy backendProxy, IBackendSessionManager backendSessionManager)
        {
            _backendProxy = backendProxy;
            _backendSessionManager = backendSessionManager;
           
            SubmitCommand = new Command(OnSubmit);
            RegisterCommand = new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new RegistrationView())); //await App.Current.MainPage.Navigation.PushModalAsync(new RegistrationView()));

        }
        public async void OnSubmit()
        {
            SessionToken session =  await _backendProxy.LoginAsync(Email, Password);
            if (!string.IsNullOrEmpty(session.AccessToken))
            {
                SessionToken refSession = session;
                Test testik = new Test();
                testik.Clock = 0;

                var seconds = TimeSpan.FromSeconds(25);
                _backendSessionManager.Session = session;
                _backendSessionManager.Testik = testik;
                

                Device.StartTimer(seconds, () => {

                    Task.Run(async () =>
                    {
                        refSession = await _backendProxy.RefreshTokenAsync(refSession.RefreshToken, refSession.Username);

                        if (!string.IsNullOrEmpty(session.AccessToken))
                        {
                            _backendSessionManager.Session = refSession;
                            Debug.WriteLine("good");
                            DateTime dateTime = DateTime.Now;
                            if(refSession.Role == "Customer")
                                testik.Clock = dateTime.Second;
                            else
                                testik.Clock = dateTime.Millisecond;
                            _backendSessionManager.Testik = testik;
                        }
                        else
                        {
                            Debug.WriteLine("bad");
                        }

                    });

                    // call your method to check for notifications here

                    // Returning true means you want to repeat this timer
                    return true;
                });

                
                try
                {
                    if(session.Role == "Customer")
                        await App.Current.MainPage.Navigation.PushModalAsync(new CustomerView());
                    else
                        await App.Current.MainPage.Navigation.PushModalAsync(new ProfiView());
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Open book error: " + ex.Message);
                    // DisplayAlert("Caution", "Please try open the book again.", "ok");
                }

            }
            else
            {
                DisplayInvalidLoginPrompt();
            }

        }
    }
}
