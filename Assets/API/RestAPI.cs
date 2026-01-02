using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
public class RestAPI : MonoBehaviour
{
    private string URL = "https://6718a2807fc4c5ff8f4a5039.mockapi.io/game/Login/users";

    public TextMeshProUGUI  _name;

    public int index;
    public static RestAPI Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
		
		DontDestroyOnLoad(this);
    }

    IEnumerator GetData(){
        using(UnityWebRequest request = UnityWebRequest.Get(URL)){
            yield return request.SendWebRequest();
            
            if(request.result == UnityWebRequest.Result.ConnectionError){
                Debug.LogError(request.error);
            }
            else{
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json);
                _name.text = "Name : "+stats[index]["Name"];
            }
        }
    }
}
