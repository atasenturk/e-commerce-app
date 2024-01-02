using E_CommerceApp.Common.Models.RequestModels.User;

namespace Infrastructure.Services.Interfaces;

public interface IIdentityService
{
    bool isUserLoggedIn { get; }
    string GetUserToken();
    string GetUserName();
    Guid GetUserId();
    Task<bool> Login(LoginUserCommand command);
    void Logout();
}