using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
using TMPro;
using System.Threading.Tasks;

public class LoginController : MonoBehaviour
{

    public CharacterClassData classDatabase;
    public Button newGame;
    public Button continueGame;

    public UseCase useCase = new UseCase();

    [Space]
    [Header("Login")]
    public TMP_InputField emailInputField;

    public Toggle rememberToggle;

    private const string EmailKey = "UserEmail";

    void Start()
    {
        Screen.SetResolution(1920, 1080, true);

        if (PlayerPrefs.HasKey(EmailKey))
        {
            string savedEmail = PlayerPrefs.GetString(EmailKey);
            emailInputField.text = savedEmail; // Populate email field
            rememberToggle.isOn = true;        // Set toggle to on
        }
        else
        {
            rememberToggle.isOn = false;       // Set toggle to off if no saved email
        }

        rememberToggle.onValueChanged.AddListener(OnRememberToggleChanged);
    }

    private void OnRememberToggleChanged(bool isOn)
    {
        if (isOn)
        {
            SaveEmail(emailInputField.text); // Save email if toggle is on
        }
        else
        {
            ClearEmail();                    // Clear email if toggle is off
        }
    }


    private void SaveEmail(string email)
    {
        PlayerPrefs.SetString(EmailKey, email);
        PlayerPrefs.Save();
        Debug.Log("Email saved: " + email);
    }

    // Function to clear saved email
    private void ClearEmail()
    {
        PlayerPrefs.DeleteKey(EmailKey);
        Debug.Log("Email cleared from PlayerPrefs.");
    }

    // Call this when the user submits the login form
    public void OnLoginButtonClick()
    {
        string email = emailInputField.text;

        // Save or clear email based on the toggle's state
        if (rememberToggle.isOn)
        {
            SaveEmail(email);
        }
        else
        {
            ClearEmail();
        }

        // Proceed with the rest of your login process
    }

    void OnCheckSaveData(){
        string dataName = PlayerPrefs.GetString("PlayerName");
        if(dataName!=""){
            continueGame.gameObject.SetActive(true);
        }else{
            continueGame.gameObject.SetActive(false);
        }

    }
    public void ContinueGame(){
       UserStatus.Instance.PName =  PlayerPrefs.GetString("PlayerPName");
       UserStatus.Instance.CName =  PlayerPrefs.GetString("PlayerClass");
       UserStatus.Instance.Gender =  PlayerPrefs.GetString("PlayerGender");
       UserStatus.Instance.PLevel =  PlayerPrefs.GetInt("PlayerPLevel");
       UserStatus.Instance.MAXHP =  PlayerPrefs.GetInt("PlayerMAXHP");
       UserStatus.Instance.HP =  PlayerPrefs.GetInt("PlayerHP");
       UserStatus.Instance.MAXMP =  PlayerPrefs.GetInt("PlayerMAXMP");
       UserStatus.Instance.MP =  PlayerPrefs.GetInt("PlayerMP");
       UserStatus.Instance.PHYSICALATTACK =  PlayerPrefs.GetInt("PlayerPHYSICALATTACK");
       UserStatus.Instance.DEFENSE =  PlayerPrefs.GetInt("PlayerDEFENSE");
       UserStatus.Instance.MAGICALATTACK =  PlayerPrefs.GetInt("PlayerMAGICALATTACK");
       UserStatus.Instance.MAGICDEFENSE =  PlayerPrefs.GetInt("PlayerMAGICDEFENSE");
       UserStatus.Instance.ACCURACY =  PlayerPrefs.GetInt("PlayerACCURACY");
       UserStatus.Instance.FLEE =  PlayerPrefs.GetInt("PlayerFLEE");
       UserStatus.Instance.CRIT =  PlayerPrefs.GetInt("PlayerCRIT");

       UserStatus.Instance.STR =  PlayerPrefs.GetInt("PlayerSTR");
       UserStatus.Instance.VIT =  PlayerPrefs.GetInt("PlayerVIT");
       UserStatus.Instance.AGI =  PlayerPrefs.GetInt("PlayerAGI");
       UserStatus.Instance.DEX =  PlayerPrefs.GetInt("PlayerDEX");
       UserStatus.Instance.INT =  PlayerPrefs.GetInt("PlayerINT");
       UserStatus.Instance.LCK =  PlayerPrefs.GetInt("PlayerLCK");

       UserStatus.Instance.COIN =  PlayerPrefs.GetInt("PlayerCOIN");
       UserStatus.Instance.EXP =  PlayerPrefs.GetInt("PlayerEXP");   

       UserStatus.Instance.WEAPON =  PlayerPrefs.GetString("PlayerWEAPON");
       UserStatus.Instance.ARMOR =  PlayerPrefs.GetString("PlayerARMOR");

        string playerClassName = PlayerPrefs.GetString("PlayerClass");

    //     CharacterClassData.ClassData _moves = classDatabase.classData.Find(m => m.Class == playerClassName);
    //    UserStatus.Instance.skill = _moves.moves;
    }
}
