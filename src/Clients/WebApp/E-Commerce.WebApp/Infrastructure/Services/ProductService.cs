using Blazored.LocalStorage;
using E_Commerce.WebApp.Infrastructure.Extensions;
using E_CommerceApp.Common.Infrastructure.Exceptions;
using E_CommerceApp.Common.Models.Queries;
using E_CommerceApp.Common.Models.Queries.User;
using E_CommerceApp.Common.Models.RequestModels.User;
using System.Net.Http.Headers;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using E_CommerceApp.Common.Models.Queries.Product;
using Infrastructure.Services.Interfaces;
using System.Text.Json.Serialization;

namespace E_CommerceApp.WebApp.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProductItemListViewModel>> GetProducts()
    {
        var response = await _httpClient.GetFromJsonAsync<List<ProductItemListViewModel>>("/api/product/getproducts");
        return response;
    }

}
