using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SelectCharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OnCreateCharacter());
    }

    IEnumerator OnCreateCharacter()
    {
        yield return new WaitForSeconds(1f);

        // GameObject _PLAYER = Instantiate(playerObject,contentStage);
        // playerObject = _PLAYER;
        // playerAnim = _PLAYER.GetComponent<Animator>();
        // player = _PLAYER.GetComponent<CharacterAnimationController>();
        // _PLAYER.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        // _PLAYER.transform.position = PinPoint[PlayerData.Instance.currentIndex].transform.position;
        // player.UpdateAnimClass();
        // player.OnUpDateEquipment();

        // GameController.Instance.playerObject = _PLAYER;
    }

}
