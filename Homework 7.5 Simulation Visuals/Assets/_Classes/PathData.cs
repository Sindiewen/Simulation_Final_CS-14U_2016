using System;
using System.Collections.Generic;

public class PathData
{
	private VertexScript startNode;
	private VertexScript goalNode;
	
	private List<VertexScript> vertices;
	private int currentVertexIndex = -1;

	private float cost = 0f;

	public PathData (VertexScript startNode, VertexScript goalNode, float cost)
	{
		vertices = new List<VertexScript> ();
		this.startNode = startNode;
		this.goalNode = goalNode;
		this.cost = cost;
	}

	public void InsertPreviousStep(VertexScript vertex)
	{
		if (currentVertexIndex == -1)
		{
			// only insert a step if we have not yet begun walking down the path
			vertices.Insert (0, vertex);
		}
		else
		{
			// ERROR: shouldn't change path data while traversing path
			UnityEngine.Debug.LogError("ERROR: shouldn't change path data in PathData.InsertPreviousStep() while traversing path.");
		}
	}

	public VertexScript GetCurrentVertex()
	{
		// if the vertices list is empty go ahead and return null without changing the value of currentVertexIndex,
		// but if there are vertices in the list then the first time we call this function move the currentVertexIndex to the first vertex in the list
		if (currentVertexIndex == -1 && vertices.Count > 0)
		{
			currentVertexIndex = 0;
		}
		return (vertices.Count != 0 && currentVertexIndex < vertices.Count ? vertices [currentVertexIndex] : null);
	}

	public void AdvanceToNextVertex()
	{
		if (currentVertexIndex != -1)
		{
			// only advance the value of currentVertexIndex if we are already traversing the list
			currentVertexIndex++;
		}
	}

	public List<VertexScript> GetVertexList()
	{
		return new List<VertexScript> (vertices);
	}

	public VertexScript StartNode
	{
		get
		{
			return startNode;
		}
	}

	public VertexScript GoalNode
	{
		get
		{
			return goalNode;
		}
	}

	public float Cost
	{
		get
		{
			return (vertices.Count != 0 ? cost : float.PositiveInfinity);
		}
	}

	public bool LeadsToGoal
	{
		get
		{
			return (vertices.Count != 0 && vertices [vertices.Count - 1] == goalNode);
		}
	}
}
