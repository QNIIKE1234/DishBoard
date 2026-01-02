using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class Player_Control : MonoBehaviour
{

    public float speed ,GX,sx;
    public LayerMask solidLayer;
    public LayerMask interactableLayer;
    public LayerMask MONFLayer;
    private bool isMoving;
    private Vector2 input;
    public event Action OnEncountered;
    [SerializeField] Animator an;
    public GameObject LOAD;
 
    public GameObject MENU;
    public bool Menusta ;
 

    void Start(){
        sx = transform.localScale.x;
        an = gameObject.GetComponent<Animator>();
    }

   



    public void HandleUpdate()
    {   
        
        GX = Input.GetAxis("Horizontal");
        if(Input.GetKeyDown(KeyCode.X)){
            Debug.Log("MI5555555555"+Menusta);
            MENUBAR();
        }
            

        if(!isMoving){
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
                    
                        if(input.x != 0) input.y=0;

                        if (input != Vector2.zero){
                         
                            var targetPos = transform.position;
                            targetPos.x += input.x;
                            targetPos.y += input.y;

                        if(IsWalkable(targetPos))
                        StartCoroutine(Move(targetPos));
            }
        }

        if(GX < 0)
        {
            transform.localScale = new Vector3(sx, transform.localScale.y, transform.localScale.z);
        }
        if(GX > 0)
        {
            transform.localScale = new Vector3(-sx, transform.localScale.y, transform.localScale.z);
        }

        if(Input.GetKeyDown(KeyCode.Z))
            Interact();
    }

    void Interact(){
        Debug.Log("MIIIIIIIIIIIIIIIIIIIIII");
       var collider = Physics2D.OverlapCircle(transform.position,1f,interactableLayer);

       if(collider != null){
           Debug.Log("Whyyyyyyyyyyyyyyyyyyy");
           collider.GetComponent<Interactable>()?.Interact();
       }

    }

    void MENUBAR(){

        if(Menusta==false){
                 MENU.SetActive(true);
                 Menusta = true;
            }
        else{
            MENU.SetActive(false);
                 Menusta = false;
        }
      
    }


    IEnumerator Move(Vector3 targetPos){


        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed*Time.deltaTime);
         yield return null;
        }
        transform.position = targetPos;

        isMoving = false;

        CheckForEncounters();
        
    }


    private bool IsWalkable(Vector3 targetPos){

        if(Physics2D.OverlapCircle(targetPos,0.2f,solidLayer | interactableLayer) != null){

            return false;

        }
        return true;
    }

 
    private void CheckForEncounters () {
       if(Physics2D.OverlapCircle(transform.position,0.2f,MONFLayer) != null){
           if(UnityEngine.Random.Range(1,101) <=5){
               AudioSource audio = GetComponent<AudioSource>();
               audio.Play();
               
               Debug.Log("Founddddddddddddddddddddddddddddd");
               isMoving = true;
               
               StartCoroutine(LoadingF());
               
           }

       }

    }

     IEnumerator LoadingF(){ 
        LOAD.SetActive(true);
         yield return new WaitForSeconds(3f);
         LOAD.SetActive(false);
        isMoving = false;
         OnEncountered();
     }
}
