using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using TMPro;

public class DialogManager : MonoBehaviour
{
  [SerializeField] GameObject dialogBox;
  [SerializeField] TextMeshProUGUI dialogText;
  [SerializeField] int lettersPersecond;

  public event Action OnShowDialog;
  public event Action OnCloseDialog;


  public static DialogManager Instance {get ; private set;}
  
  private void Awake(){
      Instance = this;
  }
  
  Dialog dialog;
  int currenctLine = 0;
  bool isTyping;

  public IEnumerator ShowDialog(Dialog dialog){
      yield return new WaitForEndOfFrame();

      OnShowDialog?.Invoke();

      this.dialog = dialog;
      dialogBox.SetActive(true);
     StartCoroutine(TyeDialog(dialog.Lines[0]));

  }


  public void HandleUpdate(){
      if(Input.GetKeyDown(KeyCode.Z) && !isTyping){

          ++currenctLine;
          if(currenctLine<dialog.Lines.Count){
               StartCoroutine(TyeDialog(dialog.Lines[currenctLine]));
          }
          else{
              currenctLine = 0;
              dialogBox.SetActive(false);
              OnCloseDialog?.Invoke();
          }

      }
  }

  public IEnumerator TyeDialog(string dialog){
      isTyping =true;
      dialogText.text = " ";
      foreach (var letter in dialog.ToCharArray())
      {
          dialogText.text += letter;
          yield return new WaitForSeconds(1f/lettersPersecond);
      }
    isTyping=false;
  }
}
