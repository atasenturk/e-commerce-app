using E_CommerceApp.Common.Models.Queries.User;
using E_CommerceApp.Common.Models.RequestModels.User;

namespace Infrastructure.Services.Interfaces;

public interface IUserService
{
    Task<bool> RegisterUser(CreateUserCommand createUserCommand);
    Task<GetUserShoppingCartDetailViewModel> GetUserCart(Guid userId);
}