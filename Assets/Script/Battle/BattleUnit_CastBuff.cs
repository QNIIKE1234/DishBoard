using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class BattleUnit : MonoBehaviour
{
    public IEnumerator onSetBuff(SkillDatabase.SkillCore _move){

        BuffSkill isAlreadyBuff = buffSkillList.Find(skill => skill.skillbuff == _move.skillName);
        if(isAlreadyBuff!=null){
            isAlreadyBuff.skillDuration ++;
        }
        else
        {
            for(int i=0; i<_move.buffSkill.Count; i++){
                if(_move.buffSkill[i].skillbuff == "AttackUP"){
                    Attack = Attack+((Attack*_move.buffSkill[i].skillValue)/100);

                }
                else if(_move.buffSkill[i].skillbuff == "Heal"){
                    int heal = ((MaxHP*_move.buffSkill[i].skillValue)/100);
                    HP += heal;
                    if(HP>MaxHP){
                        HP = MaxHP;
                    }
                    StartCoroutine(SetTextDamage(new List<string> { ""+heal }));
                    battleSystem.UpdateHP(isPlayerUnit);
                }

                if(_move.buffSkill[i].skillbuff != "Heal"){
                    BuffSkill newBuff = new BuffSkill();
                    newBuff.skillbuff = _move.skillName;
                    newBuff.skillDuration = _move.buffSkill[i].skillDuration;
                    newBuff.skillValue = _move.buffSkill[i].skillValue;
                    newBuff.isBuff = true;
                    buffSkillList.Add(newBuff);
                    AddBuffIcon();
                }

                yield return new WaitForSeconds(0.2f);
            }
        }

    }


    public IEnumerator onSetDeBuff(SkillDatabase.SkillCore _Debuff,bool _isPlayer){
        string debuffName="";
        for(int i =0; i<_Debuff.debuffSkill.Count; i++){
            BuffSkill isAlreadyBuff = buffSkillList.Find(skill => skill.skillbuff == _Debuff.debuffSkill[i].skillbuff) ?? null;

            if(isAlreadyBuff ==null || _Debuff.debuffSkill[i].skillDuration>isAlreadyBuff.skillDuration ){
                if(_Debuff.debuffSkill[i].skillbuff == "KnockBack"){
                    if(_isPlayer){
                        if(battleSystem.playerPosition<battleSystem.enemyPosition){
                            battleSystem.playerPosition = battleSystem.playerPosition - _Debuff.debuffSkill[i].skillDuration;
                        }else{
                            battleSystem.playerPosition = battleSystem.playerPosition + _Debuff.debuffSkill[i].skillDuration;
                        }
                        battleSystem.UpdatePosition(true,battleSystem.playerPosition);
                        
                    }else{
                        if(battleSystem.playerPosition<battleSystem.enemyPosition){
                            battleSystem.enemyPosition = battleSystem.enemyPosition + _Debuff.debuffSkill[i].skillDuration;
                        }else{
                            battleSystem.enemyPosition = battleSystem.enemyPosition - _Debuff.debuffSkill[i].skillDuration;
                        }
                        battleSystem.UpdatePosition(false,battleSystem.enemyPosition);
                    } 
                }

                if(_Debuff.debuffSkill[i].skillbuff != "KnockBack"){
                    if(isAlreadyBuff!=null){
                        isAlreadyBuff.skillValue = _Debuff.debuffSkill[i].skillValue;

                    }else{
                        BuffSkill newDeBuff = new BuffSkill();
                        newDeBuff.skillbuff = _Debuff.debuffSkill[i].skillbuff;
                        newDeBuff.skillDuration = _Debuff.debuffSkill[i].skillDuration;
                        newDeBuff.skillValue = _Debuff.debuffSkill[i].skillValue;
                        newDeBuff.isBuff = false;
                        buffSkillList.Add(newDeBuff);
                        AddBuffIcon();
                    }

                    
                }
            }
        }

        yield return new WaitForSeconds(0.5f);
    }

    public void AddBuffIcon(){
        foreach(GameObject tartget in buffIconGameObject){
            Destroy(tartget);
        }
        buffIconGameObject.Clear();

        foreach(BuffSkill target in buffSkillList){
            GameObject _buffIcon = Instantiate(buffIcon,buffIconContext);
            SkillDescription buffDes =  _buffIcon.GetComponent<SkillDescription>();
            buffDes.SetData(target.isBuff,target.skillbuff,target.skillDuration);
            buffIconGameObject.Add(_buffIcon);
        }

    }
}
