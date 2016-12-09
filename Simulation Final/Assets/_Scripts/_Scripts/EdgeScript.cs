using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class EdgeScript : MonoBehaviour
{

    // Shows what vertex is connected to the edge
    [Header("Connected Vertices")]
    public VertexScript VertexA;
    public VertexScript VertexB;

    [Header("Heartrate Cost")]
    public int costBPM;         // How much each edge is going to cost per movement

    [Header("Actor Traversal")]
    // 1 = does not like, -1 = likes
    [Range(-1f, 1f)] public float HikingFriendly = 0f;
    [Range(-1f, 1f)] public float BodyBuilding = 0f;
    [Range(-1f, 1f)] public float Beauty = 0f;
    [Range(-1f, 1f)] public float DirectRoute = 0f;


    [Header("Edge Stretch")]
    // Weather the edge can stretch
    public bool Stretch = true;

    void Start()
    {
        // Changes the name of each edge object to the coresponding edge location in 3D space
        this.name = ("Edge: " + transform.position);


        /* SEE INSTRUCTIONS IN VertexScript.Start() REGARDING EDGE-VERTEX CONNECTIONS
		 * if there is a problem with the links between edges and vertices in edit mode then
		 * 1) enable this line of code and reload the scene to clear out the vertex links from the edges.
		 * 2) comment out the line of code again and reload the scene a second time to reconnect edges and vertices.
		 */
        // VertexA = VertexB = null;

        GameObject singletons = GameObject.Find("Singletons");

        
        Renderer renderer = GetComponent<Renderer>();
        renderer.sharedMaterial = singletons.GetComponent<GraphVisuals>().EdgeUnvisitedMaterial;

        // Creates a Cost by getting the distance between the edges 2 adjacent nodes
		// NOTE: Should only be ran once. Once the edge cost between the nodes have been calculated, comment this line out.
        //costBPM = (int)Vector3.Distance(VertexA.GetComponent<Transform>().localPosition, VertexB.GetComponent<Transform>().localPosition) + 2;
        
    }

    void Update()
    {
        if (Stretch && VertexA && VertexB)
        {
            StretchEdgeBetweenTwoVertices(VertexA.transform.position, VertexB.transform.position);
        }
    }

    // Stretches the edge between 2 vertices
    public void StretchEdgeBetweenTwoVertices(Vector3 vertexPositionA, Vector3 vertexPositionB)
    {
        transform.position = Vector3.Lerp(vertexPositionA, vertexPositionB, 0.5f);
        Vector3 positionDifferences = vertexPositionB - vertexPositionA;
        transform.localScale = new Vector3(0.25f, positionDifferences.magnitude * 0.5f - 0.25f, 0.25f);
        transform.rotation = Quaternion.LookRotation(positionDifferences);
        transform.Rotate(90, 0, 0);
    }

    // On Trigger Enter: Checks is edge is connected to a vertex
    void OnTriggerEnter(Collider col)
    {
        CheckAndAddVertexToEdge(col);
       
    }

    // On Trigger Exit: Removes vertex from edge if not connected
    void OnTriggerExit(Collider other)
    {
        CheckAndRemoveVertexFromEdge(other);
    }

    //
    public void CheckAndAddVertexToEdge(Collider other)
    {
        if (!other) // shorthand for (other == null)
        {
            return;
        }

        VertexScript vertex = other.GetComponent<VertexScript>();
        if (vertex) // shorthand for (vertex != null)
        {
            if (VertexA == null && VertexB != vertex)
            {
                VertexA = vertex;
            }
            else if (VertexB == null && VertexA != vertex)
            {
                VertexB = vertex;
            }
            else if (VertexA && VertexB)
            {
                System.Console.WriteLine("ERROR: Edge with two vertices intersecting new vertex in OnTriggerEnter.");
            }
        }
    }

    //
    public void CheckAndRemoveVertexFromEdge(Collider other)
    {
        if (!other) // shorthand for (other == null)
        {
            return;
        }

        VertexScript vertex = other.GetComponent<VertexScript>();
        if (vertex) // shorthand for (vertex != null)
        {
            if (VertexA == vertex)
            {
                VertexA = null;
            }
            else if (VertexB == vertex)
            {
                VertexB = null;
            }
        }
    }

    //
    public VertexScript GetOtherVertex(VertexScript vertex)
    {
        VertexScript value = null;

        if (vertex == null)
        {
            System.Console.WriteLine("ERROR: null vertex in GetOtherVertex.");
        }
        else if (vertex == VertexA)
        {
            value = VertexB;
        }
        else if (vertex == VertexB)
        {
            value = VertexA;
        }
        else
        {
            System.Console.WriteLine("ERROR: vertex not connected to edge in GetOtherVertex.");
        }
        return value;
    }
}