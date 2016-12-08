using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIActorBPMDisplay : MonoBehaviour 
{
	// Public Variables
	public Text GUIBPMCurrentText;		// Stores a reference to the UI Text GameObject for Current BPM Text
	public Text GUIBPMMaxText;			// Stores a reference to the UI Text GameObject for Max BPM Text

	// private variables
	private TravelerController Actor;	// Object of the Traveler Controlle Class

	void Start () 
	{
		// Gets this GameObject's Traveler Controller componenet
		Actor = this.GetComponent<TravelerController>();
	}
	
	void Update () 
	{
		// Changes the GUI BPM Count text to associate the current BPM
		GUIBPMCurrentText.text = Actor.currentBPM.ToString();

		// Changes the MaxBPM Text to show the max BPM of the actor
		GUIBPMMaxText.text = Actor.maxBPM.ToString();

	}
}
