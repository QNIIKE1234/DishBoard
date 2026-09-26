using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class FirebaseConnection : MonoBehaviour
{
    public bool isFirebaseInitialized = true;

    public async Task<bool> LoginAsync(string email, string password)
    {
        await Task.Yield();
        if (UserStatus.Instance != null)
        {
            UserStatus.Instance.UserId = "Guest_" + SystemInfo.deviceUniqueIdentifier;
            UserStatus.Instance.Name = string.IsNullOrEmpty(email) ? "Guest" : email;
            UserStatus.Instance.Email = "guest@local";
        }
        return true;
    }
}
