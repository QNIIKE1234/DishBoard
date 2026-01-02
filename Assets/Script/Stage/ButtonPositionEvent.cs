using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System.Linq;
public class ButtonPositionEvent : MonoBehaviour
{
    public string eventType;
    public List<Button> eventImage = new List<Button>();
    public List<Sprite> ImageButton = new List<Sprite>();
    public Button Poss;

    public Image Background;
    UnityAction<int> onSetPlayerMove;

    public int pos;
    public int stageIndex;
    int current;

    int EventType;
    int maxIndexStage;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDataEvent(string _type,int _pos,UnityAction<int>_onSetPlayerMove,int _stageIndex){
        current = UserStatus.Instance.currentIndex;
        pos=_pos;
        eventType = _type;
        onSetPlayerMove = _onSetPlayerMove;
        stageIndex=_stageIndex;

        maxIndexStage = UserStatus.Instance.maxIndex;

        if(eventType=="Portal"){
            eventImage[0].gameObject.SetActive(true);
            EventType = 0;
        }else if(eventType=="NextPortal"){
            eventImage[0].gameObject.SetActive(true);
            EventType = 0;
        }else if(eventType=="Monster"){
            eventImage[1].gameObject.SetActive(true);
            EventType = 1;
        }else if(eventType=="Market"){
            eventImage[2].gameObject.SetActive(true);
            EventType = 2;
        }else if(eventType=="Boss"){
            eventImage[3].gameObject.SetActive(true);
            EventType = 3;
        }
        else if(eventType=="MiniGame"){
            eventImage[4].gameObject.SetActive(true);
            EventType = 4;
        }
        onCheckMaxStageIndex();   

    }

    public void onClick(){
       UserStatus.Instance.currentIndex = stageIndex;
        // PlayerPrefs.SetInt("MaxStageIndex",stageIndex);
        onCheckMaxStageIndex();
        PlayerPrefs.SetString("EventType",eventType);
       UserStatus.Instance.maxIndex = stageIndex+1;
        // PlayerPrefs.SetInt("StageIndex",stageIndex+1);
        onSetPlayerMove.Invoke(pos);

    }

    // public void onOpenButton(){
    //     if(stageIndex==null){
    //         PlayerPrefs.SetInt("StageIndex",1);
    //         current = PlayerPrefs.GetInt("StageIndex");
    //     }

    //     if((maxIndexStage>=stageIndex)||stageIndex<=1){
    //         Background.sprite = ImageButton[1];
    //         Poss.interactable=true;
    //         eventImage[EventType].interactable  = true;     
    //     }

    // }

    public void onOpenButton(){
        Background.sprite = ImageButton[1];
        Poss.interactable=true;
        eventImage[EventType].interactable  = true; 
    }
    public void onCheckMaxStageIndex(){
        if(current>=maxIndexStage){
           UserStatus.Instance.maxIndex = current;
            // PlayerPrefs.SetInt("MaxStageIndex",current);
        }else{

        }
        
    }
}
