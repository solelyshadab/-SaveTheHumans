using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tempGameScene : MonoBehaviour {

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
