using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 

public class SwipeControl : MonoBehaviour {

	Vector3 startPos;
	Vector3 endPos;
	float swipeDistance;
	float swipeFactor;
	private bool upSwipe, downSwipe, rightSwipe, leftSwipe, returnPlayer, returnCamera;
	//public Text swipeText;
	public Transform player;
	public Transform mainCamera;

	public Transform rightMiddleCamera;
	public Transform leftMiddleCamera;
	public Transform upMiddleCamera;
	public Transform downMiddleCamera;


	public Transform rightMiddlePlayer;
	public Transform leftMiddlePlayer;
	public Transform upMiddlePlayer;
	public Transform downMiddlePlayer;


	private Vector3 startPositionCamera;
	private Quaternion startRotationCamera;

	private Vector3 startPositionPlayer;
	private Quaternion startRotationPlayer;

	private int horizontalPosindex;
	private int verticalPosindex;

	// Use this for initialization
	void Start () {
		upSwipe = downSwipe = rightSwipe = leftSwipe = true;
		returnPlayer = returnCamera = false;
		startPositionCamera = mainCamera.localPosition;
		startRotationCamera = mainCamera.localRotation;
		startPositionPlayer = player.localPosition;
		startRotationPlayer = player.localRotation;
		horizontalPosindex = 0;
		verticalPosindex = 0;

	}
	
