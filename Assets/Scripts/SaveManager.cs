using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {

	public static SaveManager Instance { set; get;}
	public SaveState state;

	private void Awake()
	{
		
		DontDestroyOnLoad (gameObject);
		Instance = this;
		Load ();

		//Are we using the accelerometer AND can we use it
		if (state.usingAccelerometer && !SystemInfo.supportsAccelerometer) 
		{
			//If we cant, make sure we are not using it next time
			state.usingAccelerometer = false;
			Save ();
		}
	}

	//Save the whole state of this saveState script to the player pref
	public void Save()
	{
		PlayerPrefs.SetString("save", Helper.Serialize<SaveState>(state));
	}

	//load the previous saved state from the player prefs
	public void Load()
	{
		//Do we already have a save?
		if (PlayerPrefs.HasKey ("save")) {
			state = Helper.Deserialize<SaveState> (PlayerPrefs.GetString("save"));
		} 
		else 
		{
			state = new SaveState ();
			Save ();
			Debug.Log ("No save file found, creating a new one!");
		}
	}

	//Check if the color is owned
	public bool isColorOwned(int index)
	{
		//Check if the bit is set, if yes then color is owned
		return(state.colorOwned & (1 << index)) !=0;
	}

	//Check if the trail is owned
	public bool isTrailOwned(int index)
	{
		//Check if the bit is set, if yes then trail is owned
		return(state.trailOwned & (1 << index)) !=0;
	}

	//Attempt buying color, return true/false
	public bool BuyColor(int index, int cost)
	{
		if (state.gold >= cost)
		{
			//Enough money, remove from the current stack gold
			state.gold -= cost;
			UnlockColor (index);
			Save ();
			return true;
		} 
		else
		{
			//Not enough money, return false
			return false;
		}
	}

	//Attempt buying trail, return true/false
	public bool BuyTrail(int index, int cost)
	{
		if (state.gold >= cost) 
		{
			//Enough money, remove from the current stack gold
			state.gold -= cost;
			UnlockTrail (index);
			Save ();
			return true;
		} 
		else
		{
			//Not enough money, return false
			return false;
		}
	}
		

	//Unlock a color in the 'colorOwned' int
	public void UnlockColor(int index)
	{
		// Toggle on the bit at index
		state.colorOwned |= 1 << index;
	}

	//Unlock a color in the 'trailOwned' int
	public void UnlockTrail(int index)
	{
		// Toggle on the bit at index
		state.trailOwned |= 1 << index;
	}

	//Complete Level
	public void CompleteLevel (int index)
	{
		//if this is the current active level
		if(state.completedLevel == index)

		{
			state.completedLevel++;
			Save ();
		}
	}

	//Reset the whole save file
	public void ResetSave()
	{
		PlayerPrefs.DeleteKey ("save");
	}
}

