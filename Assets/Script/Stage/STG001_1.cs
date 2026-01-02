using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Threading.Tasks;

public class STG001_1 : MonoBehaviour
{
	UseCase useCase;
	public List<AreaData> areaDataList = new List<AreaData>();
    public List<GameObject> buttonStageList = new List<GameObject>();
    public int stage = 1;
    public GameObject playerObject;
    public Animator playerAnim;
    public CharacterAnimationController player;
    public GameObject buttonStagePrefab;
    public Transform contentStage;
    public bool isMarket = false;

    public bool isPortal = false;

    public bool isMiniGame = false;

    public bool isBossfightGame = false;

    public string eventType;

    Character character;

	[System.Serializable]
	public class AreaData
	{
		public string ID;
		public string Name;
        public string eventName;
        public int stageIndex;
        public float xOffset = 0f;
        public float yOffset = 0f;
        public List<int> openStage = new List<int>();

	}

    public int currentPlayerPos;

	public List<Transform> PinPoint = new List<Transform>();
	public List<Transform> ButtonSpawn = new List<Transform>();
    async void Awake()
    {
        PlayerPrefs.SetString("PreviousScene","Dungeon");
        PlayerPrefs.SetString("CurrentStageName","STG001_1");
        
        if( PlayerPrefs.GetString("CurrentStageName") == "STG001_1"){
            currentPlayerPos = UserStatus.Instance.currentIndex;
        }else{
            currentPlayerPos = UserStatus.Instance.currentIndex;
        }

        CreatePinPos();

        bool result = await OnLoadData();
        if(result){
            
        }else{

        }
        StartCoroutine(OnCreateCharacter());
  
    }
    public async Task<bool> OnLoadData()
    { // Replace with your actual user ID
        bool result = await UserStatus.Instance.useCase.LoadDataToFirestore(UserStatus.Instance.UserId);

        if (result)
        {
            Debug.Log("Data Load successfully.");
        }
        else
        {
            Debug.Log("Failed to send data.");
        }
        return result;
    }
    // Update is called once per frame
    IEnumerator OnCreateCharacter()
    {
        PopupManager.Instance.OpenLoading("isFadeIn");
        yield return new WaitForSeconds(1.5f);

        GameObject _PLAYER = Instantiate(playerObject,contentStage);
        playerObject = _PLAYER;
        playerAnim = _PLAYER.GetComponent<Animator>();
        player = _PLAYER.GetComponent<CharacterAnimationController>();
        _PLAYER.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        _PLAYER.transform.position = PinPoint[UserStatus.Instance.currentIndex].transform.position;
        player.UpdateAnimClass();
        player.OnUpDateEquipment();

        GameController.Instance.playerObject = _PLAYER;
    }

    public void CreatePinPos(){

        foreach(GameObject target in buttonStageList)
		{
			Destroy(target);
		}
		buttonStageList.Clear();

        int randomInt ;
        for(int i =0; i<areaDataList.Count; i++){
            GameObject _newButton = Instantiate(buttonStagePrefab, ButtonSpawn[i]);
            ButtonPositionEvent _button = _newButton.GetComponent<ButtonPositionEvent>();
            buttonStageList.Add(_newButton);
            _button.SetDataEvent(areaDataList[i].eventName,i,(i)=>{onMovePlayerObject(i);},i);

        }

        onOpenStage();

    }

    public void onMovePlayerObject(int index)
    {
        
        if (playerObject == null)
        {
            Debug.LogError("playerObject is not assigned.");
            return;
        }

        StartCoroutine(MovePlayer(index));

    }

    IEnumerator MovePlayer(int movePos)
    {

        Vector3 newScale = playerObject.transform.localScale;
        if(movePos< PlayerPrefs.GetInt("CurrentPlayerPos")){
            newScale.x = -1 * Mathf.Abs(newScale.x); // Ensure the y scale is set to -1
        }else{
            newScale.x = 1 * Mathf.Abs(newScale.x); // Ensure the y scale is set to 1
        }
        playerObject.transform.localScale = newScale;
        PlayerPrefs.SetInt("CurrentPlayerPos",movePos);
        eventType = PlayerPrefs.GetString("EventType");
        playerAnim.SetBool("onDash",true);

        yield return new WaitForSeconds(0.4f);

        playerObject.transform.position = PinPoint[movePos].transform.position;
        
        playerAnim.SetBool("onDash",false);


        if(eventType != "Monster"){
            PopupManager.Instance.OpenLoading("isFadeIn");
        }

        else{
            if(eventType == "Monster"){
                PopupManager.Instance.OpenLoading("isVS");

            }
        }


        yield return new WaitForSeconds(2f);

        GameController.Instance.onChangeStage(eventType);
    }

    public void onOpenStage(){
        int currentPos = PlayerPrefs.GetInt("CurrentPlayerPos");
        for(int i =0; i<areaDataList[currentPos].openStage.Count; i++){
            buttonStageList[areaDataList[currentPos].openStage[i]].GetComponent<ButtonPositionEvent>().onOpenButton();

        }

        GameController.Instance.xOffset = areaDataList[currentPos].xOffset;
        GameController.Instance.yOffset = areaDataList[currentPos].yOffset;

    }

}
