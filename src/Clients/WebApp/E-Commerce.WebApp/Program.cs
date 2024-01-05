using Blazored.LocalStorage;
using E_Commerce.WebApp;
using E_Commerce.WebApp.Infrastructure.Authentication;
using E_Commerce.WebApp.Infrastructure.Services;
using E_CommerceApp.WebApp.Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("WebApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:5001");
}).AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddScoped(sp =>
{
    var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
    return clientFactory.CreateClient("WebApiClient");
});
builder.Services.AddScoped<AuthTokenHandler>();
builder.Services.AddTransient<IIdentityService, IdentityService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