	// Update is called once per frame
	void Update () {
		
		//Debug.Log ("posindex: " + horizontalPosindex);
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Began) {
				startPos = touch.position;
			} 
			else if (touch.phase == TouchPhase.Ended) {
				endPos = touch.position;
				swipeDistance = (endPos - startPos).magnitude;
				swipeFactor = swipeDistance / (Screen.width / 2);
				if (swipeFactor > 1)
					swipeFactor = 1;
				Swipe ();
			}
		}

		if (returnPlayer && returnCamera) {
			//if (returnPlayer) {
			BringtoStartPos ();
		}

		if ((player.localPosition-startPositionPlayer).magnitude <=0.05f && Quaternion.Angle(mainCamera.localRotation, startRotationCamera) <=0.05f && !(horizontalPosindex == 0)) {		
			//if ((player.localPosition-startPositionPlayer).magnitude <=0.05f && !(horizontalPosindex == 0)) {
				horizontalPosindex = 0;

		}
		if ((player.localPosition-startPositionPlayer).magnitude <=0.05f && Quaternion.Angle(mainCamera.localRotation, startRotationCamera) <=0.05f && !(verticalPosindex == 0)) {
			//if ((player.localPosition-startPositionPlayer).magnitude <=0.05f && !(verticalPosindex == 0)) {
					verticalPosindex = 0;
		}

	}

	private void Reset(){
		startPos = endPos = Vector3.zero;
		swipeDistance = 0;

	}

	private void BringtoStartPos(){
		Vector3 desiredposPlayer = startPositionPlayer;
		Quaternion desiredRotPlayer = startRotationPlayer;
		Vector3 desiredposCamera = startPositionCamera;
		Quaternion desiredRotCamera = startRotationCamera;
		StartCoroutine (MoveCameratoStartpos(desiredposCamera, desiredRotCamera));
		StartCoroutine (MovePlayertoStartpos(desiredposPlayer, desiredRotPlayer));
	}

	private void Swipe(){		// Should be of type IEnumerator
		Vector2 distance = endPos - startPos;
		if (Mathf.Abs (distance.x) > Mathf.Abs (distance.y)) {
			Debug.Log ("Horiontal Swipe");
			if (distance.x > 0 ) {
				Debug.Log ("Right Swipe");
				//swipeText.text = "Right Swipe";

				if (horizontalPosindex == 0 && verticalPosindex == 0) {
					Vector3 desiredposPlayer = rightMiddlePlayer.localPosition;
					//desiredposPlayer.x = desiredposPlayer.x * swipeFactor;
					Quaternion desiredRotPlayer = rightMiddlePlayer.localRotation;
					Vector3 desiredposCamera = rightMiddleCamera.localPosition;
					Quaternion desiredRotCamera = rightMiddleCamera.localRotation;
					StartCoroutine (MoveCamera(desiredposCamera, desiredRotCamera));
					StartCoroutine (MovePlayer(desiredposPlayer, desiredRotPlayer));
					horizontalPosindex += 1;
					Reset ();
				} 
			else {
					Debug.Log ("Swipe already on!");
						}

			}
			else if (distance.x < 0) {
				Debug.Log ("Left Swipe");
				//swipeText.text = "Left Swipe";
				if(horizontalPosindex == 0 && verticalPosindex == 0){
					Vector3 desiredposPlayer = leftMiddlePlayer.localPosition;
					//desiredposPlayer.x = desiredposPlayer.x * swipeFactor;
					Quaternion desiredRotPlayer = leftMiddlePlayer.localRotation;
					Vector3 desiredposCamera = leftMiddleCamera.localPosition;
					Quaternion desiredRotCamera = leftMiddleCamera.localRotation;
					StartCoroutine (MoveCamera(desiredposCamera, desiredRotCamera));
					StartCoroutine (MovePlayer(desiredposPlayer, desiredRotPlayer));
					horizontalPosindex -= 1;
					Reset ();
				}
				else {
					Debug.Log ("Swipe already on!");
					}

				}
		}

			else if (Mathf.Abs (distance.x) < Mathf.Abs (distance.y)) {
				Debug.Log ("Vertical Swipe");
			if (distance.y > 0) {
				Debug.Log ("Up Swipe");
				//swipeText.text = "Up Swipe";

				if (verticalPosindex == 0 && horizontalPosindex == 0) {
					Vector3 desiredposPlayer = upMiddlePlayer.localPosition;
					//desiredposPlayer.y = desiredposPlayer.y * swipeFactor;
					//desiredposPlayer.z = desiredposPlayer.z * swipeFactor;
					Quaternion desiredRotPlayer = upMiddlePlayer.localRotation;
					Vector3 desiredposCamera = upMiddleCamera.localPosition;
					Quaternion desiredRotCamera = upMiddleCamera.localRotation;
					StartCoroutine (MoveCamera(desiredposCamera, desiredRotCamera));				
					StartCoroutine (MovePlayer(desiredposPlayer, desiredRotPlayer));
					verticalPosindex += 1;
					Reset ();
				} 
				else {
					Debug.Log ("Swipe already on!");
				}


			}
			else if (distance.y < 0) {
				Debug.Log ("Down Swipe");
				//swipeText.text = "Down Swipe";
				if (verticalPosindex == 0 && horizontalPosindex == 0) {
					Vector3 desiredposPlayer = downMiddlePlayer.localPosition;
					//desiredposPlayer.y = desiredposPlayer.y * swipeFactor;
					//desiredposPlayer.z = desiredposPlayer.z * swipeFactor;
					Quaternion desiredRotPlayer = downMiddlePlayer.localRotation;
					Vector3 desiredposCamera = downMiddleCamera.localPosition;
					Quaternion desiredRotCamera = downMiddleCamera.localRotation;
					StartCoroutine (MoveCamera(desiredposCamera, desiredRotCamera));
					StartCoroutine (MovePlayer(desiredposPlayer, desiredRotPlayer));
					verticalPosindex -= 1;
					Reset ();
				} 
				else {
					Debug.Log ("Swipe already on!");
				}

		}
	}
	}

	private IEnumerator MoveCamera(Vector3 desiredposCamera, Quaternion desiredRotCamera)
	{

		var t = 0f;

		while(true)
			{
			//Debug.Log ("initpos camera: "+ mainCamera.localPosition);
			//Debug.Log ("desiredpos camera: "+ desiredposCamera);
			float dist = Quaternion.Angle(desiredRotCamera , mainCamera.localRotation);
			//Debug.Log ("Distance magnitude for camera is: " + dist);
			if (dist<=0.05f) {
				returnCamera = true;
				Debug.Log ("return camera now");
				yield break;
			}

			//Debug.Log("Camera co routine is on!");
			t += Time.deltaTime;
			//mainCamera.localPosition = Vector3.Lerp (mainCamera.localPosition, desiredposCamera,0.1f * t);
			mainCamera.localRotation = Quaternion.Lerp (mainCamera.localRotation, desiredRotCamera, 0.5f * t);
				yield return null;
			//Debug.Log ("HorizontalposIndex is: " + horizontalPosindex);

			}

	}

	private IEnumerator MovePlayer(Vector3 desiredpos, Quaternion desiredRot)
	{
		var t = 0f;

		while(true)
		{
			float dist = (desiredpos - player.localPosition).magnitude;
			//Debug.Log ("Distance magnitude for player is: " + dist);	
			if (dist<=0.05f) {
				returnPlayer = true;
				Debug.Log ("Return player now.");
				yield break;
			}
			//Debug.Log("Player co routine is on!");
			t += Time.deltaTime;
			player.localPosition = Vector3.Lerp (player.localPosition, desiredpos, 0.1f * t);
			player.localRotation = Quaternion.Lerp (player.localRotation, desiredRot, 0.5f * t);
			//Debug.Log ("Value of t is: " + t);
			yield return null;
			//Debug.Log ("VerticalposIndex is: " + verticalPosindex);
		}

	}



	public bool UpSwipe {get { return upSwipe;} }
	public bool DownSwipe {get { return downSwipe;} }
	public bool RightSwipe {get { return rightSwipe;} }
	public bool LeftSwipe {get { return leftSwipe;} }


	private IEnumerator MoveCameratoStartpos(Vector3 desiredposCamera, Quaternion desiredRotCamera)
	{

		//var t = 0f;

		while(true)
		{
			
			float dist = Quaternion.Angle(desiredRotCamera , mainCamera.localRotation);
			//Debug.Log ("return camera:Distance magnitude for camera is: " + dist);
			if (dist<=0.05f) {
				returnCamera = false;
				yield break;
			}

			//t += Time.deltaTime;
			//Debug.Log("return camera:Camera co routine is on!");

			//mainCamera.localPosition = Vector3.Lerp (mainCamera.localPosition, desiredposCamera, 0.5f * Time.deltaTime);
			mainCamera.localRotation = Quaternion.Lerp (mainCamera.localRotation, desiredRotCamera, 0.5f * Time.deltaTime);	
			yield return null;
			//Debug.Log ("HorizontalposIndex is: " + horizontalPosindex);

		}

	}

	private IEnumerator MovePlayertoStartpos(Vector3 desiredpos, Quaternion desiredRot)
	{
		//var t = 0f;

		while(true)
		{
			float dist = (desiredpos - player.localPosition).magnitude;
			//Debug.Log ("return player: Distance magnitude for player is: " + dist);	
			if (dist<=0.05f) {
				returnPlayer = false;
				yield break;
			}

			//t += Time.deltaTime;
			//Debug.Log("return player:Player co routine is on!");
			player.localPosition = Vector3.Lerp (player.localPosition, desiredpos, 0.5f * Time.deltaTime);
			player.localRotation = Quaternion.Lerp (player.localRotation, desiredRot, 0.5f * Time.deltaTime);
			yield return null;
			//Debug.Log ("VerticalposIndex is: " + verticalPosindex);
		}

	}

}
