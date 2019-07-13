using p2p.Models;
using System.Threading.Tasks;

namespace p2p.Services
{
    public interface IBackendSessionManager
    {
        /// <summary>
        /// Gets or sets username used to authenticate and obtain session from backend service.
        /// </summary>
        string Username { get; set; }

        /// <summary>
        /// Gets or sets password used to authenticate and obtain session from backend service.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets session token.
        /// </summary>
        /// <remarks>SessionToken.Token may change after session is refreshed, but Session reference should remain.</remarks>
        SessionToken Session { get; set; }
        /// <summary>
        /// Gets or sets the backend proxy instance.
        /// </summary>
        IBackendProxy BackendProxy { get; set; }
        //Test Testik { get; set; }
        Task Refresh();
    }
}
