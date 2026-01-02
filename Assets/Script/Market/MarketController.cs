using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
public class MarketController : MonoBehaviour
{
    [SerializeField] SceneChanger sceneChanger;

    public GameObject marketThumbnails;

    public Transform content;

	public List<MarketArea> marketAreaList = new List<MarketArea>();

	[System.Serializable]
	public class MarketArea
	{
		public int area_num;
	    public List<MarketItem> marketItemList = new List<MarketItem>();


	}  

	[System.Serializable]
	public class MarketItem
	{
    	public string item_code;
        public string item_price;


	}   
    void Start()
    {
        CreateMarketItem();
    }

    public void CreateMarketItem(){
        int index = 0;
        foreach(MarketItem target in marketAreaList[0].marketItemList){
            GameObject MarketItem = Instantiate(marketThumbnails,content);
            MarketItemPrefabs item = MarketItem.GetComponent<MarketItemPrefabs>();
            item.SetData(target.item_code,target.item_price);
            index ++;
        }

        // playerAnim = _PLAYER.GetComponent<Animator>();
        // player = _PLAYER.GetComponent<CharacterAnimationController>();
        // _PLAYER.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        // _PLAYER.transform.position = PinPoint[currentPlayerPos].transform.position;
        // player.UpdateAnimClass();
        // player.OnUpDateEquipment();
    }

    public void onQuit(){
        sceneChanger.ChangeScene("Dungeon");
    }
}
