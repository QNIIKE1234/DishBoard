using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleUnit : MonoBehaviour
{


    public float ElementDamage(string _skillElement){
        if(_skillElement == "Neutral"){
            if(element != "Ghost" ){
                return 1.0f;
            }else{
                return 0.5f;
            }
        }
        if(_skillElement == "Water"){
            if(element == "Fire" || element == "Undead"){
                return 2.0f;
            }else if (element == "Fire"){
                return 0.5f;
            }
            else if(element == "Water" ){
                return 0.25f;
            }
            else if (element == "Ghost"){
                return 1.25f;
            }
            else{
                return 1f;
            } 
        }
        if(_skillElement == "Fire"){
            if(element == "Earth" ||  element == "Undead"){
                return 2.0f;
            }
            else if (element == "Water"){
                return 0.5f;
            }
            else if(element == "Fire" ){
                return 0.25f;
            }
            else if (element == "Ghost"){
                return 1.25f;
            }
            else{
                return 1f;
            }             
        }
        if(_skillElement == "Earth"){
            if(element == "Wind"){
                return 2.0f;
            }else if (element == "Water"){
                return 0.5f;
            }
            else if(element == "Earth" ){
                return 0.25f;
            }
            else if (element == "Ghost"){
                return 1.25f;
            }
            else{
                return 1f;
            }             
        }
        if(_skillElement == "Wind"){
            if(element == "Water"){
                return 2.0f;
            }else if (element == "Earth"){
                return 0.5f;
            }
            else if(element == "Wind" ){
                return 0.25f;
            }
            else if (element == "Ghost"){
                return 1.25f;
            }
            else{
                return 1f;
            }       
        }
        if(_skillElement == "Poison"){
            
            if(element == "Water"||element == "Fire"||element == "Earth"||element == "Wind"){
                return 1.75f;
            }else if (element == "Holy" || element == "Shadow" || element == "Undead"){
                return 0.5f;
            }
            else if(element == "Poison" ){
                return 0.25f;
            }
            else{
                return 1f;
            }       
        }
        if(_skillElement == "Holy"){
            if(element == "Shadow" || element == "Undead"|| element == "Poison" || element == "Ghost"){
                return 2.0f;
            }
            else if(element == "Holy" ){
                return 0.25f;
            }
            else{
                return 1f;
            }        
        }
        if(_skillElement == "Shadow"){
            if(element == "Holy" || element == "Poision"|| element == "Undead"|| element == "Ghost"){
                return 2.0f;
            }
            else if(element == "Shadow" ){
                return 0.25f;
            }
            else{
                return 1f;
            }        
        }
        if(_skillElement == "Ghost"){
            if(element == "Neutral"){
                return 2.0f;
            }
            else if (element == "Holy" || element == "Shadow" || element == "Undead"){
                return 0.5f;
            }
            else if(element == "Ghost" ){
                return 0.25f;
            }
            else{
                return 1.25f;
            }      
        }
        if(_skillElement == "Undead"){
            if(element == "Neutral"||element == "Poison"){
                return 2.0f;
            }
            else if (element == "Fire" || element == "Water" || element == "Holy"|| element == "Shadow"|| element == "Ghost"){
                return 0.5f;
            }
            else{
                return 1f;
            }               
        }

        return 1;
    }

}
