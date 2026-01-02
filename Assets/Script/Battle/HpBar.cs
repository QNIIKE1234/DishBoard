using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{

    public Slider HealthBar;
    public Slider ManathBar;
    public Slider EXPbar;

    public void SetHP(float hpNormalized,bool isPlayer = false){
        if(isPlayer){
            HealthBar.maxValue = UserStatus.Instance.MAXHP;
        }else{
            HealthBar.maxValue = hpNormalized;
        }

        HealthBar.value = hpNormalized;
        SetUpEXP();
    }
    public void SetMP(float mpNormalized,bool isPlayer = false) {
        if(isPlayer){
            ManathBar.maxValue = UserStatus.Instance.MAXMP;
        }else{
            ManathBar.maxValue = mpNormalized;
        }
        ManathBar.value = mpNormalized;
    }
    public IEnumerator SetHPSmooth(float newHp){
        float elapsedTime = 0f;
        float duration = 0.5f; // duration for the smooth transition
        float startingHp = HealthBar.value;

        while (elapsedTime < duration)
        {
            HealthBar.value = Mathf.Lerp(startingHp, newHp, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // wait until the next frame
        }

        HealthBar.value = newHp;
        if(newHp == 0){
            HealthBar.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(0.25f);
    }

    public IEnumerator SetMPSmooth(float newMp){
        float elapsedTime = 0f;
        float duration = 0.5f; 
        float startingHp = ManathBar.value;

        while (elapsedTime < duration)
        {
            ManathBar.value = Mathf.Lerp(startingHp, newMp, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        ManathBar.value = newMp;
        if(newMp == 0){
            ManathBar.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(0.25f);

    }

    public void SetUpEXP(){
        EXPbar.maxValue = UserStatus.Instance.ExpLength[UserStatus.Instance.PLevel];
        EXPbar.value = UserStatus.Instance.EXP;
    }
    public IEnumerator SetLevelEXP(){
        EXPbar.maxValue = UserStatus.Instance.ExpLength[UserStatus.Instance.PLevel];
        EXPbar.value = UserStatus.Instance.EXP;
        yield return null;
    }
    public IEnumerator SetEXPSmooth(float curEXP){
        Debug.Log("EXP = "+curEXP);
        float elapsedTime = 0f;
        float duration = 0.5f; 
        float startingHp = EXPbar.value;

        while (elapsedTime < duration)
        {
            EXPbar.value = Mathf.Lerp(startingHp, curEXP, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        if(EXPbar.maxValue<=EXPbar.value){
            UserStatus.Instance.onPLevelUp(UserStatus.Instance.PLevel+1);
            // int previousLVL = UserStatus.Instance.EXP;
            // EXPbar.value = 0f;
            // UserStatus.Instance.PLevel += 1;
            // UserStatus.Instance.EXP -= previousLVL;
            // SetUpEXP();
        }

        EXPbar.value = curEXP;
        if(curEXP == 0){
            EXPbar.gameObject.SetActive(false);
        }else{
            EXPbar.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(0.25f);

    }
}
