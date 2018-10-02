using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	private CanvasGroup fadeGroup;
	private float fadeInSpeed = 0.22f;
	List<Item> itemsToThrow = new List<Item>();
	List<Item> itemsToCompare = new List<Item>();
	public Item item1;
	public Item item2;
	public Item item3;
	public Item item4;
	public GameObject bomb;

	public Transform RightBackWall;
	public Transform LeftBackWall;
	public Transform UpWall;
	public Transform DownWall;

	public GameObject Villain;
	private GameObject itemObject;
	private GameObject bombObject;
	private GameObject explosiveGameObject;
	private List<GameObject> instantiatedItems = new List<GameObject> ();
	private Inventory inventory;
	public Camera tutorialCamera;
	private bool isTutorialOn;
	public Image exitButton;
	public Image skipTutorial;
	public Camera mainCamera;
	public GameObject inventoryGameObject;
	public Text skipIntro;

	private float gapBetweenItemThrow = 10.0f;
	private float period = 0.0f;
	public Text InstructionText;
	public Image UpArrow;
	public Image DownArrow;
	public Image RightArrow;
	public Image LeftArrow;
	private float arrowTimer = 0f;

	public Slider healthBarSlider;
	private int rocketItemMoveDirectionIndex;
	private int rocketItemxyValue;
	private Dictionary<int, int> rocketItemMoveDirections = new Dictionary<int, int>();
	private Dictionary<int, int> rocketItemxzValues = new Dictionary<int, int>();

	private LineRenderer lineRenderer;
	public Color c1;
	public Color c2;
	public Color c3;
	public Material lineMaterial;
	public GameObject lineStartingPoint;

	public Text missionText1;
	public Text missionText2;
	public Text missionText3;
	public Text missionText4;
	public Text missionText5;
	public Text missionText6;

	public AudioClip tutorialMusic;
	public AudioClip gameplayMusic;
	public AudioClip bombMusic;
	private AudioSource gameMusicSource;
	private AudioSource tutorialMusicSource;
	private AudioSource bombMusicSource;

	public bool isgameOver;
	public GameObject explosion2;

	void Start ()
	{
		//Grab the only canvasGroup in the scene
		fadeGroup = FindObjectOfType<CanvasGroup> ();

		//Start with a blank screen
		fadeGroup.alpha = 0;

		UpArrow.enabled = false;
		DownArrow.enabled = false;
		RightArrow.enabled = false;
		LeftArrow.enabled = false;
		InstructionText.enabled = false;
		exitButton.enabled = false;
		diableInventoryImages ();
		inventory = FindObjectOfType<Inventory>();
		instantiatedItems.Clear ();
		itemsToThrow.Clear ();
		itemsToCompare.Clear ();
		itemsToThrow.Add (item1);
		itemsToThrow.Add (item2);
		itemsToThrow.Add (item3);
		itemsToThrow.Add (item4);

		itemsToCompare.Add (item1);
		itemsToCompare.Add (item2);
		itemsToCompare.Add (item3);
		itemsToCompare.Add (item4);
		isgameOver = false;
		tutorialMusicSource.Play ();
	}
	public void Awake(){
		isTutorialOn = true;
		tutorialMusicSource = AddAudio(tutorialMusic, true, false, 0.8f);
		gameMusicSource = AddAudio(gameplayMusic, true, false, 0.8f);
		bombMusicSource = AddAudio(bombMusic, false, false, 1.0f);
	}

	void Update ()
	{
		PlayMusic ();
		if (isgameOver) {
			fadeGroup.alpha += Time.deltaTime * fadeInSpeed;
			if (fadeGroup.alpha >= 1) {
				Time.timeScale = 1.0F;
				FindObjectOfType<GameScene> ().ExitScene ();	
			}
		}
		transform.RotateAround(Vector3.zero, Vector3.right, (float) 0.5);
		lineStartingPoint.transform.RotateAround(Vector3.zero, Vector3.right, (float) 0.5);

		if (itemsToCompare.Count == 0 && !isTutorialOn) {
			Victory ();
		}
		if (arrowTimer > 0) {
			if ((Time.time - arrowTimer) > 3f) {
				UpArrow.enabled = false;
				DownArrow.enabled = false;
				RightArrow.enabled = false;
				LeftArrow.enabled = false;
				arrowTimer = 0f;
				InstructionText.enabled = false;
			}
		}

		if (period > gapBetweenItemThrow)
		{
			if (itemsToThrow.Count > 0) {

				int i = Random.Range (0, itemsToThrow.Count);
				int attachBombindex = Random.Range (0,2);

				itemObject = Instantiate (itemsToThrow [i].gameObject, Villain.transform.position, Quaternion.identity);
				if (attachBombindex == 0) {
					bombObject = Instantiate (bomb, itemObject.transform.localPosition, Quaternion.identity);
					bombObject.transform.parent = itemObject.transform;
				}
				instantiatedItems.Add (itemObject);
				//Adding Line renderer to the instantiated satellite part
				lineRenderer = itemObject.AddComponent<LineRenderer> ();
				//lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
				lineRenderer.material = new Material (lineMaterial);
				lineRenderer.widthMultiplier = 2.0f;
				// A simple 2 color gradient with a fixed alpha of 1.0f.
				float alpha3 = 1.0f;
				float alpha2 = 0.5f;
				float alpha1 = 0.0f;
				Gradient gradient = new Gradient ();
				gradient.SetKeys (
					new GradientColorKey[] {
						new GradientColorKey (c1, 0.0f),
						new GradientColorKey (c2, 1.0f),
						new GradientColorKey (c3, 2.0f)
					},
					new GradientAlphaKey[] {
						new GradientAlphaKey (alpha1, 0.0f),
						new GradientAlphaKey (alpha2, 1.0f),
						new GradientAlphaKey (alpha3, 2.0f)
					}
				);
				lineRenderer.colorGradient = gradient;

				itemsToThrow.Remove (itemsToThrow [i]);
				rocketItemMoveDirectionIndex = Random.Range (0, 2);
				Debug.Log ("Random no for direction is: " + rocketItemMoveDirectionIndex);
				rocketItemMoveDirections.Add (itemObject.GetInstanceID (), rocketItemMoveDirectionIndex);
				rocketItemxyValue = Random.Range (-10, 11);
				Debug.Log ("Rando no for XY Value is: " + rocketItemxyValue);
				rocketItemxzValues.Add (itemObject.GetInstanceID (), rocketItemxyValue);

				if(!isTutorialOn){
				if (rocketItemMoveDirectionIndex == 0) {
					if (rocketItemxyValue >= 0) {
						RightArrow.enabled = true;
						InstructionText.enabled = true;
						InstructionText.text = "Swipe Right to catch!";
						arrowTimer = Time.time;
					} else {
						LeftArrow.enabled = true;
						InstructionText.enabled = true;
						InstructionText.text = "Swipe Left to catch!";
						arrowTimer = Time.time;
					}
				}
				if (rocketItemMoveDirectionIndex == 1) {
					if (rocketItemxyValue >= 0) {
						DownArrow.enabled = true;
						InstructionText.enabled = true;
						InstructionText.text = "Swipe Down to catch!";
						arrowTimer = Time.time;
					} else {
						UpArrow.enabled = true;
						InstructionText.enabled = true;
						InstructionText.text = "Swipe Up to catch!";
						arrowTimer = Time.time;
					}
				}
			}

				period = 0;

			}
		}


		if (instantiatedItems.Count > 0) {
			for(int j=0; j< instantiatedItems.Count; j++){
					instantiatedItems [j].transform.RotateAround (Vector3.zero, Vector3.right, (float)0.5);

					//Rotate the object around its local X axis at 1 degree per second
					instantiatedItems [j].transform.Rotate (Vector3.right * Time.deltaTime * 5.0f);
					instantiatedItems [j].transform.Rotate (Vector3.down * Time.deltaTime * 5.0f);
				instantiatedItems [j].transform.position = Vector3.MoveTowards (instantiatedItems [j].transform.position, transform.position, 0.3f);

//				if (rocketItemMoveDirections [instantiatedItems [j].GetInstanceID()] == 0) {
//				int z = rocketItemxzValues [instantiatedItems [j].GetInstanceID()];
//									if (z >= 0){
//										instantiatedItems [j].transform.position = Vector3.MoveTowards (instantiatedItems [j].transform.position, RightBackWall.transform.position, 0.3f);
//												}
//									else {
//										instantiatedItems [j].transform.position = Vector3.MoveTowards (instantiatedItems [j].transform.position, LeftBackWall.transform.position, 0.3f);
//										}
//
//					}

//				if (rocketItemMoveDirections [instantiatedItems [j].GetInstanceID()] == 1) {
//					int z = rocketItemxzValues [instantiatedItems [j].GetInstanceID()];
//					if (z >= 0){
//						instantiatedItems [j].transform.position = Vector3.MoveTowards (instantiatedItems [j].transform.position, DownWall.transform.position, 0.3f);
//								}
//					else {
//						instantiatedItems [j].transform.position = Vector3.MoveTowards (instantiatedItems [j].transform.position, UpWall.transform.position, 0.3f);
//						}
//				}
				//lineRenderer.SetPosition(0, new Vector3(Villain.transform.position.x, Villain.transform.position.y,Villain.transform.position.z));
				//lineRenderer.SetPosition(1, new Vector3(instantiatedItems [j].transform.position.x,instantiatedItems [j].transform.position.y,instantiatedItems [j].transform.position.z));
				lineRenderer.SetPosition(0, lineStartingPoint.transform.position);
				lineRenderer.SetPosition (1, instantiatedItems[j].transform.position);
			}

		}
		healthBarSlider.value -=0.0003f;  //reduce health
		period += Time.deltaTime;

		}

	public void RobotOnTriggerEnter(Collider other) 
	{
		Debug.Log ("Player onTrigger entered");
		if (other.gameObject.CompareTag ("RocketItem"))
		{
			Debug.Log ("Player touched RocketItem");

			for (int i = 0; i < itemsToCompare.Count; i++) {
				if (other.name.Contains(itemsToCompare [i].gameObject.name) && (!isTutorialOn)) {

					Item itemi = itemsToCompare [i];
					inventory.AddItem (itemi);
					itemsToCompare.Remove(itemi);

					foreach (Transform child in other.gameObject.transform) {
						if (child.gameObject.CompareTag ("Bomb")) {
							Time.timeScale = 0.3F;
							Debug.Log ("Player hit bomb!");
							BlowPlayerandExit ();
						}
					}

					for (int j = 0; j < itemsToThrow.Count; j++) {
						if (other.name.Contains (itemsToThrow [j].gameObject.name)) {
							Item itemj = itemsToThrow [j];
							itemsToThrow.Remove(itemj);
						}
					}
						
					for(int k = 0; k< instantiatedItems.Count; k++){
						
						if (other.name.Contains (instantiatedItems [k].name)) {
							instantiatedItems.Remove (instantiatedItems[k]);
						}
					}

					int key1 = other.gameObject.GetInstanceID();

					if (rocketItemxzValues.ContainsKey (key1)) {
						rocketItemxzValues.Remove (key1);
					}
				
					if (rocketItemMoveDirections.ContainsKey (key1)) {
						rocketItemMoveDirections.Remove (key1);
					}

				}
			}
		
		}
		other.gameObject.SetActive (false);
	}


	private void Victory()
	{
		FindObjectOfType<GameScene> ().CompleteLevel ();
	}

	private void BlowPlayerandExit(){
		Debug.Log ("Player will be blown now.");
		explosiveGameObject = Instantiate (explosion2, transform.localPosition, Quaternion.identity);
		explosiveGameObject.transform.parent = transform;
		gameMusicSource.Stop ();
		bombMusicSource.Play();
		GameOver ();
	}

	private void GameOver(){
		isgameOver = true;
		Debug.Log ("GameOver!");
	}
	public void removeUnTouchedItemsAndPutBackForThrow(Collider wallCollider){


		// Remove the rocket items which have passed player without touching him, and put back in itemsToThrow
			for (int a = 0; a < itemsToCompare.Count; a++) {
			if (wallCollider.name.Contains (itemsToCompare [a].gameObject.name)) {
					Item item = itemsToCompare [a];
					itemsToThrow.Add (item);
				}
			}

		//remove from instantiated List as well
		for(int k = 0; k < instantiatedItems.Count; k++){
			if (wallCollider.name.Contains (instantiatedItems [k].name)) {
				instantiatedItems.Remove (instantiatedItems[k]);

			}
		}

		//Remove the collided gameobject references from the Dictionaries entries created during instantiation
		int key1 = wallCollider.gameObject.GetInstanceID();

		if (rocketItemxzValues.ContainsKey (key1)) {
			rocketItemxzValues.Remove (key1);
		}
			

		if (rocketItemMoveDirections.ContainsKey (key1)) {
			rocketItemMoveDirections.Remove (key1);
		}
	}

	private void printDictionaries(){
		foreach (KeyValuePair<int, int> pair in rocketItemMoveDirections)
		{
			Debug.Log("Rocket Directions Key: {0} " + pair.Key + "Values: {1}" + pair.Value);
		}

		foreach (KeyValuePair<int, int> pair in rocketItemxzValues)
		{
			Debug.Log(" Rocket XY Values Key: {0} " + pair.Key + "Values: {1}" + pair.Value);
		}
	}

	public void onExitTutorialClick(){
		isTutorialOn = false;
		exitButton.enabled = true;
		mainCamera.enabled = true;
		skipTutorial.enabled = false;
		tutorialCamera.enabled = false;
		skipIntro.enabled = false;
		missionText1.text = "";
		missionText2.text = "";
		missionText3.text = "";
		missionText4.text = "";
		missionText5.text = "";
		missionText6.text = "";
		tutorialMusicSource.Stop ();

		enableInventoryImages ();
	}

	public bool getisTutorialOn {get { return isTutorialOn;} }
	public bool getisGameOver {get { return isgameOver;} }

	private void diableInventoryImages(){
		foreach (Transform t in inventoryGameObject.transform) {
			foreach (Transform tt in t) {
				tt.GetComponent<Image> ().enabled = false;
			}
		}
	}

	private void enableInventoryImages(){
		foreach (Transform t in inventoryGameObject.transform) {
			foreach (Transform tt in t) {
				tt.GetComponent<Image> ().enabled = true;
			}
		}
	}

	public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol) { 
		AudioSource newAudio = gameObject.AddComponent<AudioSource>();
		newAudio.clip = clip; 
		newAudio.loop = loop;
		newAudio.playOnAwake = playAwake;
		newAudio.volume = vol; 
		return newAudio; 
	}

	public void PlayMusic(){
		
		if(!isTutorialOn && !gameMusicSource.isPlaying && !isgameOver){
			gameMusicSource.Play ();
		}
	}

}
