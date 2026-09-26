using System;
using System.Threading.Tasks;
using UnityEngine;

public class MockLocalAuthService : IAuthService, IDataService
{
    private string _currentUserId = "";
    private string _currentUserName = "";
    private bool _isLoggedIn = false;

    public bool IsLoggedIn => _isLoggedIn;
    public string CurrentUserId => _currentUserId;
    public string CurrentUserName => _currentUserName;

    public async Task<AuthResult> GuestLoginAsync()
    {
        await Task.Yield();

        string savedName = PlayerPrefs.GetString("SavedUserName", "Guest");
        string deviceId = "Guest_" + SystemInfo.deviceUniqueIdentifier;

        _currentUserId = deviceId;
        _currentUserName = savedName;
        _isLoggedIn = true;

        if (UserStatus.Instance != null)
        {
            UserStatus.Instance.UserId = _currentUserId;
            UserStatus.Instance.Name = _currentUserName;
            UserStatus.Instance.Email = _currentUserName + "@local";
        }

        PlayerPrefs.SetString("SavedUserId", _currentUserId);
        PlayerPrefs.SetString("SavedUserName", _currentUserName);
        PlayerPrefs.Save();

        Debug.Log($"[MockAuth] Guest login successful: {_currentUserName} ({_currentUserId})");
        return AuthResult.Ok(_currentUserId, _currentUserName, "Welcome Guest!");
    }

    public async Task<AuthResult> LoginAsync(string email, string password)
    {
        await Task.Yield();

        string username = "Guest";
        if (!string.IsNullOrEmpty(email))
        {
            username = email.Trim();
            // If email format, take user part before @
            if (username.Contains("@"))
            {
                username = username.Split('@')[0];
            }
        }
        else if (PlayerPrefs.HasKey("SavedUserName"))
        {
            username = PlayerPrefs.GetString("SavedUserName");
        }

        string deviceId = "User_" + SystemInfo.deviceUniqueIdentifier;
        _currentUserId = deviceId;
        _currentUserName = username;
        _isLoggedIn = true;

        if (UserStatus.Instance != null)
        {
            UserStatus.Instance.UserId = _currentUserId;
            UserStatus.Instance.Name = _currentUserName;
            UserStatus.Instance.Email = (email != null && email.Contains("@")) ? email : _currentUserName + "@local";
        }

        PlayerPrefs.SetString("SavedUserId", _currentUserId);
        PlayerPrefs.SetString("SavedUserName", _currentUserName);
        PlayerPrefs.Save();

        Debug.Log($"[MockAuth] Login successful: {_currentUserName}");
        return AuthResult.Ok(_currentUserId, _currentUserName, $"Welcome back, {_currentUserName}!");
    }

    public async Task<AuthResult> RegisterAsync(string name, string email, string password)
    {
        await Task.Yield();

        string username = string.IsNullOrEmpty(name) ? "Hero" : name.Trim();
        string userEmail = string.IsNullOrEmpty(email) ? username + "@local" : email.Trim();
        string deviceId = "User_" + SystemInfo.deviceUniqueIdentifier;

        _currentUserId = deviceId;
        _currentUserName = username;
        _isLoggedIn = true;

        if (UserStatus.Instance != null)
        {
            UserStatus.Instance.UserId = _currentUserId;
            UserStatus.Instance.Name = _currentUserName;
            UserStatus.Instance.Email = userEmail;
        }

        PlayerPrefs.SetString("SavedUserId", _currentUserId);
        PlayerPrefs.SetString("SavedUserName", _currentUserName);
        PlayerPrefs.Save();

        Debug.Log($"[MockAuth] Registration successful: {_currentUserName}");
        return AuthResult.Ok(_currentUserId, _currentUserName, $"Account created for {_currentUserName}!");
    }

