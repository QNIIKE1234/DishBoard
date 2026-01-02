using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{

    public Animator loading;

    public string stage = "";

    public void Start(){
        SetData(stage);
        Destroy(this.gameObject,3f);
    }
    public void SetData(string _stage)
    {
        stage = _stage;
        if(stage != ""){
            loading.SetBool(stage,true);
        }


    }
}
