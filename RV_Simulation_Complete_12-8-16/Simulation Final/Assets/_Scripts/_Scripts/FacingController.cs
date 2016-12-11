using UnityEngine;
using System.Collections;

public class FacingController : MonoBehaviour
{
	void Update ()
	{
		// turns the traveler gameobject to face the direction of travel
		TravelerController travelerController = GetComponentInParent<TravelerController> ();
		if (travelerController != null && travelerController.FacingDirection != Vector3.zero)
		{
			// only set the rotation if the FacingDirection vector has valid values (i.e. not all zeroes)
			transform.rotation = Quaternion.LookRotation (travelerController.FacingDirection);
		}
	}
}
