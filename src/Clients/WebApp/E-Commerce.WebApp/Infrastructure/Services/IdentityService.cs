using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using E_Commerce.WebApp.Infrastructure.Extensions;
using E_CommerceApp.Common.Infrastructure.Exceptions;
using E_CommerceApp.Common.Models.Queries.User;
using E_CommerceApp.Common.Models.RequestModels.User;
using Infrastructure.Services.Interfaces;

namespace E_Commerce.WebApp.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly ISyncLocalStorageService _localStorage;

        public IdentityService(HttpClient httpClient, ISyncLocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public bool isUserLoggedIn => !string.IsNullOrEmpty(GetUserToken());

        public string GetUserToken()
        {
            return _localStorage.GetToken();
        }

        public string GetUserName()
        {
            return _localStorage.GetUserName();
        }
        public Guid GetUserId()
        {
            return _localStorage.GetUserId();
        }
        public async Task<bool> Login(LoginUserCommand command)
        {
            string responseStr;
            var httpResponse = await _httpClient.PostAsJsonAsync("/api/User/Login", command);
            if (httpResponse == null && !httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode is HttpStatusCode.BadRequest)
                {
                    throw new DatabaseDuplicateException(await httpResponse.Content.ReadAsStringAsync());
                }
                return false;
            }

            responseStr = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<LoginUserViewModel>(responseStr);
            if (!string.IsNullOrEmpty(response.Token))
            {
                _localStorage.SetToken(response.Token);
                _localStorage.SetUsername(response.UserName);
                _localStorage.SetUserId(response.Id);
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("bearer", response.UserName);
                return true;
            }
            return false;
        }
        public void Logout()
        {
            _localStorage.RemoveItem(LocalStorageExtension.TokenName);
            _localStorage.RemoveItem(LocalStorageExtension.UserId);
            _localStorage.RemoveItem(LocalStorageExtension.UserName);
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
