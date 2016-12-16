using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ActorPositionManager : MonoBehaviour 
{


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


	[HideInInspector]
	public TravelerController actorInFirst;		// Stores the current actor in first place

    // Private Variables
    
    // Stores the current min distance - the closest actor to the goal node
    float actorMinDistance = Mathf.Infinity;

    // Stores the current max distance

    

	private bool ActorReachedGoalNode;
	private bool actorRunOnce;

	private bool resetActorsGoalNode;


	// Use this for initialization
	void Start () 
	{


		// If the simulation is in racing mode
		if (RacingMode)
		{

			resetActorsGoalNode = true;
			
			if (resetActorsGoalNode)
			{
				goalNodeReset();
			}

			ActorReachedGoalNode = false;
			actorRunOnce = false;

			for (int i = 0; i < Actors.Length; i++)
			{				
				Actors[i].GetComponent<GUIActorBPMDisplay>().GUIWinCount.text = "Wins: " + Actors[i].GetComponent<GUIActorBPMDisplay>().winCount;
			}
            // Ensure the UI has the places of each actor
        }
		// Else, turn off racing mode

	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//TODO : Reset actorMinDistance to infinity when the winning actor reached the goal node
		// 

		for (int i = 0; i < Actors.Length; i++)
		{
            
			// If the actors current position - the goal nodes position is < minDistance
			if (Vector3.Distance(Actors[i].transform.position, GoalNode.transform.position) < actorMinDistance && ActorReachedGoalNode == false)
			{
				// Actor in first is the current actor
				actorInFirst = Actors[i];

				// Stores the minimum distance
				actorMinDistance = Vector3.Distance(Actors[i].transform.position, GoalNode.transform.position);
			}
            
        }

        // Prints the actor in first place
        uiPosText.text = "1st: " + actorInFirst;

        // Changes the color of the position text respectivley by the color of the leading actor
        uiPosText.color = actorInFirst.GetComponentInChildren<MeshRenderer>().material.color;


		// If the actor is in first place and they reached the goal node
		if (actorInFirst.CurrentNode == GoalNode)
		{
			ActorReachedGoalNode = true;
			resetActorsGoalNode = true;


			if (ActorReachedGoalNode == true && actorRunOnce == true)
			{
				//Debug.Log(actorInFirst + "Has won");
				actorInFirst.GetComponent<GUIActorBPMDisplay>().addToWinCount();
				//actorInFirst.GetComponent<GUIActorBPMDisplay>().GUIWinCount.text = "Wins: " + actorInFirst.GetComponent<GUIActorBPMDisplay>().winCount + 1;
				//actorMinDistance = Mathf.Infinity;
				ActorReachedGoalNode = actorRunOnce = false;
			}
		}
		else
		{
			//ActorReachedGoalNode = false;
			actorRunOnce = true;
			goalNodeReset();
		}

		// If the actors have a new goal node


    }

	// Garunteed to run after everythuing has been ran inside of update
	void LateUpdate()
	{
		// moves the camera to the location of the first place actor
		firstPlaceCam.transform.position = actorInFirst.transform.position + Vector3.up * 15;

	}

	void goalNodeReset()
	{
		// if actors has new goal node

		// Set each actor to the same goal node so we can race each actor to the finish
		for (int i = 0; i < Actors.Length; i++)
		{
			if (Actors[i].StartNode != GoalNode)
			{
				ActorReachedGoalNode = false;
			}

			// Sets each actor's goal node to this goal node
			Actors[i].GoalNode = GoalNode;
			actorMinDistance = Mathf.Infinity;
			resetActorsGoalNode = false;
		} 
	}
}
