using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ActorPositionManager : MonoBehaviour {


	// TODO:
	/*
	 * - Get the current list of actors
	 * - Get the current position of each actor, subtract it by the distance of the goal node to get the difference.
	 * 		- The difference is used to figure out how far they are from the goal
	 * 		- Once the distance has been get, use this, compare with every actor to see who is in their respective place.
	 * 		- Print to the console what place each actor is in
	 * 		- Once the first place actor has reached the goal node, iterate on the actor how many times they won.
	 */

	// Public Variables

	// A public array of actors

	[Header("Racing Mode Toggle")]
	public bool RacingMode;					// Checkbox - Weather the actors are going to race to the goal node or not

	[Header("Simulation Tools")]
	public TravelerController[] Actors;		// Array of actors to compare
	public VertexScript GoalNode;			// Stores the goal node	

	[Header("UI Position Text")]
	public Text uiPosText;					// Stores refrence to the UI Text Game object - prints the actor in 1st

	[Header("Camera Control")]
 	public Camera firstPlaceCam;            // Camera to view the leader actor
    //public Camera midPlaceCam;              // Camera to view the midpoint of the group

    // Private Variables
    
    // Stores the current min distance - the closest actor to the goal node
    float actorMinDistance = Mathf.Infinity;

    // Stores the current max distance
    //float actorMaxDistance = Mathf.Infinity;
        
    /*
    // Array of actors in their respective positions
    private TravelerController[] actorPositionArray;


    // 0th element = closest to goal
    // last element = farthest from goal
    // Array of total actor distances
    private float[] actorDistances;
    */

    private TravelerController actorInFirst;		// Stores the current actor in first place


	// Use this for initialization
	void Start () 
	{
		// If the simulation is in racing mode
		if (RacingMode)
		{
			// Set each actor to the same goal node so we can race each actor to the finish
			for (int i = 0; i < Actors.Length; i++)
			{
				// Sets each actor's goal node to this goal node
				Actors[i].GoalNode = GoalNode;
			}

            /*
            // Initializes the Actor Array and the Distances Array
            actorPositionArray = new TravelerController[Actors.Length];
            actorDistances = new float[Actors.Length];

            // Sets all the actorDistance values to mathf.infinity
            for (int i = 0; i < actorDistances.Length; i++)
            {
                actorDistances[i] = Mathf.Infinity;
            }*/

            // Ensure the UI has the places of each actor
        }
		// Else, turn off racing mode

	
	}
	
	// Update is called once per frame
	void Update () 
	{
		

		for (int i = 0; i < Actors.Length; i++)
		{
            
			// If the actors current position - the goal nodes position is < minDistance
			if (Vector3.Distance(Actors[i].transform.position, GoalNode.transform.position) < actorMinDistance)
			{
				// Actor in first is the current actor
				actorInFirst = Actors[i];

				// Stores the minimum distance
				actorMinDistance = Vector3.Distance(Actors[i].transform.position, GoalNode.transform.position);
			}
            
        }

        // Prints the actor in first place
        uiPosText.text = "First: " + actorInFirst;

        // Changes the color of the position text respectivley by the color of the leading actor
        uiPosText.color = actorInFirst.GetComponentInChildren<MeshRenderer>().material.color;
    }

	// Garunteed to run after everythuing has been ran inside of update
	void LateUpdate()
	{
        //Vector3 midpointOffset;

		// moves the camera to the location of the first place actor
		firstPlaceCam.transform.position = actorInFirst.transform.position + Vector3.up * 15;

        // Moves the camera to the midpoint of the group
        //midpointOffset = actorPositionArray[0].transform.position - actorPositionArray[4].transform.position;
        //midPlaceCam.transform.position = midpointOffset + Vector3.up * 15;
	}
}
