using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{

    public static GameController Instance { get; private set; }
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
		
     }
    [SerializeField] SceneChanger sceneChanger;
    [SerializeField] StageList stageList;
    public List<GameObject> stageListData = new List<GameObject>();
    public float FollowSpeed = 5f;
    public float xOffset = 0f;
    public float yOffset = 0f;
    public Transform contentStage;
    public GameObject playerObject;
    public GameObject stageObject;
    void Start()
    {

        stageListData = stageList.StageListData;
        CreateMap();
    }

    void Update()
    {
        if(playerObject!=null){
            onUpdatePos();
        }

    }

    public void CreateMap(){

        GameObject _newButton = Instantiate(stageListData[0], contentStage);
        stageObject = _newButton;
    }

    public void OpenInventory(){
        PopupManager.Instance.OpenInventoryPanel(contentStage);
    }

    public void onUpdatePos(){
        Vector3 oppositePos = new Vector3(-playerObject.transform.position.x + xOffset, -playerObject.transform.position.y + yOffset, playerObject.transform.position.z);
        
        stageObject.transform.position = Vector3.Slerp(stageObject.transform.position, oppositePos, FollowSpeed * Time.deltaTime);
    }

    public void onChangeStage(string _stage){
        if(_stage == "Portal"){
            sceneChanger.ChangeScene("Dungeon");
        }
        else if(_stage == "NextPortal"){
            Destroy(this);
            sceneChanger.ChangeScene("Dungeon");

        }
        else if(_stage == "Monster"){
            sceneChanger.ChangeScene("BattleScene");
        }
        else if(_stage == "Boss"){
          sceneChanger.ChangeScene("BattleScene");     
        }
        else if(_stage== "Market"){
            sceneChanger.ChangeScene("Market");
        }
        else if(_stage == "MiniGame"){
            sceneChanger.ChangeScene("Market");
        }
    }
}
