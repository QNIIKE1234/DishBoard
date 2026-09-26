using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.Events;

public class ResultPanel : MonoBehaviour
{

    public TextMeshProUGUI EXPText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI coinsText;

    public GameObject itemPrefabs;
    public Transform content;

    UnityAction onclick;
    void Start()
    {
        
    }

    // Update is called once per frame
    public async Task SetData(UnityAction _function,int _EXP, int _coins)
    {
        onclick = _function;
        EXPText.text = "EXP : "+_EXP;
        coinsText.text = "COIN : "+_coins;

        bool result = await UserStatus.Instance.useCase.SendDataToFirestore(UserStatus.Instance.UserId);
        if (result)
        {
            Debug.Log("Data sent successfully.");
        }
        else
        {
            Debug.Log("Failed to send data.");
        }

    }

    private bool isClosed = false;

    public void OnClose()
    {
        if (isClosed) return;
        isClosed = true;

        Button[] buttons = GetComponentsInChildren<Button>();
        foreach (var btn in buttons)
        {
            if (btn != null) btn.interactable = false;
        }

        try
        {
            if (onclick != null)
            {
                if (onclick.Target is UnityEngine.Object unityObj && unityObj == null)
                {
                    Debug.LogWarning("[ResultPanel] Target object has been destroyed. Skipping callback.");
                }
                else
                {
                    onclick.Invoke();
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning($"[ResultPanel] Exception invoking OnClose callback: {ex.Message}");
        }

        Destroy(this.gameObject);
    }
}
