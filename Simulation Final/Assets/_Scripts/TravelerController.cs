using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
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

	public int currentBPM;					    // Stores the actor's current BPM
	public int idleDefaultBPM;				    // Stores the actor's idleBPM - DEFAULT BPM OF ACTOR
												// The current BPM when the actor is doing nothing

	[Range(40, 220)] public int maxBPM;			// Threshold: Stores a value of the actor's max BPM before they 
												// can/cannot continue anymore untill they reach their idleBPM

	public float actorIdleTime;		    			// Value for how many seconds to decrement a bpm
	[Range(1, 5)] public int BPMDecrementValue;		// How many BPM to decrement per second

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
	public VertexScript NextNode;	    // Stores the Next node
    public VertexScript CurrentNode;    // Stores the current Node

	public Vector3 FacingDirection;

	/////////////////////////////////////////////////
	//////* Private Variables - Do Not change *//////
	/////////////////////////////////////////////////

	/* Private Heartrate Values */
	// Dijkstra values
	private const float DELTA = 0.01f; // any two values this close are effectively the same value
	private PathData path;
	private Dijkstra algorithm;

    // Variables to start Dijkstra
    private bool dijkstraIsRunning;
    private bool dijkstraHasPath;

    // If decrementing currentBPM has ran already
    private bool decrementHasRan;
 

    // Temp Values:
    // For swapping node values
    private VertexScript tempGoalNode;
    private VertexScript tempStartNode;


	void Start()
	{
        // Variable initialization
        // Dijkstra Values

        // Dijkstra is not running;
        dijkstraIsRunning = false;

        // Dijkstra has does not have a path yet
        dijkstraHasPath = false;

        


        // decrement has ran
        decrementHasRan = false;




		// Calculates the actor's safe BPM
		safeBPM = maxBPM - actorAge;


		//algorithm.Actor = this;
		algorithm = new Dijkstra(this);


		// Sets the current bpm to the idle
		currentBPM = idleDefaultBPM;
		Debug.Log("Start: " + currentBPM);
	}

    // Ensures when the game quits (either game window closed, or editor play mode ended...
    // CurrentBPM is reset to 0.
    void OnApplicationWQuit()
    {
        currentBPM = 0;
    }

	///////////////////////////////////
	/// Here Lies the Dijkstra Call ///
	///////////////////////////////////

	// Update is called once per frame
	void Update () 
	{

        // Always checks to ensure the actor is at a safeBPM
        safeBPMCheck();

		// If there is a startNode and a Goal node
		if (StartNode != null && GoalNode != null)
		{
			// have somewhere to go
			if (path == null || path.StartNode != StartNode || path.GoalNode != GoalNode)
			{

                // Checks if Dijkstra has already ran before
                if (dijkstraIsRunning == false && dijkstraHasPath == false)
                {
                    // runs Dijkstra's algorithm
                    runDijkstra();
                }


                


				if (path != null && path.GetCurrentVertex () != null && StartAtStartNode)
				{
					transform.position = path.GetCurrentVertex ().transform.position;
					StartAtStartNode = false;
				}
			}



            /*
            // Sets the current node as the start node
            if (CurrentNode == GoalNode && )
            {
                StartNode = GoalNode;
            }
            // If (The actor has arrived at the goal node)
            // - Set startNode to Goal Node
            //
            */
		}
		else
		{
			// have nowhere to go
			path = null;
		}


        // If the actor can move
		if (Move && path != null && path.GetCurrentVertex() != null)
		{
			VertexScript nextNode = path.GetCurrentVertex (); // <- In PathData.cs, returns the current vertex

			NextNode = path.GetCurrentVertex();			// returns the current vertex the actor is at
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


    // Moved running dijkstra inside this function to allow re-running easily
    void runDijkstra()
    {
        // Dijkstra is running
        dijkstraIsRunning = true;

        // either have no path yet or path data is stale (no longer correct) so get new path
        path = algorithm.GetPath(StartNode, GoalNode, TravelerProfileCatalog.GetProfile(Type), CostMethod);

        //Dijkstra is not running
        dijkstraIsRunning = false;

        // Actor now has a path to use
        dijkstraHasPath = true;

    }

    void safeBPMCheck()
    {
        // If the currentBPM is >= to the maxBPM the actor can have
        if (currentBPM >= maxBPM)
        {

            // Sets the current node to the start node
            StartNode = CurrentNode;

            // If the decrement coroutine has ran or not 
            if (decrementHasRan == false)
            {
                // Sets decrementHasRan to true
                decrementHasRan = true;

                // Actor cannot move
                Move = false;

                // Starts BPMdecrement Coroutine
                StartCoroutine(DecrementCurrentBPMToSafeBPM());
            }
       }
    }

    IEnumerator DecrementCurrentBPMToSafeBPM()
    {
        // Decrements currentBPM untill at a safeBPM
        while (currentBPM >= safeBPM)
        {
            // Waits a specified time before the actor's BPM can decrement
            yield return new WaitForSeconds(actorIdleTime);

            // Decrements the actors currentBPM by the BPMDecrementValue
            currentBPM -= BPMDecrementValue;

            // Prints value to console
            Debug.Log("New CurrentBPM: " + currentBPM);
        }
        // Reruns dijkstra to ensure we get a newer, more updated path
        runDijkstra();

        // The actor can move again
        Move = true;

        // Resets decrement has ran to false
        decrementHasRan = false;
    }


    void OnTriggerEnter(Collider col)
    {
        // Gets the current node
        if(col.gameObject.tag == ("Node"))
        {
            GameObject thisCurrentNode = col.gameObject;
            CurrentNode = thisCurrentNode.GetComponent<VertexScript>();
        }
    }


    // When the actor Exits the trigger another object
    void OnTriggerExit(Collider col)
    {
        // If the Actor colides with an object of tag "Edge"
        if(col.gameObject.tag == ("Edge"))
        {
            // Creates a Collided Edge GameObject
            GameObject collidedEdge = col.gameObject;

            // Gets the EdgeScript component of the object and passes it's cost to the setCurBPM function
            setCurBPM(collidedEdge.GetComponent<EdgeScript>().costBPM);
        }

        
    }

	// Sets the current BPM
	void setCurBPM(int cost)
	{
		// TODO: Ask Erez how to scale the base cost value to BPM

		// Gives both positive and negative numbers
		currentBPM += cost;
		//Debug.Log("CurrentBPM: " + currentBPM);
	}

    
	


}














