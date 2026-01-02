using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public enum GameState {FreeRoam, Battle, Dialog,POPEFight}


public class GameControll : MonoBehaviour
{

   
    [SerializeField] Player_Control playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;
    [SerializeField] BattleSystem POPE;
    public Animator Found;
    GameState state;
    public GameObject LOAD_main;

    private void Start(){
        // playerController.OnEncountered += StartBattle;
        // battleSystem.OnBattleOver +=EndBattle;

        // DialogManager.Instance.OnShowDialog += () =>{
        
        //      state = GameState.Dialog;

        // };
        // DialogManager.Instance.OnCloseDialog += () =>{

        //     if(state == GameState.Dialog)
        //      state = GameState.FreeRoam;
          

        // };

        StartBattle();
    }

  

    void StartBattle(){
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);
        playerController.gameObject.SetActive(false);

        battleSystem.StartBattle();
        


    }

    // void StartBattlePOPE(){
    //     Debug.Log("Battle POPE START");
    //     state = GameState.POPEFight;
    //     POPE.gameObject.SetActive(true);
    //     worldCamera.gameObject.SetActive(false);
    //     playerController.gameObject.SetActive(false);

    //     battleSystem.StartBattle();
    // }

    void EndBattle(bool won){
        state = GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
        playerController.gameObject.SetActive(true);
  
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Z)){
            if(state == GameState.Battle){
                AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            }
            if(state == GameState.Dialog){
                AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            }
        }
        if(state == GameState.FreeRoam){
            playerController.HandleUpdate();
        }
        else if (state == GameState.Battle){
        }
        else if (state == GameState.Dialog){
            DialogManager.Instance.HandleUpdate();
        }
      
    }
    
}
