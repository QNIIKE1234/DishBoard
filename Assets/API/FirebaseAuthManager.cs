using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FirebaseAuthManager : MonoBehaviour
{
    [SerializeField] SceneChanger sceneChanger;

    [Space]
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;

    [Space]
    [Header("Registration")]
    public TMP_InputField nameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField confirmPasswordRegisterField;

    [Space]
    [Header("Panel")]
    public GameObject MenuPanel;
    public GameObject LoginNResigPanel;
    public GameObject PresstoStartPanel;

    public bool isFirebaseInitialized = true;

    private void Awake()
    {
        isFirebaseInitialized = true;
    }

    private void Start()
    {
        if (MenuPanel != null) MenuPanel.SetActive(true);
        if (PresstoStartPanel != null) PresstoStartPanel.SetActive(false);

        // Ensure Login and Register sub-panels are hidden until user clicks Login/Register on MENU
        if (LoginNResigPanel != null)
        {
            LoginNResigPanel.SetActive(true);
            foreach (Transform child in LoginNResigPanel.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Regular login (reads email & password input fields if provided, otherwise logs in as Guest/saved user)
    /// </summary>
    public async void Login()
    {
        string email = emailLoginField != null ? emailLoginField.text.Trim() : "";
        string password = passwordLoginField != null ? passwordLoginField.text.Trim() : "";

        var result = await UseCase.AuthService.LoginAsync(email, password);
        HandleAuthSuccess(result);
    }

    /// <summary>
    /// Dedicated Guest Login button handler
    /// </summary>
    public async void LoginGuest()
    {
        var result = await UseCase.AuthService.GuestLoginAsync();
        HandleAuthSuccess(result);
    }

    /// <summary>
    /// Registration handler
    /// </summary>
    public async void Register()
    {
        string name = nameRegisterField != null ? nameRegisterField.text.Trim() : "Hero";
        string email = emailRegisterField != null ? emailRegisterField.text.Trim() : "";
        string password = passwordRegisterField != null ? passwordRegisterField.text.Trim() : "";

        var result = await UseCase.AuthService.RegisterAsync(name, email, password);

        if (nameRegisterField != null) nameRegisterField.text = "";
        if (emailRegisterField != null) emailRegisterField.text = "";
        if (passwordRegisterField != null) passwordRegisterField.text = "";
        if (confirmPasswordRegisterField != null) confirmPasswordRegisterField.text = "";

        HandleAuthSuccess(result);
    }

    private void HandleAuthSuccess(AuthResult result)
    {
        if (result != null && result.Success)
        {
            if (PopupManager.Instance != null)
            {
                PopupManager.Instance.OnLoginAlert(result.Message, true);
            }

            if (MenuPanel != null) MenuPanel.SetActive(false);
            if (LoginNResigPanel != null) LoginNResigPanel.SetActive(false);
            if (PresstoStartPanel != null) PresstoStartPanel.SetActive(true);
        }
        else
        {
            if (PopupManager.Instance != null)
            {
                PopupManager.Instance.OnLoginAlert(result != null ? result.Message : "Login failed!", false);
            }
        }
    }

    public void OnPressToStrat()
    {
        StartCoroutine(CheckCharacterData());
    }

    private IEnumerator CheckCharacterData()
    {
        if (PopupManager.Instance != null)
        {
            PopupManager.Instance.OpenLoading("isFadeIn");
        }
        yield return new WaitForSeconds(0.5f);

        string userId = UserStatus.Instance != null ? UserStatus.Instance.UserId : "";
        bool hasCharacter = UseCase.DataService.HasSavedCharacter(userId);

        if (hasCharacter)
        {
            Debug.Log("[AuthManager] Character data found. Loading data and going to Dungeon.");
            var loadTask = UseCase.DataService.LoadDataAsync(userId);
            yield return new WaitUntil(() => loadTask.IsCompleted);

            if (sceneChanger != null)
            {
                sceneChanger.ChangeScene("Dungeon");
            }
        }
        else
        {
            Debug.Log("[AuthManager] No character data found. Redirecting to CreateCharacter scene.");
            if (sceneChanger != null)
            {
                sceneChanger.ChangeScene("CreateCharacter");
            }
        }
    }
}