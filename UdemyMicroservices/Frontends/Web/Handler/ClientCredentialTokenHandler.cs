using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Web.Exceptions;
using Web.Services.Abstract;

namespace Web.Handler
{
    public class ClientCredentialTokenHandler : DelegatingHandler
    {
        private readonly IClientCredentialTokenService _clientCredentialTokenService;

        public ClientCredentialTokenHandler(IClientCredentialTokenService clientCredentialTokenService)
        {
            _clientCredentialTokenService = clientCredentialTokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            var accessToken = await _clientCredentialTokenService.GetToken();

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnAuthorizeException();
            }

            return response;

        }
    }
}
