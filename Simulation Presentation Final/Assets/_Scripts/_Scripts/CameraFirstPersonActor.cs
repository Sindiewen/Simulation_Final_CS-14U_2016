using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Allows the camera to get a first person view of the chosen actor
public class CameraFirstPersonActor : MonoBehaviour
{

    // Public Variables
	public GameObject[] Actors;
	public GameObject SingletonsObject;

	// Private Variables

	private bool firstPlaceToggle = true;
	private bool neutralToggle = false;
	private bool hikerToggle = false;
	private bool bodyToggle = false;
	private bool floristToggle = false;
	private bool businessToggle = false;

	private TravelerController actorInFirst;

	void Start()
	{
		GoToFirst();
	}

	void Update()
	{
		actorInFirst = SingletonsObject.GetComponent<ActorPositionManager>().actorInFirst;
	}

	void LateUpdate()
	{
		if (firstPlaceToggle == true)
		{
			Transform first = actorInFirst.transform.FindChild("view");
			this.transform.position = first.transform.position;
			transform.rotation = first.transform.rotation;
		}
		else if (neutralToggle == true)
		{
			this.transform.position = Actors[0].transform.position + Vector3.up * 2;
			transform.rotation = Actors[0].transform.rotation;
		}
		else if (hikerToggle == true)
		{
			this.transform.position = Actors[1].transform.position + Vector3.up * 2;
			transform.rotation = Actors[1].transform.rotation;
		}
		else if (bodyToggle == true)
		{
			this.transform.position = Actors[2].transform.position + Vector3.up * 2;
			transform.rotation = Actors[2].transform.rotation;
		}
		else if (floristToggle == true)
		{
			this.transform.position = Actors[3].transform.position + Vector3.up * 2;
			transform.rotation = Actors[3].transform.rotation;
		}
		else if (businessToggle == true)
		{
			this.transform.position = Actors[4].transform.position + Vector3.up * 2;
			transform.rotation = Actors[5].transform.rotation;
		}
	}

	public void GoToFirst()
	{
		firstPlaceToggle = true; 
		neutralToggle = false;
		hikerToggle = false;
		bodyToggle = false;
		floristToggle = false;
		businessToggle = false;
	}

    public void GoToNeutral()
    {
		firstPlaceToggle = false;
		neutralToggle = true;
		hikerToggle = false;
		bodyToggle = false;
		floristToggle = false;
		businessToggle = false;

	}

    public void GoToHiker()
    {
		firstPlaceToggle = false;
		neutralToggle = false;
		hikerToggle = true;
		bodyToggle = false;
		floristToggle = false;
		businessToggle = false;
	}


    public void GoToBody()
    {
		firstPlaceToggle = false;
		neutralToggle = false;
		hikerToggle = false;
		bodyToggle = true;
		floristToggle = false;
		businessToggle = false;
	}

    public void GoToFlorist()
    {
		firstPlaceToggle = false;
		neutralToggle = false;
		hikerToggle = false;
		bodyToggle = false;
		floristToggle = true;
		businessToggle = false;
	}

    public void GoToBusiness()
    {
		firstPlaceToggle = false;
		neutralToggle = false; 
		hikerToggle = false;
		bodyToggle = false;
		floristToggle = false;
		businessToggle = true;
	}
}
