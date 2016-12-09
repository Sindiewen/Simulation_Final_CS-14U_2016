using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class SystemControlTime : MonoBehaviour 
{
	// Public Variables
	//public Text TimescaleButtonText;

	// Private Variables
	//[Range(0f, 3f)] private float TimescaleSlider = 1.0f;


	public void DoubleSpeed()
	{
		// Change timescale to double speed
		Time.timeScale = 2.0f;
	}

	public void NormalSpeed()
	{
		// Changes timescale to normal speed
		Time.timeScale = 1.0f;
	}

	public void PauseTimescale()
	{
		Time.timeScale = 0.0f;
	}

	/*
	// Allows a slider to interface with the TimescaleSlider to change the timescale
	public void ChangeTimeScale()
	{

	}

	void Update()
	{
		// Ensures the timescale gets changed at runtime.
		Time.timeScale = TimescaleSlider;
	}
	*/
}