    public async Task<bool> SaveDataAsync(string userId)
    {
        await Task.Yield();
        try
        {
            if (UserStatus.Instance == null) return false;

            var u = UserStatus.Instance;
            PlayerPrefs.SetString("PlayerCharacterCreated", "Yes");
            PlayerPrefs.SetString("PlayerName", u.PName);
            PlayerPrefs.SetString("PlayerCharacterName", u.CName);
            PlayerPrefs.SetString("PlayerGender", u.Gender);
            PlayerPrefs.SetInt("PlayerLevel", u.PLevel);
            PlayerPrefs.SetInt("PlayerMaxHP", u.MAXHP);
            PlayerPrefs.SetInt("PlayerHP", u.HP);
            PlayerPrefs.SetInt("PlayerMaxMP", u.MAXMP);
            PlayerPrefs.SetInt("PlayerMP", u.MP);
            PlayerPrefs.SetInt("PlayerPhysicalAttack", u.PHYSICALATTACK);
            PlayerPrefs.SetInt("PlayerDefense", u.DEFENSE);
            PlayerPrefs.SetInt("PlayerMagicalAttack", u.MAGICALATTACK);
            PlayerPrefs.SetInt("PlayerMagicDefense", u.MAGICDEFENSE);
            PlayerPrefs.SetInt("PlayerAccuracy", u.ACCURACY);
            PlayerPrefs.SetInt("PlayerFlee", u.FLEE);
            PlayerPrefs.SetInt("PlayerCrit", u.CRIT);
            PlayerPrefs.SetInt("PlayerStrength", u.STR);
            PlayerPrefs.SetInt("PlayerVitality", u.VIT);
            PlayerPrefs.SetInt("PlayerAgility", u.AGI);
            PlayerPrefs.SetInt("PlayerDexterity", u.DEX);
            PlayerPrefs.SetInt("PlayerIntelligence", u.INT);
            PlayerPrefs.SetInt("PlayerLuck", u.LCK);
            PlayerPrefs.SetInt("PlayerCoin", u.COIN);
            PlayerPrefs.SetInt("PlayerExperience", u.EXP);
            string saveClass = !string.IsNullOrEmpty(u.Class) ? u.Class : (!string.IsNullOrEmpty(u.CName) ? u.CName : "WarriorClass");
            PlayerPrefs.SetString("PlayerClass", saveClass);
            PlayerPrefs.SetString("PlayerCharacterName", saveClass);
            PlayerPrefs.SetInt("PlayerIndexClass", u.IndexClass);
            PlayerPrefs.SetString("PlayerWeapon", u.WEAPON);
            PlayerPrefs.SetString("PlayerOffHand", u.OFFHAND);
            PlayerPrefs.SetString("PlayerArmor", u.ARMOR);
            PlayerPrefs.SetString("PlayerCape", u.CAPE);
            PlayerPrefs.SetString("PlayerHelm", u.HELM);
            PlayerPrefs.SetInt("PlayerCurrentIndex", u.currentIndex);
            PlayerPrefs.SetInt("PlayerMaxIndex", u.maxIndex);

            // Legacy keys used by SaveGameController and LoginController
            PlayerPrefs.SetString("PlayerPName", u.PName);
            PlayerPrefs.SetInt("PlayerPLevel", u.PLevel);
            PlayerPrefs.SetInt("PlayerMAXHP", u.MAXHP);
            PlayerPrefs.SetInt("PlayerMAXMP", u.MAXMP);
            PlayerPrefs.SetInt("PlayerPHYSICALATTACK", u.PHYSICALATTACK);
            PlayerPrefs.SetInt("PlayerDEFENSE", u.DEFENSE);
            PlayerPrefs.SetInt("PlayerMAGICALATTACK", u.MAGICALATTACK);
            PlayerPrefs.SetInt("PlayerMAGICDEFENSE", u.MAGICDEFENSE);
            PlayerPrefs.SetInt("PlayerACCURACY", u.ACCURACY);
            PlayerPrefs.SetInt("PlayerFLEE", u.FLEE);
            PlayerPrefs.SetInt("PlayerCRIT", u.CRIT);
            PlayerPrefs.SetInt("PlayerSTR", u.STR);
            PlayerPrefs.SetInt("PlayerVIT", u.VIT);
            PlayerPrefs.SetInt("PlayerAGI", u.AGI);
            PlayerPrefs.SetInt("PlayerDEX", u.DEX);
            PlayerPrefs.SetInt("PlayerINT", u.INT);
            PlayerPrefs.SetInt("PlayerLCK", u.LCK);
            PlayerPrefs.SetInt("PlayerCOIN", u.COIN);
            PlayerPrefs.SetInt("PlayerEXP", u.EXP);
            PlayerPrefs.SetString("PlayerWEAPON", u.WEAPON);
            PlayerPrefs.SetString("PlayerARMOR", u.ARMOR);

            PlayerPrefs.Save();
            Debug.Log("[MockStorage] Player data successfully written to PlayerPrefs.");
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError("[MockStorage] Error writing player data to PlayerPrefs: " + ex);
            return false;
        }
    }

