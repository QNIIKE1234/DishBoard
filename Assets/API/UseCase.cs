using System;
using System.Threading.Tasks;
using UnityEngine;

public class UseCase
{
    private static IAuthService _authService = new MockLocalAuthService();
    private static IDataService _dataService = (IDataService)_authService;

    public static IAuthService AuthService => _authService;
    public static IDataService DataService => _dataService;

    /// <summary>
    /// Switch out the backend service provider (e.g. from Mock to Supabase, PlayFab, REST API, etc.)
    /// </summary>
    public static void SetServices(IAuthService auth, IDataService data)
    {
        _authService = auth ?? throw new ArgumentNullException(nameof(auth));
        _dataService = data ?? throw new ArgumentNullException(nameof(data));
    }

    public async Task<bool> Login(string email, string password)
    {
        var result = await _authService.LoginAsync(email, password);
        return result.Success;
    }

    public async Task<bool> GuestLogin()
    {
        var result = await _authService.GuestLoginAsync();
        return result.Success;
    }

    public async Task<bool> Register(string name, string email, string password)
    {
        var result = await _authService.RegisterAsync(name, email, password);
        return result.Success;
    }

    public async Task<bool> SaveData(string userId)
    {
        return await _dataService.SaveDataAsync(userId);
    }

    public async Task<bool> LoadData(string userId)
    {
        return await _dataService.LoadDataAsync(userId);
    }

    // --- Backward compatibility aliases for existing codebase ---
    public async Task<bool> SendDataToFirestore(string userId)
    {
        return await SaveData(userId);
    }

    public async Task<bool> LoadDataToFirestore(string userId)
    {
        return await LoadData(userId);
    }
}
