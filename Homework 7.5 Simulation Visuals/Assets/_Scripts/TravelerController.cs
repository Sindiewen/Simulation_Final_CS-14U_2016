using UnityEngine;
using System.Collections;

public class TravelerController : MonoBehaviour {

	public TravelerProfileCatalog.TravelerType Type = TravelerProfileCatalog.TravelerType.Neutral;
	public Dijkstra.CostMethodType CostMethod = Dijkstra.CostMethodType.DistanceTrue;

	public bool Move = false;
	public bool StartAtStartNode = true;
	[Range(0f, 10f)]public float Speed = 1;

	public VertexScript StartNode;
	public VertexScript GoalNode;

	public Vector3 FacingDirection;

	private const float DELTA = 0.01f; // any two values this close are effectively the same value

	private PathData path;
	private Dijkstra algorithm = new Dijkstra();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
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
			VertexScript nextNode = path.GetCurrentVertex ();
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
}














