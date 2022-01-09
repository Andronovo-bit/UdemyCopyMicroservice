using IdentityModel.Client;
using Shared.Library.Dtos;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services.Abstract
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SigninInput signinInput);

        Task<TokenResponse> GetAccessTokenByRefreshToken();

        Task RevokeRefreshToken();
    }
}
