using System;
using System.Threading.Tasks;

public class AuthResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }

    public static AuthResult Ok(string userId, string userName, string message = "")
    {
        return new AuthResult { Success = true, UserId = userId, UserName = userName, Message = message };
    }

    public static AuthResult Fail(string message)
    {
        return new AuthResult { Success = false, Message = message };
    }
}

public interface IAuthService
{
    Task<AuthResult> LoginAsync(string email, string password);
    Task<AuthResult> RegisterAsync(string name, string email, string password);
    Task<AuthResult> GuestLoginAsync();
    bool IsLoggedIn { get; }
    string CurrentUserId { get; }
    string CurrentUserName { get; }
}

public interface IDataService
{
    Task<bool> SaveDataAsync(string userId);
    Task<bool> LoadDataAsync(string userId);
    bool HasSavedCharacter(string userId);
}
