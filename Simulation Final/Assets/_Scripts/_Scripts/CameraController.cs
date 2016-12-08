using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{

	// Public Variables
	public float PanSpeed = 1.0f;


	void Update()
	{

		// Ensures valures are not 0
		float deltaX = Input.GetAxis ("Horizontal");
		float deltaY = Input.GetAxis ("Vertical");
		float deltaZoom = Input.GetAxis ("Zoom");

		if (deltaX != 0 || deltaY != 0)
		{
			// Moves the object around
			transform.Translate(deltaX * PanSpeed, deltaY * PanSpeed, 0.0f);
		}


		if (deltaZoom != 0)
		{
			// Zooms the camera in and out
			Camera.main.orthographicSize += deltaZoom;	// For orthographic
			Camera.main.fieldOfView += deltaZoom;		// for perspective
		}
	
	}


}
