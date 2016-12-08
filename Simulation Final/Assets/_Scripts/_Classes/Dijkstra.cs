using UnityEngine; // needed for Debug.Assert, Mathf, and Vector3
using System;
using System.Collections.Generic;

public class Dijkstra
{

	private TravelerController actor { get; set; }
	public TravelerController Actor
	{
		get
		{
			return actor;
		}
		set
		{
			actor = value;
		}
	}


	public enum CostMethodType { ClosestToCompass, DistanceTrue, DistanceHorizontal, DistanceVertical, NumberOfStops, PeakLover, SmoothTurns, 
	Steepness, UpSucksDownOkay };


	public Dijkstra (TravelerController TC)
	{
		// initialize the traveler profile catalog
		TravelerProfileCatalog.Initialize();

		Actor = TC;
	}

	public PathData GetPath (
		VertexScript startNode,
		VertexScript goalNode,
		TravelerProfile traveler = null,
		CostMethodType costMethod = CostMethodType.Steepness, 	// <- Default cost method
		int maxDepth = int.MaxValue)
	{
		PathData data = null;

		if (startNode == null || goalNode == null)
		{
			return null;
		}
		else if (startNode == goalNode)
		{
			Debug.Log ("ERROR: startNode == goalNode in GetPath.");
			return null;
		}

		if (traveler == null)
		{
			traveler = TravelerProfileCatalog.GetProfile(TravelerProfileCatalog.TravelerType.Neutral); // <- Setting default to neutral
		}

		PriorityQueue<VertexWrapper> priorityQueue = new PriorityQueue<VertexWrapper> ();
		Dictionary<VertexScript, VertexWrapper> vertexDict = new Dictionary<VertexScript, VertexWrapper> ();
		Dictionary<EdgeScript, EdgeWrapper> edgeDict = new Dictionary<EdgeScript, EdgeWrapper> ();

		EdgeWrapper edgeWrapper;
		bool reachedGoal = false;

		VertexWrapper nodeWrapper = GetOrMakeVertexWrapper (startNode, vertexDict);
		nodeWrapper.LowestCostSoFar = 0;
		priorityQueue.Enqueue (nodeWrapper);

		while (!reachedGoal && priorityQueue.Count != 0)
		{
			VertexWrapper tempWrapper = priorityQueue.Dequeue ();
			if (tempWrapper != null)
			{
				nodeWrapper = tempWrapper;
				if (nodeWrapper.Vertex == goalNode)
				{
					reachedGoal = true;
					break;
				}
				if (nodeWrapper.Depth == maxDepth)
				{
					break;
				}
				VertexScript currentNode = nodeWrapper.Vertex;
				foreach (EdgeScript edge in currentNode.Edges)
				{
					if (edge == null)
						continue;

					VertexScript otherNode = edge.GetOtherVertex (currentNode);
					VertexWrapper otherNodeWrapper = GetOrMakeVertexWrapper(otherNode, vertexDict);
					if (otherNode != null)
					{

						// returns the edge cost //
						float cost = GetCost (nodeWrapper, otherNodeWrapper, edge, costMethod, traveler, startNode, goalNode);
						//actor.setCurBPM(cost);

						// // // // // // // // //

						Debug.Assert (cost >= 0f, "ERROR: Dijkstra's Algorithm does not accept negative edge weights.");

						float costOfPathPlusThisEdge = nodeWrapper.LowestCostSoFar + cost;
						if (costOfPathPlusThisEdge < otherNodeWrapper.LowestCostSoFar)
						{
							edgeWrapper = GetOrMakeEdgeWrapper (edge, edgeDict);
							edgeWrapper.Cost = cost;
							otherNodeWrapper.LowestCostSoFar = costOfPathPlusThisEdge;
							otherNodeWrapper.LowestCostEdgeSoFar = edgeWrapper;
							otherNodeWrapper.Depth = nodeWrapper.Depth + 1;
							priorityQueue.Enqueue (otherNodeWrapper);
						}
					}
				}
			}
		}

		data = new PathData (startNode, goalNode, nodeWrapper.LowestCostSoFar);
		data.InsertPreviousStep (nodeWrapper.Vertex);

		// traverse the shortest path from the goalNode backwards to the startNode
		edgeWrapper = nodeWrapper.LowestCostEdgeSoFar;
		while (edgeWrapper != null)
		{
			nodeWrapper = GetOrMakeVertexWrapper(edgeWrapper.Edge.GetOtherVertex (nodeWrapper.Vertex), vertexDict);
			edgeWrapper = nodeWrapper.LowestCostEdgeSoFar;
			data.InsertPreviousStep (nodeWrapper.Vertex);
		}
		Debug.Assert (nodeWrapper.Vertex == startNode, "ERROR: shortest path did not terminate at startNode in MakeShortestPath.");

		priorityQueue.Clear ();
		vertexDict.Clear ();
		edgeDict.Clear ();

		return data;
	}



	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // //
	// TODO: Take this specific function, Modify it to only calculate BPM cost
	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // //
	public float GetCost(VertexWrapper fromNodeWrapper, VertexWrapper toNodeWrapper, EdgeScript edge, CostMethodType method, TravelerProfile Traveler = null, VertexScript startNode = null, VertexScript goalNode = null)
	{
		VertexScript fromNode = fromNodeWrapper.Vertex;
		VertexScript toNode = toNodeWrapper.Vertex;

		// the baseCost is our interpretation of the physical cost of travel, ignoring any personality traits
		float baseCost = 0f;

		switch (method)
		{
		case CostMethodType.ClosestToCompass:
			// pick the path with the smallest angle compared to the vector between you and the destination
			Vector3 differenceForEdge = toNode.transform.position - fromNode.transform.position;
			Vector3 differenceForGoal = goalNode.transform.position - fromNode.transform.position;
			baseCost = Mathf.Abs (Vector3.Angle (differenceForEdge, differenceForGoal));
			break;

		case CostMethodType.DistanceHorizontal:
			// use only the pure horizontal distance, ignoring any vertical distance
			Vector3 difference = toNode.transform.position - fromNode.transform.position;
			difference.y = 0;
			baseCost = difference.magnitude;
			break;

		case CostMethodType.DistanceTrue:
			// use the typical 3D distance between the two points
			baseCost = (toNode.transform.position - fromNode.transform.position).magnitude;
			break;

		case CostMethodType.DistanceVertical:
			// use only the pure vertical distance, ignoring any horizontal distance
			// this is equivalent to saying "walking on flat ground is easy, but any change in elevation is hard"
			baseCost = Mathf.Abs (toNode.transform.position.y - fromNode.transform.position.y);
			break;

		case CostMethodType.NumberOfStops:
			// counts the number the vertices between the start and the goal nodes
			baseCost = 1f;
			break;

		case CostMethodType.PeakLover:
			// for people who enjoy walking along the tops of hills and mountains so they can enjoy the view
			// there is a cost for going down, but not for going up
			baseCost = Mathf.Max (0f, fromNode.transform.position.y - toNode.transform.position.y);
			break;

		case CostMethodType.SmoothTurns:
			// when you're driving very fast or driving a very large vehicle it's important that your turns are smooth
			// the cost is smaller if the edge is a straight-forward continuation of the last edge and larger if you have to make a sharp turn
			if (fromNodeWrapper.LowestCostEdgeSoFar != null)
			{
				// all nodes except the start node will have a "lowest cost edge so far", so we can use it
				Vector3 differenceNext = toNode.transform.position - fromNode.transform.position;
				Vector3 differencePrevious = fromNode.transform.position - fromNodeWrapper.LowestCostEdgeSoFar.Edge.GetOtherVertex (fromNode).transform.position;
				// ignore changes in elevation so that only horizontal turning is taken into account
				differenceNext.y = 0;
				differencePrevious.y = 0;
				// the cost should be higher if the angle is larger or if the distance traveled is shorter (i.e. it's a sharper turn)
				// the cost should be lower if the angle is smaller or if the distance traveled is larger (i.e. it's a long smooth turn)
				baseCost = Mathf.Abs (Vector3.Angle (differencePrevious, differenceNext)) / (differencePrevious.magnitude + differenceNext.magnitude);
			}
			else
			{
				// the start node has no "lowest cost edge so far" so just use 0 (no need to turn when leaving the start node)
				baseCost = 0f;
			}
			break;


			///////////////////
		case CostMethodType.Steepness:	// <- Use this for calculating edge BPM Cost
			// the cost is the angle that the edge makes with the horizon
			// so flat paths are good but the steeper the edge (going up or going down) the more it costs
			Vector3 differenceFull = toNode.transform.position - fromNode.transform.position;
			Vector3 differenceFlat = new Vector3 (differenceFull.x, differenceFull.z, 0f);
			baseCost = Mathf.Abs (Vector3.Angle (differenceFlat, differenceFull));
			break;

		case CostMethodType.UpSucksDownOkay:	// <- work with both
			// for people who hate walking uphill but don't mind going downhill
			// there is a cost for going up but no cost for going down
			baseCost = Mathf.Max (0f, toNode.transform.position.y - fromNode.transform.position.y);
			break;
			////////////////////


		default:
			// if nothing else, just use the physical length of the edge
			baseCost = (edge.transform.localScale.y * 2f);
			break;
		}
		// end of baseCost calculation

		// retrieve the traveler profile, then check if the profile is null to prevent errors
		if (Traveler == null)
		{
			Traveler = TravelerProfileCatalog.GetProfile (TravelerProfileCatalog.TravelerType.Neutral);
		}

        // if profile is not null use the average of the trait indexes (which will be -1 to 1)
        // but if profile is null then just use zero
        /*
		float compatibility = (Traveler != null
			?  (Traveler.LikesToWalk * edge.PedestrianFriendly +
				Traveler.LikesToBicycle * edge.BicyclistFriendly +
				Traveler.LikesToDrive * edge.CarFriendly +
				Traveler.LikesVistas * edge.Beautiful +
				Traveler.LikesFood * edge.FoodAvailable +
				Traveler.LikesHeart * edge.Heart) / 5f

			: 0f);
            */
        float compatibility = (Traveler != null ?
            (
                Traveler.LikesHiking * edge.HikingFriendly +
                Traveler.LikesBodyBuilding * edge.BodyBuilding +
                Traveler.LikesBeauty * edge.Beauty +
                Traveler.LikesBusiness * edge.DirectRoute
            ) / 5f 
                : 0f);

		// (1 - compatibility) gives a value that is 0 (good) to 2 (bad)
		// this translates to:
		//   -- cost is reduced when the traveler likes the edge
		//   -- cost is not affected when the traveler has no opinion of the edge
		//   -- cost is increased when the traveler dislikes the edge
		float personalityCost = (1f - compatibility);

		// using personalityCost * baseCost means "the traveler's emotions can make anything easy or anything hard"
		return (personalityCost * baseCost);
	}
	/*
	 * 
	 * 
	 */









	private VertexWrapper GetOrMakeVertexWrapper(VertexScript vertex, Dictionary<VertexScript, VertexWrapper> vertexDict)
	{
		VertexWrapper wrapper;
		if (!vertexDict.TryGetValue (vertex, out wrapper))
		{
			wrapper = new VertexWrapper (vertex);
			vertexDict [vertex] = wrapper;
		}
		return wrapper;
	}

	private EdgeWrapper GetOrMakeEdgeWrapper(EdgeScript edge, Dictionary<EdgeScript, EdgeWrapper> edgeDict)
	{
		EdgeWrapper wrapper;
		if (!edgeDict.TryGetValue (edge, out wrapper))
		{
			wrapper = new EdgeWrapper (edge);
			edgeDict [edge] = wrapper;
		}
		return wrapper;
	}





}