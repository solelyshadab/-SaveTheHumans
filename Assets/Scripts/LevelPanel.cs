using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InitLevel ();
		
	}
	
	private void InitLevel()
	{
		

		//For every children under our level Panel, find the button and add on-click

		int i = 0;
		foreach (Transform t in transform) 
		{
			int currentIndex = i;
			Button b = t.GetComponent<Button> ();
			b.onClick.AddListener (() => OnLevelSelect (currentIndex));
	
			Image img = t.GetComponent<Image> ();
			
						//Is the clicked level unlocked?
						if(i <= SaveManager.Instance.state.completedLevel)
						{
							//It is unlocked, but is it completed?
							if (i == SaveManager.Instance.state.completedLevel)
							{
								//It is not completed!
								img.color = Color.white;
							}
							else
							{
								//Level is already completed!
								img.color = Color.green;
							}
						}
						else
						{
							// Level is not locked, disable the button
							b.interactable = false;
			
							// Set to a dark color
							img.color = Color.grey;
						}

			i++;
		}
	}

		private void OnLevelSelect(int currentIndex)
		{
			Debug.Log ("Selecting level button: " + currentIndex);
			Manager.Instance.currentLevel = currentIndex;
		SceneManager.LoadScene(Manager.Instance.currentLevel.ToString());
			
				//isEnteringLevel = true;
		
		}
}
