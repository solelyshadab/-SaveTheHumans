using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillainMovement : MonoBehaviour {
	public Transform VillainTargetPos;
	public Transform VillainStartPos;
	public Transform VillainShip;
	private bool returnVillain;

	private float gapBetweenItemThrow = 5.0f;
	private float period = 0.0f;
	private PlayerController playerController;
	private bool moveVillain;
	// Use this for initialization
	void Start () {
		playerController = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (period > gapBetweenItemThrow && !playerController.getisTutorialOn) {
			moveVillain = true;
			//VillainShip.transform.position = Vector3.MoveTowards (VillainShip.transform.position,VillainTargetPos.transform.position, 0.3f);
			period = 0;
		}
		if(!playerController.getisTutorialOn)
			period += Time.deltaTime;

		if (returnVillain) {
			StartCoroutine (MoveVillain(VillainStartPos.localPosition));
			//VillainShip.transform.position = Vector3.MoveTowards (VillainShip.transform.position, VillainStartPos.transform.position, 0.3f);
		}
		if(moveVillain)
			StartCoroutine (MoveVillain(VillainTargetPos.localPosition));
		
	}

	private IEnumerator MoveVillain(Vector3 desiredpos)
	{
		var t = 0f;

		while(true)
		{
			float dist = (desiredpos - VillainShip.localPosition).magnitude;
			Debug.Log ("Distance magnitude for player is: " + dist);	
			if (dist<=0.05f) {
				returnVillain = true;
				Debug.Log ("Return Villain now.");
				yield break;
			}
			Debug.Log ("MoveVillain co-routine on!");
			VillainShip.localPosition = Vector3.Lerp (VillainShip.localPosition, desiredpos, 0.01f * t);
			yield return null;
		}

	}

	private IEnumerator MoveVillaintoStartpos(Vector3 desiredpos)
	{

		while(true)
		{
			float dist = (desiredpos - VillainShip.localPosition).magnitude;
			if (dist<=0.05f) {
				returnVillain = false;
				yield break;
			}
			Debug.Log ("MoveVillainStartPos co-routine on!");
			VillainShip.localPosition = Vector3.Lerp (VillainShip.localPosition, desiredpos, 0.01f * Time.deltaTime);
			yield return null;
		}
	}

}