    public async Task<bool> LoadDataAsync(string userId)
    {
        await Task.Yield();
        try
        {
            if (UserStatus.Instance == null) return false;
            var u = UserStatus.Instance;

            if (HasSavedCharacter(userId))
            {
                u.PName = PlayerPrefs.GetString("PlayerName", "Hero");
                u.CName = PlayerPrefs.GetString("PlayerCharacterName", PlayerPrefs.GetString("PlayerClass", "WarriorClass"));
                u.Gender = PlayerPrefs.GetString("PlayerGender", "Male");
                u.PLevel = PlayerPrefs.GetInt("PlayerLevel", PlayerPrefs.GetInt("PlayerPLevel", 1));
                u.MAXHP = PlayerPrefs.GetInt("PlayerMaxHP", PlayerPrefs.GetInt("PlayerMAXHP", 100));
                u.HP = PlayerPrefs.GetInt("PlayerHP", u.MAXHP);
                if (u.HP <= 0 || u.HP > u.MAXHP) u.HP = u.MAXHP;
                u.MAXMP = PlayerPrefs.GetInt("PlayerMaxMP", PlayerPrefs.GetInt("PlayerMAXMP", 50));
                u.MP = PlayerPrefs.GetInt("PlayerMP", u.MAXMP);
                if (u.MP <= 0 || u.MP > u.MAXMP) u.MP = u.MAXMP;
                u.PHYSICALATTACK = PlayerPrefs.GetInt("PlayerPhysicalAttack", PlayerPrefs.GetInt("PlayerPHYSICALATTACK", 10));
                u.DEFENSE = PlayerPrefs.GetInt("PlayerDefense", PlayerPrefs.GetInt("PlayerDEFENSE", 5));
                u.MAGICALATTACK = PlayerPrefs.GetInt("PlayerMagicalAttack", PlayerPrefs.GetInt("PlayerMAGICALATTACK", 10));
                u.MAGICDEFENSE = PlayerPrefs.GetInt("PlayerMagicDefense", PlayerPrefs.GetInt("PlayerMAGICDEFENSE", 5));
                u.ACCURACY = PlayerPrefs.GetInt("PlayerAccuracy", PlayerPrefs.GetInt("PlayerACCURACY", 10));
                u.FLEE = PlayerPrefs.GetInt("PlayerFlee", PlayerPrefs.GetInt("PlayerFLEE", 5));
                u.CRIT = PlayerPrefs.GetInt("PlayerCrit", PlayerPrefs.GetInt("PlayerCRIT", 5));
                u.STR = PlayerPrefs.GetInt("PlayerStrength", PlayerPrefs.GetInt("PlayerSTR", 5));
                u.VIT = PlayerPrefs.GetInt("PlayerVitality", PlayerPrefs.GetInt("PlayerVIT", 5));
                u.AGI = PlayerPrefs.GetInt("PlayerAgility", PlayerPrefs.GetInt("PlayerAGI", 5));
                u.DEX = PlayerPrefs.GetInt("PlayerDexterity", PlayerPrefs.GetInt("PlayerDEX", 5));
                u.INT = PlayerPrefs.GetInt("PlayerIntelligence", PlayerPrefs.GetInt("PlayerINT", 5));
                u.LCK = PlayerPrefs.GetInt("PlayerLuck", PlayerPrefs.GetInt("PlayerLCK", 5));
                u.COIN = PlayerPrefs.GetInt("PlayerCoin", PlayerPrefs.GetInt("PlayerCOIN", 1500));
                u.EXP = PlayerPrefs.GetInt("PlayerExperience", PlayerPrefs.GetInt("PlayerEXP", 0));
                u.Class = PlayerPrefs.GetString("PlayerClass", "WarriorClass");
                if (string.IsNullOrEmpty(u.Class) || u.Class == "None")
                {
                    u.Class = !string.IsNullOrEmpty(u.CName) ? u.CName : "WarriorClass";
                }
                if (string.IsNullOrEmpty(u.CName))
                {
                    u.CName = u.Class;
                }
                if (u.PHYSICALATTACK < 120)
                {
                    u.GetExtraStat();
                }
                u.IndexClass = PlayerPrefs.GetInt("PlayerIndexClass", 0);
                u.WEAPON = PlayerPrefs.GetString("PlayerWeapon", PlayerPrefs.GetString("PlayerWEAPON", ""));
                u.OFFHAND = PlayerPrefs.GetString("PlayerOffHand", "");
                u.ARMOR = PlayerPrefs.GetString("PlayerArmor", PlayerPrefs.GetString("PlayerARMOR", ""));
                u.CAPE = PlayerPrefs.GetString("PlayerCape", "");
                u.HELM = PlayerPrefs.GetString("PlayerHelm", "");
                u.currentIndex = PlayerPrefs.GetInt("PlayerCurrentIndex", 0);
                u.maxIndex = PlayerPrefs.GetInt("PlayerMaxIndex", 1);

                Debug.Log("[MockStorage] Player data successfully loaded from PlayerPrefs.");
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Debug.LogError("[MockStorage] Error loading player data from PlayerPrefs: " + ex);
            return false;
        }
    }

    public bool HasSavedCharacter(string userId)
    {
        return PlayerPrefs.HasKey("PlayerCharacterCreated") || PlayerPrefs.HasKey("PlayerName");
    }
}
