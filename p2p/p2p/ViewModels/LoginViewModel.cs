﻿using p2p.Models;
using p2p.Services;
using p2p.Views;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using p2p.Helpers;
using Xamarin.Essentials;

namespace p2p.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string email;
        

        private IBackendProxy _backendProxy;
        private IBackendSessionManager _backendSessionManager;
        private IEncryptionHelper _encryptionHelper;
       
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
        public LoginViewModel(IBackendProxy backendProxy, IBackendSessionManager backendSessionManager, IEncryptionHelper encryptionHelper)
        {
            _backendProxy = backendProxy;
            _backendSessionManager = backendSessionManager;
            _encryptionHelper = encryptionHelper;
           
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
             


                try
                {
                    await SecureStorage.SetAsync("username", Email);
                    await SecureStorage.SetAsync("password", _encryptionHelper.Encrypt(Password));
                }
                catch (Exception ex)
                {
                    // Possible that device doesn't support secure storage on device.
                }


                //var seconds = TimeSpan.FromSeconds(25);
                _backendSessionManager.Username = Email;
                _backendSessionManager.Session = session;
                //_backendSessionManager.Testik = testik;

                MessagingCenter.Send<LoginViewModel, string[]>(this,"logged", new string[] {session.RefreshToken, Email});

                /*
                                Device.StartTimer(seconds, () => {

                                    Task.Run(async () =>
                                    {
                                        refSession = await _backendProxy.RefreshTokenAsync(refSession.RefreshToken, refSession.Username);

                                        if (!string.IsNullOrEmpty(refSession.AccessToken))
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
                */

                try
                {
                    if(session.Role == "Customer")
                        await App.Current.MainPage.Navigation.PushModalAsync(new CustomerTabbedPage());
                    else
                        await App.Current.MainPage.Navigation.PushModalAsync(new ProfiTabbedPage());
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
