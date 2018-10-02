using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameScene : MonoBehaviour {

	private CanvasGroup fadeGroup;
	private float fadeInDuration = 2;
	private bool gameStarted;

	//public Transform arrow;
	private Transform playerTransform;
	//public Objective objective;


	private void Start()
	{
		//Let's find the player transform (Player is the only object that has player motor class on it)
		//playerTransform = FindObjectOfType<PlayerMotor>().transform;

		//Load up the level
		//SceneManager.LoadScene(Manager.Instance.currentLevel.ToString(), LoadSceneMode.Additive);

		//Grab the only canvasGroup in the scene
		fadeGroup = FindObjectOfType<CanvasGroup> ();

		//Start with a white screen i.e set the fade to full capacity
		fadeGroup.alpha = 1;
	}

	private void Update()
	{
//		if (objective != null)
//		{
//			//If we have an objective
//			//Rotate the arrow
//			Vector3 dir = playerTransform.InverseTransformPoint(objective.GetCurrentRing().position);
//			float a = Mathf.Atan2 (dir.x, dir.z) * Mathf.Rad2Deg;
//			a += 180;
//			arrow.transform.localEulerAngles = new Vector3 (0, 180 , a);
//		}

		if (Time.timeSinceLevelLoad <= fadeInDuration) {
			//Initial fade-in
			fadeGroup.alpha = 1 - (Time.timeSinceLevelLoad / fadeInDuration);
		}
		//If the initial fade-in is completed, and the game has not started
		else if (!gameStarted) 
		{
			//Ensure that fade is completely gone
			fadeGroup.alpha = 0;
			gameStarted = true;
		}
	}

	public void CompleteLevel()
	{
		
		//Complete the level, save the progress
		SaveManager.Instance.CompleteLevel(Manager.Instance.currentLevel);

		//Focus the level selection when we return to the menu scene
		Manager.Instance.menuFocus = 1;

		ExitScene ();
	}

	public void ExitScene()
	{
		SceneManager.LoadScene ("Menu");
	}
}
