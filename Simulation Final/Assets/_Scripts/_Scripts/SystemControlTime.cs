using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class SystemControlTime : MonoBehaviour 
{
	// Public Variables
	public Text TimescaleCurrentText;
	public Slider TimescaleSlider;

	// Private variables
	private bool flag = false;


	void Update()
	{

		// If the flag is true
		if (flag)
		{
			// Pause simulation
			Time.timeScale = 0.0f;
		}
		// Otherwise, continue the simulation
		else
		{
			// Changes the timescale of the project:
			// 0 = Pause
			// 1 = Default
			// 2 = Double Speed
			// 3 = 3X Speed

			// Ensures the timescale gets changed at runtime.
			Time.timeScale = TimescaleSlider.value;
		}

		// Prints the current timescale value
		TimescaleCurrentText.text = Time.timeScale.ToString() + "x";

	}

	public void pause()
	{
		flag = !flag;
	}
}
