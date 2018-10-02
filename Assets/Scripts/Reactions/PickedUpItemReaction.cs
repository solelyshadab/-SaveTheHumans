using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickedUpItemReaction : MonoBehaviour {

	public Item item;
	private Inventory inventory;

	protected void SpecificInt(){
		inventory = FindObjectOfType<Inventory> ();
	}

	protected void ImmediateReaction(){
	
		inventory.AddItem (item);
	}
}
