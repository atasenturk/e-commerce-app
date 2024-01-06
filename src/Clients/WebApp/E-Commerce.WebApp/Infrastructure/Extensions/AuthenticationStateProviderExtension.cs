using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace E_Commerce.WebApp.Infrastructure.Extensions
{
    public static class AuthenticationStateProviderExtension
    {
        public static async Task<Guid> GetUserId(this AuthenticationStateProvider provider)
        {
            var state = await provider.GetAuthenticationStateAsync();
            var userId = state.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return new Guid(userId);
        }

        public static async Task<string> GetUsername(this AuthenticationStateProvider provider)
        {
            var state = await provider.GetAuthenticationStateAsync();
            var username = state.User.FindFirst(ClaimTypes.Name).Value;

            return username;
        }

        public static async Task<string> GetGivenName(this AuthenticationStateProvider provider)
        {
            var state = await provider.GetAuthenticationStateAsync();
            var givenName = state.User.FindFirst(ClaimTypes.GivenName).Value;

            return givenName;
        }
    }
}
