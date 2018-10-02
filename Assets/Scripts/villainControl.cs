using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class villainControl : MonoBehaviour {
	private LineRenderer lineRendererLeft;
	private LineRenderer lineRendererRight;
	public Color c1;
	public Color c2;
	public Color c3;
	public Material lineMaterial;
	public GameObject lineEndPointLeft;
	public GameObject lineEndPointRight;
	public GameObject lineStartPointLeft;
	public GameObject lineStartPointRight;

	// Use this for initialization
	void Start () {
		lineRendererLeft = lineStartPointLeft.AddComponent<LineRenderer>();
		lineRendererLeft.material = new Material(lineMaterial);
		lineRendererLeft.widthMultiplier = 2.0f;
		// A simple 2 color gradient with a fixed alpha of 1.0f.
		float alpha3 = 0.8f;
		float alpha2 = 0.4f;
		float alpha1 = 0.1f;
		Gradient gradient = new Gradient();
		gradient.SetKeys(
			new GradientColorKey[] { new GradientColorKey(c3, 0.0f), new GradientColorKey(c2, 1.0f), new GradientColorKey(c1, 2.0f)  },
			new GradientAlphaKey[] { new GradientAlphaKey(alpha1, 0.0f), new GradientAlphaKey(alpha2, 1.0f), new GradientAlphaKey(alpha3, 2.0f) }
		);
		lineRendererLeft.colorGradient = gradient;

		lineRendererRight = lineStartPointRight.AddComponent<LineRenderer>();
		lineRendererRight.material = new Material(lineMaterial);
		lineRendererRight.widthMultiplier = 3.0f;
		// A simple 2 color gradient with a fixed alpha of 1.0f.
		lineRendererRight.colorGradient = gradient;

	}

	// Update is called once per frame
	void LateUpdate () {
		transform.RotateAround(Vector3.zero, Vector3.right, (float) 0.5);
		lineRendererLeft.SetPosition (1, lineStartPointLeft.transform.position);
		lineRendererLeft.SetPosition(0, lineEndPointLeft.transform.position);

		lineRendererRight.SetPosition (1, lineStartPointRight.transform.position);
		lineRendererRight.SetPosition(0, lineEndPointRight.transform.position);


	}

}




