using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCamera : MonoBehaviour {

	public Transform player;
	public Transform villain;
	private Vector3 center;
	public Transform villainLeftSide;
	public Transform villainRightSide;
	private Vector3 side1;
	private Vector3 side2;
	private Vector3 perpendicularAxis;

	private float sentencefinishTime = 0f;
	private AudioSource audiosource;
	public AudioClip typeSound;
	public AudioClip impactSound;

	private string message1;
	public Text missionText1;
	private string message2;
	public Text missionText2;
	private string message3;
	public Text missionText3;
	private string boldMessage1;
	public Text missionTextBold1;
	private string boldMessage2;
	public Text missionTextBold2;
	private string boldMessage3;
	public Text missionTextBold3;
	private bool startNextLine;

	public Text skipIntroText;
	private PlayerController playerController;

	void Start () {
		audiosource = GetComponent<AudioSource>();

		playerController = FindObjectOfType<PlayerController> ();
		message1 = missionText1.text;
		missionText1.text = "";
		message2 = missionText2.text;
		missionText2.text = "";
		StartCoroutine(TypeText1());
		startNextLine = false;
		StartCoroutine(TypeText2());
		message3 = missionText3.text;
		missionText3.text = "";
		StartCoroutine(TypeText3());

		boldMessage1 = missionTextBold1.text;
		missionTextBold1.text = "";
		StartCoroutine(TypeText4());
		boldMessage2 = missionTextBold2.text;
		missionTextBold2.text = "";
		StartCoroutine(TypeText5());
		boldMessage3 = missionTextBold3.text;
		missionTextBold3.text = "";
		StartCoroutine(TypeText6());
		StartCoroutine(ChangetoPlayText());
	}

	public void LateUpdate () {

		transform.RotateAround(Vector3.zero, Vector3.right, (float) 0.5);
		center = ((villain.position - player.position)/2.0f) + player.position;
		side1 = (villainLeftSide.position - player.position);
		side2 = (villainRightSide.position - player.position);
		perpendicularAxis = Vector3.Cross (side2,side1);
		transform.RotateAround (center,perpendicularAxis, 0.5f );
		transform.LookAt(center);

		if (string.Compare(message1, missionText1.ToString())==0) {
			sentencefinishTime = Time.time;
		}
		if ((Time.time - sentencefinishTime) > 7.0f) {
			//Debug.Log("Time to start next line");
			//missionText1.text = "";
			startNextLine = true;

		}
		if(startNextLine){
			//StartCoroutine(TypeText2());
			startNextLine = false;
		}

	}

	IEnumerator TypeText1 () {
					
			foreach (char letter in message1.ToCharArray()) {
			
			if (!playerController.getisTutorialOn)
				yield break;
				missionText1.text += letter;
			audiosource.PlayOneShot (typeSound, 0.2F);
				yield return null;
				yield return new WaitForSeconds (0.05f);
				//Debug.Log ("TypeText1 co routine is ON.");
			}
	
}
	IEnumerator TypeText2 () {

			
			yield return new WaitForSeconds (22.0f);
			missionText1.text = "";
			foreach (char letter1 in message2.ToCharArray()) {
			if (!playerController.getisTutorialOn)
				yield break;
			audiosource.PlayOneShot (typeSound, 0.2F);
				missionText2.text += letter1;
				yield return null;
				yield return new WaitForSeconds (0.05f);
			}
	
	}

	IEnumerator TypeText3 () {

			
			yield return new WaitForSeconds (44.0f);
			missionText2.text = "";
			foreach (char letter2 in message3.ToCharArray()) {
			if (!playerController.getisTutorialOn)
				yield break;
			audiosource.PlayOneShot (typeSound, 0.2F);
				missionText3.text += letter2;
				yield return null;
				yield return new WaitForSeconds (0.05f);
			}	
		
	}
	IEnumerator TypeText4 () {
				
			yield return new WaitForSeconds (52.0f);
		if (!playerController.getisTutorialOn)
			yield break;
			missionTextBold1.text = boldMessage1;	
		audiosource.PlayOneShot (impactSound, 1F);
	}

	IEnumerator TypeText5 () {
		
			
			yield return new WaitForSeconds (53.0f);
		if (!playerController.getisTutorialOn)
			yield break;
			missionTextBold2.text = boldMessage2;
		audiosource.PlayOneShot (impactSound, 1F);
	}

	IEnumerator TypeText6 () {
		
			
			yield return new WaitForSeconds (54.0f);
		if (!playerController.getisTutorialOn)
			yield break;
			missionTextBold3.text = boldMessage3;	
		audiosource.PlayOneShot (impactSound, 1F);
	}

	IEnumerator ChangetoPlayText () {
		yield return new WaitForSeconds (55.0f);
		skipIntroText.text = "Play";		
	}
}

