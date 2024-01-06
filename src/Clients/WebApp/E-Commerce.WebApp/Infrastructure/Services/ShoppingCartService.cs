using System.Net.Http.Json;
using E_CommerceApp.Common.Models.RequestModels.User;
using Infrastructure.Services.Interfaces;

namespace E_Commerce.WebApp.Infrastructure.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly HttpClient _httpClient;

    public ShoppingCartService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> AddItemToCart(AddOrDeleteItemShoppingCartCommand addOrDeleteItemShoppingCartCommand)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/shoppingcart/AddOrDeleteItemInCart", addOrDeleteItemShoppingCartCommand);

        return response.IsSuccessStatusCode;
    }
}