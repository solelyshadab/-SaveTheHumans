using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDownWall : MonoBehaviour {

	private PlayerController playerController;
	// Use this for initialization
	void Start(){
		playerController = FindObjectOfType<PlayerController>();
	}


	void OnTriggerEnter(Collider other) 
	{
		Debug.Log ("PlayerBack Wall collided!");
		if (other.gameObject.CompareTag ("RocketItem"))
		{
			Debug.Log ("Back Wall touched RocketItem");
			other.gameObject.SetActive (false);
			playerController.removeUnTouchedItemsAndPutBackForThrow (other);

		}
	}
}
