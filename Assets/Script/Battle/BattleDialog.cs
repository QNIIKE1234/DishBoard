using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BattleDialog : MonoBehaviour
{
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] int letterPerSec;
    [SerializeField] Color highlightColor;
    [SerializeField] Color highlightColorTwo;
    [SerializeField] GameObject actionSelect;
    [SerializeField] GameObject moveSelect;
    [SerializeField] GameObject moveDetails;
    [SerializeField] GameObject P_MoveOutTXT;
    [SerializeField] GameObject E_MoveOutTXT;
    [SerializeField] List<TextMeshProUGUI> actionTexts;
    [SerializeField] List<TextMeshProUGUI> moveTexts;
    [SerializeField] List<Image> moveSprite;
    [SerializeField] List<GameObject> moveObject;
    [SerializeField] TextMeshProUGUI DetailsSkill;
    List<string> moveList = new List<string>();
    public TextMeshProUGUI PdialogMoveText;
    public TextMeshProUGUI EdialogMoveText;
    SkillDatabase.SkillCore skill;
    [SerializeField] GameObject movePosition;
    public GameObject selectMpvePos;
    public void SetDialog(string dialog){
        dialogText.text = dialog;
    }

    public IEnumerator TypeDialog(string dialog){
        
    dialogText.text="";
        foreach (var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f/letterPerSec);
        }
    }

    public void EnableDialogTxt(bool enabled){
        dialogText.enabled = enabled;
    }

    public void EnableActionSelector(bool enabled){
        actionSelect.SetActive(enabled);
    }

    public void EnableMoveSelector(bool enabled){
        moveSelect.SetActive(enabled);
        moveDetails.SetActive(enabled);
    }

    public void P_MoveOut(bool enabled){
        P_MoveOutTXT.SetActive(enabled);
    }

    public void E_MoveOut(bool enabled){
        E_MoveOutTXT.SetActive(enabled);
    }


    public void SKILL_TXT(string skill_player){
        PdialogMoveText.text = skill_player;
    }

    public void ESKILL_TXT(string skill_enemy){
        EdialogMoveText.text = skill_enemy;
    }

    public void UpdateActionSelecion(int selectAction){
    for(int i=0; i<actionTexts.Count; ++i){
        if(i == selectAction)
         actionTexts[i].color = highlightColor;
        else
         actionTexts[i].color = Color.black;
        }
    }   

    public void UpdateMoveSelection(int selectMove, Move move){
       for(int i=0; i<moveTexts.Count; ++i){
        if(i == selectMove)
         moveTexts[i].color = highlightColorTwo;
        else
         moveTexts[i].color = Color.white;
        }
    }
    public void onShowPosition(int _index){
        int playerPos = battleSystem.playerPosition;
        int movePosHL = 1;
        SkillDatabase.SkillCore skill = SkillDatabase.Instance.skillcore.Find(skill => skill.skillID == moveList[_index]);
        if(skill.SkillLength<10){
            int positionSkill = playerPos+skill.SkillLength+1;
            for(int i=0; i<positionSkill; i++){
                if(i>playerPos){
                    battleSystem.positionHightlight[i].gameObject.SetActive(true);
                    if(skill.moveSkill){
                        battleSystem.positionHightlight[playerPos-movePosHL].gameObject.SetActive(true);
                        movePosHL++;
                    }
                }

            }
        }
        else{
            battleSystem.positionHightlight[playerPos].gameObject.SetActive(true);
        }

    }
    public void onClosePosition(){
        foreach(Image target in battleSystem.positionHightlight){
             target.gameObject.SetActive(false);
        }

    }
    public void onShowDescription(int _index){

    }

    public void SetMoveName(List<Skill> _move, int _Level)
    {
        for (int i = 0; i<_move.Count; i++){
            SkillDatabase.SkillCore skill = SkillDatabase.Instance.skillcore.Find(skill => skill.skillID == _move[i].skillCode);
            moveList.Add(skill.skillID);
            if (skill != null)
            {
                if(_move[i].skillLevel<=_Level){
                    moveTexts[i].text = skill.skillName;
                    moveSprite[i].sprite = skill.skillIconPath;
                    moveObject[i].SetActive(true);

                }else{
                    moveTexts[i].text = skill.skillName;
                    moveSprite[i].sprite = skill.skillIconPath;
                    moveObject[i].SetActive(false);
                }

            }
            else
            {
                moveTexts[i].text = "-";
                // Optionally set a default sprite for moveSprite[i] if skill is not found
            }

        }

    }


    public void onChoosePosition(bool _setActive){
        movePosition.SetActive(_setActive);
        if(!_setActive){
            moveSelect.SetActive(true);
        }
    }

}


