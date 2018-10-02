using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 

public class SwipeControl1 : MonoBehaviour {

	Vector3 startPos;
	Vector3 endPos;
	float swipeDistance = 0;
	private bool upSwipe, downSwipe, rightSwipe, leftSwipe;
	public Text swipeText;
	public Transform player;
	public Transform mainCamera;
	private Quaternion desiredRotation;
	Vector3 desiredposCamera;
	public Transform rightEndCamera;
	public Transform leftEndCamera;
	public Transform upEndCamera;
	public Transform downEndCamera;
	private Vector3 startPositionCamera;
	private Quaternion startRotationCamera;
	private int posindex;
	//Vector3 playermiddlepos;
	//Vector3 desiredpos;
	//float rad =0;


	// Use this for initialization
	void Start () {
		upSwipe = downSwipe = rightSwipe = leftSwipe = true;
		startPositionCamera = mainCamera.localPosition;
		startRotationCamera = mainCamera.localRotation;
		posindex = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Began) {
				startPos = touch.position;
			} 
			else if (touch.phase == TouchPhase.Ended) {
				endPos = touch.position;
				swipeDistance = (endPos - startPos).magnitude;
				Swipe ();
				Reset ();
			}
		}
//		if (player.transform.localPosition == playermiddlepos) {
//			upSwipe = downSwipe = rightSwipe = leftSwipe = true;
//		}
	}

	private void Reset(){
		startPos = endPos = Vector3.zero;
		swipeDistance = 0;
	}

	private void Swipe(){
		Vector2 distance = endPos - startPos;
		if (Mathf.Abs (distance.x) > Mathf.Abs (distance.y)) {
			Debug.Log ("Horiontal Swipe");
			if (distance.x > 0 ) {
				Debug.Log ("Right Swipe");
				swipeText.text = "Right Swipe";
				//slowMotion();
				//desiredpos = player.transform.right * 10;
				//player.transform.localPosition = Vector3.Lerp (player.transform.localPosition, desiredpos, 0.01f);
				if (posindex == 0) {
					player.transform.Translate (Vector3.right * 10);
					desiredposCamera = rightEndCamera.localPosition;
					mainCamera.localPosition = Vector3.Lerp (mainCamera.localPosition, desiredposCamera, 0.1f);
					desiredRotation = rightEndCamera.localRotation;
					mainCamera.localRotation = Quaternion.Lerp(mainCamera.localRotation, desiredRotation, 0.1f );
					posindex += 1;
				}
				else if (posindex == -1) {
					player.transform.Translate (Vector3.right * 10);
					desiredposCamera = startPositionCamera;
					mainCamera.localPosition = Vector3.Lerp (mainCamera.localPosition, desiredposCamera, 0.1f);
					desiredRotation = startRotationCamera;
					mainCamera.localRotation = Quaternion.Lerp (mainCamera.localRotation, desiredRotation, 0.1f);
					posindex += 1;
				} else {
					Debug.Log ("Cant move more right!");
				}
					//mainCamera.transform.Translate(Vector3.right * -7);
				//mainCamera.transform.Translate(Vector3.up * -10);
				//mainCamera.transform.Translate(Vector3.forward * -19);
				//Debug.Log (mainCamera.transform.rotation);
				//normalMotion ();
				//if (rightSwipe) {
				//StartCoroutine (MovePlayer (1));
					//rightSwipe = false;
					//leftSwipe = true;
				//}

			}
			else if (distance.x < 0) {
				Debug.Log ("Left Swipe");
				swipeText.text = "Left Swipe";
				//slowMotion();
				//Debug.Log ("Player pos: " + player.transform.localPosition);
				//float rad = Vector3.Distance(new Vector3(player.transform.position.x, player.transform.position.y, 0), Vector3.zero);
				//float angle = (float) (-20.0f * System.Math.PI / 180);
				//desiredpos = new Vector3 ((float)System.Math.Cos(angle) * rad, (float) System.Math.Sin(angle)* rad , player.transform.localPosition.z );
				//desiredpos = player.transform.right * -10;
				//player.transform.localPosition = Vector3.Lerp (player.transform.localPosition, desiredpos, 0.01f);

				//mainCamera.transform.Rotate (0, 0, 10);
				//Debug.Log (mainCamera.transform.rotation);
				//mainCamera.transform.Translate (desiredposCamera);
				if(posindex == 0){
				player.transform.Translate (Vector3.left * 10);
				desiredposCamera = leftEndCamera.localPosition;
				mainCamera.localPosition = Vector3.Lerp (mainCamera.localPosition, desiredposCamera, 0.1f);
				desiredRotation = leftEndCamera.localRotation;
				mainCamera.localRotation = Quaternion.Lerp (mainCamera.localRotation, desiredRotation, 0.1f );
				posindex -= 1;
				}
				else if (posindex == 1) {
					player.transform.Translate (Vector3.left * 10);
					desiredposCamera = startPositionCamera;
					mainCamera.localPosition = Vector3.Lerp (mainCamera.localPosition, desiredposCamera, 0.1f);
					desiredRotation = startRotationCamera;
					mainCamera.localRotation = Quaternion.Lerp(mainCamera.localRotation, desiredRotation, 0.1f );
					posindex -= 1;
				}
				else {
					Debug.Log ("Cant move more left!");
				}
					//normalMotion ();
				//if (leftSwipe) {
					//StartCoroutine (MovePlayer (2));
					//rightSwipe = true;
					//leftSwipe = false;
				//}

			}
		}

			else if (Mathf.Abs (distance.x) < Mathf.Abs (distance.y)) {
				Debug.Log ("Vertical Swipe");
			if (distance.y > 0) {
				Debug.Log ("Up Swipe");
				Debug.Log ("Player pos: " + player.transform.localPosition);
				swipeText.text = "Up Swipe";
				//slowMotion ();
				player.transform.Translate (Vector3.forward * -10);
				//mainCamera.transform.Rotate (-10, 0, 0);
				//desiredpos = player.transform.forward * -10;
				//player.transform.localPosition = Vector3.Lerp (player.transform.localPosition, desiredpos, 0.01f);
				desiredRotation = Quaternion.Euler (-20, 0, 0);
				mainCamera.transform.localRotation = Quaternion.Lerp (mainCamera.localRotation, desiredRotation, 0.1f );
				normalMotion();
				if (upSwipe) {
					//StartCoroutine (MovePlayer (3));
					//upSwipe = false;
					//downSwipe = false;
				}
			}
			if (distance.y < 0) {
				Debug.Log ("Down Swipe");
				Debug.Log ("Player pos: " + player.transform.localPosition);
				swipeText.text = "Down Swipe";
				//slowMotion ();
				player.transform.Translate (Vector3.forward * 10);
				//mainCamera.transform.Rotate (10, 0, 0);
				//desiredpos = player.transform.forward * 10;
				//player.transform.localPosition = Vector3.Lerp (player.transform.localPosition, desiredpos, 0.01f);
				desiredRotation = Quaternion.Euler (-60, 0, 0);
				mainCamera.transform.localRotation = Quaternion.Lerp (mainCamera.localRotation, desiredRotation, 0.1f );
				normalMotion();
				//if(downSwipe){
				//StartCoroutine(MovePlayer(4));
					//downSwipe = false;
					//upSwipe = true;
			//}
		}
	}
	}


	IEnumerator MovePlayer(int i)
	{
		//Gather player's input
		//Vector3 inputs = Manager.Instance.GetPlayerInput();
		//float x = inputs.x;
		var t = 0f;
		var start = player.transform.localPosition;
		//swipeText.text = "CoRoutine";

		while(t < 0.3)
		{
			t += Time.deltaTime;
			//player.transform.localPosition = Vector3.Lerp(start, start + new Vector3(0, x, 0), t);
			if (i == 1) {
				//desiredpos = new Vector3 (Mathf.Asin (0.0174f) * rad, Mathf.Acos (0.0174f) * rad , player.transform.localPosition.z );
				player.transform.localPosition = Vector3.Lerp (player.transform.localPosition, player.transform.localPosition + Vector3.right * 10, t);
				//player.transform.localPosition = Vector3.Lerp (player.transform.localPosition, desiredpos, t);
			}
			else if (i == 2){
				//desiredpos = new Vector3 (Mathf.Asin (-0.0174f) * rad, Mathf.Acos (-0.0174f) * rad , player.transform.localPosition.z );
				player.transform.localPosition = Vector3.Lerp(player.transform.localPosition, player.transform.localPosition - Vector3.right * 10, t);
				//player.transform.localPosition = Vector3.Lerp(player.transform.localPosition, desiredpos, t);
			//swipeText.text = "";
			}
			else if(i==3)
				player.transform.localPosition = Vector3.Lerp(player.transform.localPosition, player.transform.localPosition - Vector3.up * 10, t);
			else if (i == 4)
				player.transform.localPosition = Vector3.Lerp(player.transform.localPosition, player.transform.localPosition - Vector3.down * 10, t);
			//swipeText.text = "";
			yield return null;

		}

	}

	public bool UpSwipe {get { return upSwipe;} }
	public bool DownSwipe {get { return downSwipe;} }
	public bool RightSwipe {get { return rightSwipe;} }
	public bool LeftSwipe {get { return leftSwipe;} }

	public void slowMotion(){

		Time.timeScale = 0.5f;
		Time.fixedDeltaTime = 0.02F * Time.timeScale;
	}

	public void normalMotion(){

		Time.timeScale = 1;
		Time.fixedDeltaTime = 0.02F ;
	}

}
