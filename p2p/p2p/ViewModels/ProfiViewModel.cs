using p2p.Models;
using p2p.Services;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;



namespace p2p.ViewModels
{
    public class ProfiViewModel : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string _content;

        private IBackendProxy _backendProxy;
        private IBackendSessionManager _backendSessionManager;
        
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Content"));
            }
        }

        public ICommand SubmitCommand { protected set; get; }
        public ProfiViewModel(IBackendProxy backendProxy, IBackendSessionManager backendSessionManager)
        {
            _backendProxy = backendProxy;
            _backendSessionManager = backendSessionManager;
            
            SubmitCommand = new Command(OnSubmit);
        }


        public async void OnSubmit()
        {
            //Test test = (Test) App.Container.GetService(typeof(Test));
          

            Content =  await _backendProxy.GetContentProfiAsync(_backendSessionManager.Session.AccessToken, _backendSessionManager.Testik);

        }
    }
}
