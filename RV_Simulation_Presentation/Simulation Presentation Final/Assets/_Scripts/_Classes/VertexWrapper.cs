using System;

public class VertexWrapper : IPrioritizable
{
	public VertexScript Vertex;
	public float LowestCostSoFar;
	public EdgeWrapper LowestCostEdgeSoFar;
	public int Depth;
	
	public VertexWrapper (VertexScript Vertex)
	{
		this.Vertex = Vertex;
		this.LowestCostSoFar = float.PositiveInfinity;
		this.LowestCostEdgeSoFar = null;
		this.Depth = 0;
	}

	public int Priority
	{
		// multiply by 1,000 to get more granularity in the cost
		get { return (int)(LowestCostSoFar * 1000f); }
	}
}