using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory1 : MonoBehaviour {
	public Image[] itemImages1 = new Image[numItemSlots];
	public Item[] items1 = new Item[numItemSlots];

	public static int numItemSlots = 6;

	public void AddItem(Item itemToAdd) {

		for(int i = 0 ; i < items1.Length; i++){

			if(items1[i] == null){
				items1 [i] = itemToAdd;
				itemImages1 [i].sprite = itemToAdd.sprite;
				itemImages1 [i].enabled = true;
				return;
			}

		}
	}

	public void RemoveItem(Item itemToRemove) {

		for(int i = 0 ; i < items1.Length; i++){

			if(items1[i] == itemToRemove){
				items1 [i] = null;
				itemImages1 [i].sprite = null;
				itemImages1 [i].enabled = false;
				return;
			}

		}
	}

}
