using p2p.Helpers;
using p2p.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace p2p.Services
{
    public class BackendSessionManager : IBackendSessionManager
    {

        private IEncryptionHelper _encryptionHelper;

        /// <summary>
        /// Gets or sets username used to authenticate and obtain session from backend service.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets password used to authenticate and obtain session from backend service.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets session token.
        /// </summary>
        /// <remarks>SessionToken.Token may change after session is refreshed, but Session reference should remain.</remarks>
        public SessionToken Session { get; set; }


        /// <summary>
        /// Gets or sets the backend proxy instance.
        /// </summary>
        public IBackendProxy BackendProxy { get; set; }

        public Test Testik { get; set; }


        public BackendSessionManager(IBackendProxy proxy, IEncryptionHelper encryptionHelper)
        {
            BackendProxy = proxy;
            Username = null as string;
            Password = null as string;
            _encryptionHelper = encryptionHelper;
        }


        public Task Refresh()
        {
            return Task.Run(async() =>
            {
                try
                {
                    var refSession =  await BackendProxy.RefreshTokenAsync(Session.RefreshToken, Username);
                    if (!string.IsNullOrEmpty(refSession.AccessToken))
                    {
                        Session = refSession;
                    }
                    else
                    {
                        var newSession = await BackendProxy.LoginAsync(Username, _encryptionHelper.Decrypt(await SecureStorage.GetAsync("password")));
                        if (!string.IsNullOrEmpty(newSession.AccessToken))
                        {
                            Session = newSession;
                        }
                        else
                        {
                            Session = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    Session = null;
                }
            });
        }

    }
}
