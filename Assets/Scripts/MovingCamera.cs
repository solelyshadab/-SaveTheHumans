using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour {

	public Camera MainCamera;
	private Camera MoveCamera;
	//public GameObject player;

	// Use this for initialization
	void Start () {
		MoveCamera = FindObjectOfType<Camera> ();
		MainCamera.enabled = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 playerposition = player.transform.position;

		//transform.RotateAround(playerposition, Vector3.right, (float) 1);
		
	}

	public void showMovingCamera(){
		MainCamera.enabled = false;
		MoveCamera.enabled = true;
	}

	public void showMainCamera(){
		MainCamera.enabled = true;
		MoveCamera.enabled = false;
	}
}
