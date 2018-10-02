using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	public static Manager Instance { set; get;}

	public Material PlayerMaterial;
	public Color[] playerColors = new Color[10];
	public GameObject[] playerTrails = new GameObject[10];

	public int currentLevel = 0;  // Used when changing from menu to game scene, which level to load
	public int menuFocus = 0;	// Used when entering the menu scene, to know which menu to focus

	private Dictionary<int, Vector2> activeTouches = new Dictionary<int, Vector2>();


	private void Awake()
	{
		DontDestroyOnLoad (gameObject); // so that we dont loose the object as we chnge the scene
		Instance = this;
	}



	public Vector3 GetPlayerInput()
	{
		//Are we using accelerometer
		if (SaveManager.Instance.state.usingAccelerometer) 
		{
			//If we can use it, replace Y parameter by Z, we dont need that Y
			Vector3 a = Input.acceleration;
			a.y = a.z;
			return a;
		}

		//Read all touches from the user
		Vector3 r = Vector3.zero; // r will be zero at the beginning
		foreach (Touch touch in Input.touches) 
		{
			//If we just started pressing on the screen
			if (touch.phase == TouchPhase.Began) {
				activeTouches.Add (touch.fingerId, touch.position);
			}
			//If we remove our finger off the screen
			else if (touch.phase == TouchPhase.Ended || Input.GetMouseButtonUp (0)) {
				if (activeTouches.ContainsKey (touch.fingerId))
					activeTouches.Remove (touch.fingerId);
			} 
			//Our finger is either moving, or stationary, in both cases, let's use the delta
			else
			{
				float mag = 0;
				r = touch.position - activeTouches [touch.fingerId];
				mag = r.magnitude / 600;
				r = r.normalized * mag;

			}
		

		}

		return r;
	}

}
