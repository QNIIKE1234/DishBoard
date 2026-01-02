using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void PreviousScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("PreviousScene"));
    }
    public IEnumerator onDelay(float time){
        yield return new WaitForSeconds(time);
    }
}
