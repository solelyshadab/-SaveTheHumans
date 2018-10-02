using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour {

	private CanvasGroup fadeGroup;
	private float loadTime;
	private float minimunlogotime = 3.0f;

	private void Start()
	{
		//Grab the only canvasGroup in the scene
		fadeGroup = FindObjectOfType<CanvasGroup>();

		//Start with a white screen
		fadeGroup.alpha = 1;

		//Pre load the game

		//Get a timestamp of the completion time
		//if loadtime is less give it a small buffer so that logo can be appreciated
		if (Time.time < minimunlogotime)
			loadTime = minimunlogotime;
		else
			loadTime = Time.time;
	}
	private void Update()
	{
		//Fade-in
		if (Time.time < minimunlogotime)
			fadeGroup.alpha = 1 - Time.time;

		//Fade-out
		if (Time.time > minimunlogotime && loadTime != 0) 
		{
			fadeGroup.alpha = Time.time - minimunlogotime;
			if (fadeGroup.alpha >= 1)
			{
				SceneManager.LoadScene ("Menu");
			}
		}

	}
}
