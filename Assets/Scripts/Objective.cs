using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour {

	private List<Transform> rings = new List<Transform> ();

	public Material activeRing;
	public Material inactiveRing;
	public Material finalRing;

	private int ringPassed = 0;

	private void Start()
	{
		//Set the objective field in the game scene
		//FindObjectOfType<GameScene>().objective = this;


		//At the start of the level, assign inactive to all the rings
		foreach (Transform t in transform)
		{
			rings.Add (t);
			t.GetComponent<MeshRenderer> ().material = inactiveRing;
		}

		//Make sure we are not stupid
		if (rings.Count == 0) 
		{
			Debug.Log ("There is no objective assigned for this level, make sure you put some rings under the objective Object");
			return;
		}
		//Activate the first ring
		rings[ringPassed].GetComponent<MeshRenderer> ().material = activeRing;
		rings[ringPassed].GetComponent<Ring> ().ActivateRing();
	}

	public void NextRing()
	{
		//Play FX on the current ring
		rings[ringPassed].GetComponent<Animator>().SetTrigger("collectionTrigger");

		//Up the int
		ringPassed++;

		//If it's the final ring, let's call Victory
		if (ringPassed == rings.Count) 
		{
			Victory ();
			return;
		}

		//If this is the previous last, then give the next ring the "Final Ring" material
		if (ringPassed == rings.Count - 1)
			rings [ringPassed].GetComponent<MeshRenderer> ().material = finalRing;
		else
			rings [ringPassed].GetComponent<MeshRenderer> ().material = activeRing;

		//In both cases, we need to activate the ring!
		rings [ringPassed].GetComponent<Ring> ().ActivateRing();
	}

	public Transform GetCurrentRing()
	{
		return rings[ringPassed];
	}

	private void Victory()
	{
		FindObjectOfType<GameScene> ().CompleteLevel ();
	}
}
