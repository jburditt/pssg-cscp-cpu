using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http.Headers;

namespace Gov.Cscp.Victims.Public.Services
{
    public class KeycloakHandler : DelegatingHandler
    {
        private readonly IKeycloakAuthService _keycloakAuthService;

        public KeycloakHandler(
            IKeycloakAuthService keycloakAuthService)
        {
            _keycloakAuthService = keycloakAuthService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await _keycloakAuthService.GetToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
