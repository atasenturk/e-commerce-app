using Blazored.LocalStorage;

namespace E_Commerce.WebApp.Infrastructure.Extensions
{
    public static class LocalStorageExtension
    {
        public const string TokenName = "token";
        public const string UserName = "username";
        public const string UserId = "userId";
        public static bool IsUserLoggedIn(this ISyncLocalStorageService localStorageService)
        {
            return !string.IsNullOrEmpty(GetToken(localStorageService));
        }
        public static string GetUserName(this ISyncLocalStorageService localStorageService)
        {
            return localStorageService.GetItem<string>(UserName);
        }
        public static async Task<string> GetUserName(this ILocalStorageService localStorageService)
        {
            return await localStorageService.GetItemAsync<string>(UserName);
        }
        public static void SetUsername(this ISyncLocalStorageService localStorageService, string username)
        {
            localStorageService.SetItem(UserName, username);
        }
        public static async Task SetUsername(this ILocalStorageService localStorageService, string username)
        {
            await localStorageService.SetItemAsync(UserName, username);;
        }
        public static Guid GetUserId(this ISyncLocalStorageService localStorageService)
        {
            return localStorageService.GetItem<Guid>(UserId);
        }
        public static async Task<Guid> GetUserId(this ILocalStorageService localStorageService)
        {
            return await localStorageService.GetItemAsync<Guid>(UserId);
        }
        public static void SetUserId(this ISyncLocalStorageService localStorageService, Guid userId)
        {
            localStorageService.SetItem(UserId, userId);
        }
        public static async Task SetUserId(this ILocalStorageService localStorageService, Guid userId)
        {
            await localStorageService.SetItemAsync(UserId, userId); ;
        }

        public static string GetToken(this ISyncLocalStorageService localStorageService)
        {
            var token = localStorageService.GetItem<string>(TokenName);
            //TODO
            if (string.IsNullOrEmpty(token))
            {
                token =
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
            }
            return token;
        }
        public static async Task<string> GetToken(this ILocalStorageService localStorageService)
        {
            //TODO
            var token = await localStorageService.GetItemAsync<string>(TokenName);

            if (string.IsNullOrEmpty(token))
            {
                token =
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
            }
            return token;
        }
        public static void SetToken(this ISyncLocalStorageService localStorageService, string token)
        {
            localStorageService.SetItem(TokenName, token);
        }
        public static async Task SetToken(this ILocalStorageService localStorageService, string token)
        {
            await localStorageService.SetItemAsync(TokenName, token);
        }
    }
}
