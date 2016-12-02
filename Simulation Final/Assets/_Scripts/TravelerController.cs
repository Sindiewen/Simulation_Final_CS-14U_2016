using UnityEngine;
using System.Collections;

public class TravelerController : MonoBehaviour 
{

	[Header("Actor AI Values")]

	// Dropdown:
	// Type of actor
	public TravelerProfileCatalog.TravelerType Type = TravelerProfileCatalog.TravelerType.Neutral;

	// Dropdown:
	// Cost method for the actor's movement
	public Dijkstra.CostMethodType CostMethod = Dijkstra.CostMethodType.DistanceTrue;

	// How fast the actor will move
	[Range(0f, 10f)]public float Speed = 1;

	// public Actor Heartrate values //
	[Header("Actor Heartrate Values")]
	public int actorAge;						// Stores the actor's current age

	public int currentBPM = 0;					// Stores the actor's current BPM
	public int idleDefaultBPM = 90;				// Stores the actor's idleBPM - DEFAULT BPM OF ACTOR
												// The current BPM when the actor is doing nothing

	[Range(40, 220)] public int maxBPM;			// Threshold: Stores a value of the actor's max BPM before they 
												// can/cannot continue anymore untill they reach their idleBPM

	//public int actorIdleTime;					// How long the actor has to wait before they get to a safe BPM
	[Range(1, 5)] public int timePerBPM;		// Stores value for how long each 1 BPM = 1 second

	public int safeBPM;							// Stores the actor's safe BPM - their (maxBPM - actorAge)
												// A value for when the actor feels "safe" to continue moving

	[Range(0.01f, 1f)]
	public float probabilityOfPreferedRoute;	// Using Percentages: How often an actor will take prefered route
												// over harder route. Higher % = higher chance of harder route

	// AI States
	[Header("AI States")]
	public bool Move = false;
	public bool StartAtStartNode = true;

	// Node Locaitions for start and goal nodes
	[Header("Node Locations")]
	public VertexScript StartNode;		// Stores the start node
	public VertexScript GoalNode;		// Stores the goal node
	public VertexScript currentNode;	// Stores the current node

	public Vector3 FacingDirection;

	/////////////////////////////////////////////////
	//////* Private Variables - Do Not change *//////
	/////////////////////////////////////////////////

	/* Private Heartrate Values */
	// Dijkstra values
	private const float DELTA = 0.01f; // any two values this close are effectively the same value
	private PathData path;
	private Dijkstra algorithm;// = new Dijkstra(this);


	void Start()
	{
		// Variable initialization

		// Gets how long the actor needs to wait before their ok to move again
		//actorIdleTime = timePerBPM;

		// Calculates the actor's safe BPM
		safeBPM = maxBPM - actorAge;


		//algorithm.Actor = this;

		algorithm = new Dijkstra(this);


		// Sets the current bpm to the idle
		currentBPM = idleDefaultBPM;
		Debug.Log("Start: " + currentBPM);
	}


	///////////////////////////////////
	/// Here Lies the Dijkstra Call ///
	///////////////////////////////////

	// Update is called once per frame
	void Update () 
	{
		// If there is not startNode and no Goal node
		if (StartNode != null && GoalNode != null)
		{
			// have somewhere to go
			if (path == null || path.StartNode != StartNode || path.GoalNode != GoalNode)
			{
				// either have no path yet or path data is stale (no longer correct) so get new path
				path = algorithm.GetPath(StartNode, GoalNode, TravelerProfileCatalog.GetProfile(Type), CostMethod);


				if (path != null && path.GetCurrentVertex () != null && StartAtStartNode)
				{
					transform.position = path.GetCurrentVertex ().transform.position;
					StartAtStartNode = false;
				}
			}
		}
		else
		{
			// have nowhere to go
			path = null;
		}

		if (Move && path != null && path.GetCurrentVertex() != null)
		{
			VertexScript nextNode = path.GetCurrentVertex (); // <- In PathData.cs, returns the current vertex

			currentNode = path.GetCurrentVertex();			// returns the current vertex the actor is at
			// TODO: Upon getting current vertex, if the actors heartrate > maxBPM
			// Wait untill heartrate is at safe BPM
			// after reaching safe BPM, continue with movement

			Vector3 nextDestination = nextNode.transform.position;
			Vector3 currentPosition = transform.position;
			Vector3 difference = nextDestination - currentPosition;
			// difference.Normalize (); // scales vector to have length = 1

			FacingDirection = difference;

			float distanceToDest = difference.magnitude;
			if (distanceToDest <= DELTA)
			{
				// we're already there, so just correct the position
				// and advance to the next vertex in the path
				transform.position = nextDestination;
				path.AdvanceToNextVertex ();
			}
			else if (distanceToDest < Speed * Time.deltaTime)
			{
				// we're close enough to arrive there this frame
				transform.position = nextDestination;
			}
			else
			{
				// it'll take more than one frame to arrive there
				Vector3 normalizedVelocity = difference.normalized; // velocity vector
				Vector3 movementThisFrame = Speed * normalizedVelocity * Time.deltaTime;
				transform.position += movementThisFrame;
			}
		}
	}

	// Sets the current BPM
	public void setCurBPM(float cost)
	{
		// TODO:: Create totalEdgeCostBPM[]. Store each cost into an array.
		// EVery time actor traverses an edge, add 1 value to the currentBpm.
		// Remove value from array.

		//TODO: Instead of an array, use a queue (LIFO), so that an 
		//currentBPM += (int)cost;

		// TODO: Ask Erez how to scale the base cost value to BPM

		// Gives both positive and negative numbers
		currentBPM = (int)cost * 2;
		Debug.Log(currentBPM);
	}
	


}














