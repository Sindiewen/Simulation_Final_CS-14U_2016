using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class VertexScript : MonoBehaviour
{
	public EdgeScript[] Edges = new EdgeScript[6];

	void Start()
	{
		GameObject singletons = GameObject.Find ("Singletons");
		
		Renderer renderer = GetComponent<Renderer> ();
		renderer.sharedMaterial = singletons.GetComponent<GraphVisuals> ().GetVertexMaterialByHeight (transform.position.y);

		
		// ONLY ENABLE IN EDIT MODE TO UPDATE YOUR VERTEX-EDGE CONNECTIONS
		// if there is a problem with the links between edges and vertices in edit mode
		// then comment out this section and follow instructions in EdgeScript.Start()
		Edges = new EdgeScript[6];
		Collider[] colliders = Physics.OverlapSphere (transform.position, transform.localScale.x * 0.5f);
		for (int i = 0; i < colliders.Length; i++)
		{
			CheckAndAddEdgeToVertex (colliders [i], true);
		}
		
	}

	/*void OnGUI(){
		Vector3 getPixelPos = Camera.main.WorldToScreenPoint(transform.position);
		getPixelPos.y = Screen.height - getPixelPos.y;
		GUI.Label( new Rect(getPixelPos.x,getPixelPos.y,200f,100f) , gameObject.name);
	}*/

	void OnTriggerEnter(Collider other)
	{
		CheckAndAddEdgeToVertex (other);
	}

	void OnTriggerExit(Collider other)
	{
		CheckAndRemoveEdgeFromVertex (other);
	}

	public void CheckAndAddEdgeToVertex(Collider other, bool editMode = false)
	{
		if (!other) // shorthand for (other == null)
		{
			return;
		}

		EdgeScript edge = other.GetComponent<EdgeScript> ();
		if (edge)
		{
			if (editMode)
			{
				// update the edge to link to this vertex, but only in edit mode
				edge.CheckAndAddVertexToEdge (GetComponent<Collider> ());
			}
			for (int i = 0; i < Edges.Length; i++)
			{
				if (Edges [i] == edge)
				{
					// if this edge is already in the list there is no need to add it a second time
					break;
				}
				if (Edges [i] == null)
				{
					// add edge to first empty slot in the list
					Edges [i] = edge;
					break;
				}
			}
		}
	}

	public void CheckAndRemoveEdgeFromVertex(Collider other, bool editMode = false)
	{
		if (!other) // shorthand for (other == null)
		{
			return;
		}

		EdgeScript edge = other.GetComponent<EdgeScript> ();
		if (edge)
		{
			if (editMode)
			{
				// update the edge that links to this vertex, but only in edit mode
				edge.CheckAndRemoveVertexFromEdge (GetComponent<Collider> ());
			}
			for (int i = 0; i < Edges.Length; i++)
			{
				if (Edges [i] == edge)
				{
					// remove the edge from the list and move all following edges in the list up one spot
					for (int j = i + 1; j < Edges.Length; j++)
					{
						Edges [j - 1] = Edges [j];
					}
					Edges [Edges.Length - 1] = null;
					break;
				}
			}
		}
	}
}
