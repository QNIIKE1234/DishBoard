using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class W_TEXT : MonoBehaviour
{
    private Text UI_TXT;
    private string TEXT_TO_WRITE;
    private float timePerCH;
    private float timer;
    private int ChaIn;
    // Update is called once per frame
    void Update()
    {
            if(UI_TXT != null){
                timer -= Time.deltaTime;
                if(timer <= 0f ){
                    UI_TXT.text = TEXT_TO_WRITE;
                    timer += timePerCH;
                    ChaIn++;
                    UI_TXT.text = TEXT_TO_WRITE.Substring(0,ChaIn);
                    if(ChaIn >= TEXT_TO_WRITE.Length){
                        UI_TXT = null;
                    }
                }   
        }
    }
 public void addWriter(Text UI_TXT, string TEXT_TO_WRITE, float timePerCH){
     this.UI_TXT = UI_TXT;
     this.TEXT_TO_WRITE = TEXT_TO_WRITE;
     this.timePerCH = timePerCH;
 }
}