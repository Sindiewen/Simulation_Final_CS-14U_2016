using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class SystemControlTime : MonoBehaviour 
{
	// Public Variables
	public Text TimescaleButtonText;


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
}
