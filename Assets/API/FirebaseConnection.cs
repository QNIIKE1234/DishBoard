using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
using TMPro;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class FirebaseConnection : MonoBehaviour
{
UserStatus userStatus;
public DependencyStatus dependencyStatus;
public bool isFirebaseInitialized = false;
private FirebaseFirestore firestore;
public FirebaseAuth auth;
public FirebaseUser user;
    private void Awake(){
    FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            dependencyStatus = task.Result;
            
            if(dependencyStatus == DependencyStatus.Available){
                // Initialize Firebase
                FirebaseApp app = FirebaseApp.DefaultInstance;
                InitializeFirebase();
                Debug.Log("Firebase initialized and databaseReference is set.");
            }
            else{
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    void InitializeFirebase() {
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);

        firestore = FirebaseFirestore.DefaultInstance;
        isFirebaseInitialized = true;
    }


    void AuthStateChanged(object sender, System.EventArgs eventArgs){
        if(auth.CurrentUser  != user){
            bool signIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if(!signIn && user != null){
                Debug.Log("Signed out "+ user.UserId);
            }

            user = auth.CurrentUser;

            if(signIn){
                Debug.Log("Signed in "+ user.UserId);
            }
        }

    }

    #region
    public async Task<bool> LoginAsync(string _email, string _password){

        bool _success = false;

        var loginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);


        if (loginTask.Exception != null)
        {
            Debug.LogError(loginTask.Exception);

            FirebaseException firebaseException = loginTask.Exception.GetBaseException() as FirebaseException;
            AuthError authError = (AuthError)firebaseException.ErrorCode;


            string failedMessage = "Login Failed! Because ";

            switch (authError)
            {
                case AuthError.InvalidEmail:
                    failedMessage += "Email is invalid";
                    break;
                case AuthError.WrongPassword:
                    failedMessage += "Wrong Password";
                    break;
                case AuthError.MissingEmail:
                    failedMessage += "Email is missing";
                    break;
                case AuthError.MissingPassword:
                    failedMessage += "Password is missing";
                    break;
                default:
                    failedMessage = "Login Failed";
                    break;
            }
            PopupManager.Instance.OnLoginAlert(failedMessage,false);
            Debug.Log(failedMessage);
            _success = false;
        }
        else
        {
            user = loginTask.Result.User;
            PopupManager.Instance.OnLoginAlert("You Are Successfully Logged In "+ user.DisplayName,true);

            // Store user data
            userStatus.UserId = user.UserId;
            userStatus.Name = user.DisplayName;
            userStatus.Email = user.Email;
            
            // StartCoroutine(CheckCharacterData(user.UserId, user.DisplayName, email));
            _success = true;
        }

        return _success;
    }

    public async Task<bool> RegisterAsync(string name, string email, string password, string confirmPassword){

        bool _success = false;
        bool _sendDataSuccess = false;
        if (name == "")
        {
            Debug.LogError("User Name is empty");
        }
        else if (email == "")
        {
            Debug.LogError("email field is empty");
        }
        else if (password != confirmPassword)
        {
            Debug.LogError("Password does not match");
        }
        else
        {
            if (auth == null)
            {
                return false;
            }
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email,password);

            if (registerTask.Exception != null)
            {
                Debug.LogError(registerTask.Exception);

                FirebaseException firebaseException = registerTask.Exception.GetBaseException() as FirebaseException;
                AuthError authError = (AuthError)firebaseException.ErrorCode;

                string failedMessage = "Registration Failed! Becuase ";
                switch (authError)
                {
                    case AuthError.InvalidEmail:
                        failedMessage += "Email is invalid";
                        break;
                    case AuthError.WrongPassword:
                        failedMessage += "Wrong Password";
                        break;
                    case AuthError.MissingEmail:
                        failedMessage += "Email is missing";
                        break;
                    case AuthError.MissingPassword:
                        failedMessage += "Password is missing";
                        break;
                    default:
                        failedMessage = "Registration Failed";
                        break;
                }

                Debug.Log(failedMessage);
            }
            else
            {
                // Get The User After Registration Success
                user = registerTask.Result.User;

                UserProfile userProfile = new UserProfile { DisplayName = name };

                var updateProfileTask = user.UpdateUserProfileAsync(userProfile);

                if (updateProfileTask.Exception != null)
                {
                    // Delete the user if user update failed
                    user.DeleteAsync();

                    Debug.LogError(updateProfileTask.Exception);

                    FirebaseException firebaseException = updateProfileTask.Exception.GetBaseException() as FirebaseException;
                    AuthError authError = (AuthError)firebaseException.ErrorCode;


                    string failedMessage = "Profile update Failed! Becuase ";
                    switch (authError)
                    {
                        case AuthError.InvalidEmail:
                            failedMessage += "Email is invalid";
                            break;
                        case AuthError.WrongPassword:
                            failedMessage += "Wrong Password";
                            break;
                        case AuthError.MissingEmail:
                            failedMessage += "Email is missing";
                            break;
                        case AuthError.MissingPassword:
                            failedMessage += "Password is missing";
                            break;
                        default:
                            failedMessage = "Profile update Failed";
                            break;
                    }
                    PopupManager.Instance.OnLoginAlert(failedMessage,false);
                    Debug.Log(failedMessage);
                    _success = false;
                }
                else
                {
                    Debug.Log("Registration Sucessful Welcome " + user.DisplayName);
                    _sendDataSuccess = await AddUserData(user.UserId, name, email);
                    _success = true;
                }
            }
        }
        return _success;
    }
    #endregion


    private async Task<bool> AddUserData(string userId, string userName, string email)
    {
        DocumentReference docRef = firestore.Collection("users").Document(userId);
        bool _sendDataSuccess = false;

        var userData = new Dictionary<string, object>
        {
            { "Name", userName },
            { "Email", email },
            { "Character",null}
        };

        docRef.SetAsync(userData).ContinueWithOnMainThread(task => {
            if (task.IsCompleted && !task.IsFaulted)
            {
                Debug.Log("User data added successfully");
                _sendDataSuccess = true;
            }
            else
            {
                Debug.LogError("Failed to add user data: " + task.Exception);
                _sendDataSuccess = false;
            }
        });

        return _sendDataSuccess;
    }
}
