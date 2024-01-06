using E_CommerceApp.Common.Models.Queries.Product;
using System.Net;
using System.Net.Http.Json;
using E_CommerceApp.Common.Models.RequestModels.User;
using System.Text.Json.Serialization;
using System.Text;
using E_CommerceApp.Api.Application.Features.Queries.User;
using E_CommerceApp.Common.Enums;
using Infrastructure.Services.Interfaces;
using E_CommerceApp.Common.Models.Queries.User;

namespace E_Commerce.WebApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> RegisterUser(CreateUserCommand createUserCommand)
        {
            var response = await _httpClient.PostAsJsonAsync("api/user/createuser", createUserCommand);

            return response.IsSuccessStatusCode;
        }

        public async Task<GetUserShoppingCartDetailViewModel> GetUserCart(Guid userId)
        {
            var response = await _httpClient.PostAsJsonAsync("api/user/getcart", new GetUserShoppingCartDetailQuery
            {
                UserId = userId
            });
            if (!response.IsSuccessStatusCode)
            {
                return new GetUserShoppingCartDetailViewModel();
            }

            var cartItems = await response.Content.ReadFromJsonAsync<GetUserShoppingCartDetailViewModel>();
            return cartItems;
        }


    }
}
