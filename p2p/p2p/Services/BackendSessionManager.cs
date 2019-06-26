using p2p.Models;

namespace p2p.Services
{
    public class BackendSessionManager : IBackendSessionManager
    {
        /// <summary>
        /// Gets or sets session token.
        /// </summary>
        /// <remarks>SessionToken.Token may change after session is refreshed, but Session reference should remain.</remarks>
        public SessionToken Session { get; set; }

        public Test Testik { get; set; }
    }
}
