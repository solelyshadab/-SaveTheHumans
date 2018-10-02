using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class CameraController : MonoBehaviour {

	public GameObject player;

	//private Vector3 offset;
	private Vector3 offsetRotation;


	public Transform lookAt;

	private Vector3 desiredPosition;
	private float offset = 1.5f;
	private float distance = 3.5f;

	void Start ()
	{
		//offset = transform.position - player.transform.position;


	}

	void LateUpdate ()
	{
		//transform.position = player.transform.position + offset;

//		transform.Translate(Vector3.right * Time.deltaTime);
		//transform.RotateAround(Vector3.zero, Vector3.left, (float) 0.1);
		//transform.LookAt (player.transform);

		//Update position
		desiredPosition = lookAt.position + (- transform.forward * distance) + (transform.up * offset);
		transform.position = Vector3.Lerp (transform.position, desiredPosition, 0.05f);

		//Update rotation
		transform.LookAt(lookAt.position + (Vector3.back * 1.5f));

	}


}

