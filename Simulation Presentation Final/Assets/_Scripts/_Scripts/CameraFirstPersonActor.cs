using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Allows the camera to get a first person view of the chosen actor
public class CameraFirstPersonActor : MonoBehaviour
{

    // Public Variables
	public GameObject[] Actors;

	// Private Variables

	private bool neutralToggle = false;
	private bool hikerToggle = false;
	private bool bodyToggle = false;
	private bool floristToggle = false;
	private bool businessToggle = false;

	void LateUpdate()
	{
		if (neutralToggle == true)
		{
			this.transform.position = Actors[0].transform.position + Vector3.up * 2;
		}
		else if (hikerToggle == true)
		{
			this.transform.position = Actors[1].transform.position + Vector3.up * 2;
		}
		else if (bodyToggle == true)
		{
			this.transform.position = Actors[2].transform.position + Vector3.up * 2;
		}
		else if (floristToggle == true)
		{
			this.transform.position = Actors[3].transform.position + Vector3.up * 2;
		}
		else if (businessToggle == true)
		{
			this.transform.position = Actors[4].transform.position + Vector3.up * 2;
		}
	}

    public void GoToNeutral()
    {
		neutralToggle = true;
		hikerToggle = false;
		bodyToggle = false;
		floristToggle = false;
		businessToggle = false;

	}

    public void GoToHiker()
    {
		neutralToggle = false;
		hikerToggle = true;
		bodyToggle = false;
		floristToggle = false;
		businessToggle = false;
	}


    public void GoToBody()
    {
		neutralToggle = false;
		hikerToggle = false;
		bodyToggle = true;
		floristToggle = false;
		businessToggle = false;
	}

    public void GoToFlorist()
    {
		neutralToggle = false;
		hikerToggle = false;
		bodyToggle = false;
		floristToggle = true;
		businessToggle = false;
	}

    public void GoToBusiness()
    {
		neutralToggle = false; 
		hikerToggle = false;
		bodyToggle = false;
		floristToggle = false;
		businessToggle = true;
	}
}
