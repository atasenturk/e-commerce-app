using System.Net.Http.Headers;
using Blazored.LocalStorage;
using E_Commerce.WebApp.Infrastructure.Extensions;

namespace E_Commerce.WebApp.Infrastructure.Authentication
{
    public class AuthTokenHandler : DelegatingHandler
    {
        private readonly ISyncLocalStorageService _syncLocalStorageService;

        public AuthTokenHandler(ISyncLocalStorageService syncLocalStorageService)
        {
            _syncLocalStorageService = syncLocalStorageService;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _syncLocalStorageService.GetToken();
            if (string.IsNullOrEmpty((token)) && request.Headers.Authorization is null)
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
