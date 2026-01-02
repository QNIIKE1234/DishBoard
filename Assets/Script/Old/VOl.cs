using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VOl : MonoBehaviour
{
    [SerializeField] Slider volumSlider;
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume")){
            PlayerPrefs.SetFloat("musicVolume",1);
            Load();
        }
        else{
            Load();
        }
    }

public void ChangeVol(){
    AudioListener.volume = volumSlider.value;
    Save();
}

private void Load(){
    volumSlider.value = PlayerPrefs.GetFloat("musicVolume");

}

private void Save(){
     PlayerPrefs.SetFloat("musicVolume",volumSlider.value);
}
   
  
}
