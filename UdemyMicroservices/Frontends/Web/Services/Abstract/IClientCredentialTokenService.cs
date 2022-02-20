using System.Threading.Tasks;

namespace Web.Services.Abstract
{
    public interface IClientCredentialTokenService
    {
        Task<string> GetToken();
    }
}
