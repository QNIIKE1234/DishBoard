using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TF : MonoBehaviour
{

    public GameObject dialogBox;
    public bool playerInRange;
    public Animator transition;
    public Text dialogText;
    public string dialog;
    float timer = 0.0f;
    
    public float transitionTime = 3f;
     [SerializeField] private W_TEXT w_TEXT;


    void Start()
    {
        dialogText = transform.Find("dialog").Find("dialogText").GetComponent<Text>();
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        timer += Time.deltaTime;
         w_TEXT.addWriter(dialogText,
              "Long ago the far distant past the gods along with their families and creations waged a seemingly never-ending war for the right to rule this world they fought for the right to become the one true god eventually it become clear there would be no real victor as the sky the sea the land and the star were all destroyed in the devastating battles however hidden amongst the wreckage a single deity had survived although he never participated in the conflict the title became his his name was tet and he was know as the god of play.\n You creature who have fouht with violence blood and death who built a tower of corpses that rises to the sky and who still call yourselves wise tell me this what makes you different from wild animals!!\n In the face of their shttered world excuses where meaninggless and god spoke thou shalt not kill one another murder ad thuevery in this realm is henceforth forbidden.\n You sixteen races full of greed and hubris i command you to use you ingenuity and wit your great fortunes and power to buil me a tower of knowledge and to prove to me.\n\n\n GOD No.1"
              ,0.07f);
        Debug.Log(timer);
        
        if(timer>75f){
            transition.SetTrigger("Load"); 
            LoadNextLVL();
        }
        
    }

      public void LoadNextLVL()
{
    StartCoroutine(Loadinggame(SceneManager.GetActiveScene().buildIndex + 1));
    // SceneManager.LoadScene();
}


    public void Skip(){
        transition.SetTrigger("Load");
        LoadNextLVL();
    }


     IEnumerator Loadinggame(int levelIndex){
      
    yield return new WaitForSeconds(transitionTime);

    SceneManager.LoadScene(levelIndex);
  }
}
