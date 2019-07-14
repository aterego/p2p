using p2p.Communication;
using p2p.Models;
using System.Threading.Tasks;

namespace p2p.Services
{
    public interface IBackendProxy
    {
        /// <summary>
        /// Authenticate on backend service.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        Task<SessionToken> LoginAsync(string username, string password);
        /// <summary>
        /// Refresh Token.
        /// </summary>
        /// <param name="refreshToken">RefreshToken.</param>
        /// <param name="username">Username.</param>
        Task<SessionToken> RefreshTokenAsync(string refreshToken, string username);

        Task<string> GetContentCustomerAsync(string accessToken);
        Task<string> GetContentProfiAsync(string accessToken);
        /// <summary>
        /// Register Customer.
        /// </summary>
        /// <param name="arr">string array.</param>
        Task<RegistrationResponse> RegisterCustomerAsync(string[] arr);
        /// <summary>
        /// Register Profi.
        /// </summary>
        /// <param name="arr">string array.</param>
        Task<RegistrationResponse> RegisterProfiAsync(string[] arr);
        /// <summary>
        /// Get all Categories with Prices.
        /// </summary>
        /// <param name="accessToken">accessToken.</param>
        Task<CategoriesData> GetCategoriesAsync(string accessToken);


    }
}
