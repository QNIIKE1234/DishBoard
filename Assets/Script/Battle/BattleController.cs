using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState {FreeRoam, Battle, Dialog,BossFight}


public class BattleController : MonoBehaviour
{


    [SerializeField] BattleSystem battleSystem;
    GameState state;

    private void Start(){
        StartBattle();
    }

  

    void StartBattle(){
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        battleSystem.StartBattle();
        


    }

    void StartBattleBossFight(){
        state = GameState.BossFight;
        battleSystem.StartBattle();
    }

    void EndBattle(bool won){
        state = GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
  
    }

    private void Update(){
        // if(Input.GetKeyDown(KeyCode.Z)){
        //     if(state == GameState.Battle){
        //     }
        //     if(state == GameState.Dialog){
        //     }
        // }
        // if (state == GameState.Battle){

        // }
        // else if (state == GameState.Dialog){

        // }
      
    }
}
