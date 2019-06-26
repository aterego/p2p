using p2p.Models;

namespace p2p.Services
{
    public interface IBackendSessionManager
    {
        /// <summary>
        /// Gets or sets session token.
        /// </summary>
        /// <remarks>SessionToken.Token may change after session is refreshed, but Session reference should remain.</remarks>
        SessionToken Session { get; set; }
        Test Testik { get; set; }
    }
}
