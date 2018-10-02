using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellites : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		 //Rotate the object around its local X axis at 1 degree per second
		transform.Rotate(Vector3.right * Time.deltaTime * 5);
		transform.Rotate (Vector3.down * Time.deltaTime * 5);

		 //...also rotate around the World's Y axis
		//transform.Rotate(Vector3.right * 10 * Time.deltaTime, Space.World);
		//transform.Rotate(Vector3.down * Time.deltaTime, Space.World);
	}
}
