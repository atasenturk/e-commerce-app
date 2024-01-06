using E_CommerceApp.Common.Models.RequestModels.User;

namespace Infrastructure.Services.Interfaces;

public interface IShoppingCartService
{
    Task<bool> AddItemToCart(AddOrDeleteItemShoppingCartCommand addOrDeleteItemShoppingCartCommand);
}