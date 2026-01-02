using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
using TMPro;
using System.Threading.Tasks;
public class FirebaseAuthManager : MonoBehaviour
{
    UseCase useCase;
    UserStatus userStatus = new UserStatus();
    [SerializeField] SceneChanger sceneChanger;
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;
    private FirebaseFirestore firestore;

    [Space]
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;

    [Space]
    [Header("Regisreation")]
    public TMP_InputField nameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField confirmPasswordRegisterField;

    [Space]
    [Header("Panel")]
    public GameObject MenuPanel;
    public GameObject LoginNResigPanel;
    public GameObject PresstoStartPanel;

    public bool isFirebaseInitialized = false;
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

    public void Login()
    {
        StartCoroutine(LoginAsync(emailLoginField.text, passwordLoginField.text));
    }
    private IEnumerator LoginAsync(string email, string password)
    {
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => loginTask.IsCompleted);

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
        }
        else
        {
            user = loginTask.Result.User;
            PopupManager.Instance.OnLoginAlert("You Are Successfully Logged In "+ user.DisplayName,true);

            // Store user data
            UserStatus.Instance.UserId = user.UserId;
            UserStatus.Instance.Name = user.DisplayName;
            UserStatus.Instance.Email = user.Email;

            MenuPanel.SetActive(false);
            LoginNResigPanel.SetActive(false);
            PresstoStartPanel.SetActive(true);
        }
    }

    public void OnPressToStrat(){
        StartCoroutine(CheckCharacterData( UserStatus.Instance.UserId,  UserStatus.Instance.Name ,  UserStatus.Instance.Email));
    }

    public void Register()
    {
        if(!isFirebaseInitialized){
            Debug.LogError("Firebase is not initialized. Cannot register.");
            return;           
        }
        StartCoroutine(RegisterAsync(nameRegisterField.text, emailRegisterField.text, passwordRegisterField.text, confirmPasswordRegisterField.text));
    }

    private IEnumerator RegisterAsync(string name, string email, string password, string confirmPassword)
    {

        if (name == "")
        {
            Debug.LogError("User Name is empty");
        }
        else if (email == "")
        {
            Debug.LogError("email field is empty");
        }
        else if (passwordRegisterField.text != confirmPasswordRegisterField.text)
        {
            Debug.LogError("Password does not match");
        }
        else
        {
            if (auth == null)
            {
                Debug.LogError("Firebase Auth is not initialized");
                yield break;
            }
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email,password);

            yield return new WaitUntil(() => registerTask.IsCompleted);

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

                yield return new WaitUntil(() => updateProfileTask.IsCompleted);

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

                    Debug.Log(failedMessage);
                }
                else
                {
                    Debug.Log("Registration Sucessful Welcome " + user.DisplayName);
                    nameRegisterField.text = "";
                    emailRegisterField.text= "";
                    passwordRegisterField.text= ""; 
                    confirmPasswordRegisterField.text = "";
                    AddUserData(user.UserId, name, email);
                }
            }
        }
    }

    private void AddUserData(string userId, string userName, string email)
    {
        DocumentReference docRef = firestore.Collection("users").Document(userId);

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
                AddUserCharacterData(user.UserId, name, email);
            }
            else
            {
                Debug.LogError("Failed to add user data: " + task.Exception);
            }
        });
    }
    private void AddUserCharacterData(string userId, string userName, string email)
    {
        DocumentReference docRef = firestore.Collection("characterData").Document(userId);
        var userData = new Dictionary<string, object>
        {
            { "CharacterName", "" },
        };
        docRef.SetAsync(userData).ContinueWithOnMainThread(task => {
            if (task.IsCompleted && !task.IsFaulted)
            {
                Debug.Log("User data added successfully");
            }
            else
            {
                Debug.LogError("Failed to add user data: " + task.Exception);
            }
        });
    }
    private IEnumerator CheckCharacterData(string userId, string name, string email)
    {
        // Access the user's document
        DocumentReference docRef = firestore.Collection("characterData").Document(userId);
        var docTask = docRef.GetSnapshotAsync();
        PopupManager.Instance.OpenLoading("isFadeIn");
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => docTask.IsCompleted);
        if (docTask.Exception != null)
        {
            Debug.LogError("Error getting document: " + docTask.Exception);
        }
        else
        {
            // Check if the document exists
            if (docTask.Result.Exists)
            {
                // Check if the "Character" field exists
                if (docTask.Result.ContainsField("Character"))
                {
                    // Get the value of the "Character" field
                    object characterValue = docTask.Result.GetValue<object>("Character");

                    if (characterValue == null) // If Character is null
                    {
                        Debug.Log("Character field is null. Redirecting to SelectCharacter scene.");
                        sceneChanger.ChangeScene("CreateCharacter");
                    }
                    else
                    {
                        Debug.Log("Character field already has a value: " + characterValue);
                        // PlayerData.Instance.GetDataToFirestore(userId);
                        
                        sceneChanger.ChangeScene("Dungeon");
                    }
                    
                }
                else
                {
                    Debug.Log("Character field does not exist. Redirecting to SelectCharacter scene.");
                    sceneChanger.ChangeScene("CreateCharacter");
                }
            }
            else
            {
                Debug.Log("User does not exist. Creating new user data.");
                // Optionally, you can create the user data here
                // CreateUserData(userId, name, email); // Call to a method that creates user data
            }
        }
    }

}