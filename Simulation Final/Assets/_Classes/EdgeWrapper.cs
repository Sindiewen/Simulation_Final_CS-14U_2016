using System;

public class EdgeWrapper
{
	public EdgeScript Edge;
	public float Cost;

	public EdgeWrapper (EdgeScript Edge, float Cost = float.PositiveInfinity)
	{
		this.Edge = Edge;
		this.Cost = Cost;
	}
}