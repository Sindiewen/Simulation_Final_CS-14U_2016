using UnityEngine;
using System.Collections;

public class Text3DFaceCamera : MonoBehaviour 
{
	// Public Variables
	public Camera camToFace;		// Stores which camera the text should face

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Looks at the main camera at all times
		this.transform.LookAt(camToFace.transform);
		this.transform.Rotate(0f, 180f, 0f);
	}
}
