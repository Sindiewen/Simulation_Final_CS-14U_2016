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

    // Private Variables
    private bool IsCrossingEdge;

    void Start()
    {
        // Changes the name of each edge object to the coresponding edge location in 3D space
        //this.name = ("Edge: " + transform.position);


        /* SEE INSTRUCTIONS IN VertexScript.Start() REGARDING EDGE-VERTEX CONNECTIONS
		 * if there is a problem with the links between edges and vertices in edit mode then
		 * 1) enable this line of code and reload the scene to clear out the vertex links from the edges.
		 * 2) comment out the line of code again and reload the scene a second time to reconnect edges and vertices.
		 */
        // VertexA = VertexB = null;

        GameObject singletons = GameObject.Find("Singletons");

        Renderer renderer = GetComponent<Renderer>();
        renderer.sharedMaterial = singletons.GetComponent<GraphVisuals>().EdgeUnvisitedMaterial;
        

        // Sets flag for crossing edge to false
        //IsCrossingEdge = false;


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
        transform.localScale = new Vector3(0.25f, positionDifferences.magnitude * 0.5f, 0.25f);
        transform.rotation = Quaternion.LookRotation(positionDifferences);
        transform.Rotate(90, 0, 0);
    }

    // On Trigger Enter: Checks is edge is connected to a vertex
    void OnTriggerEnter(Collider col)
    {
        CheckAndAddVertexToEdge(col);

        // Inside Trigger:
        /*
         * When an actor object triggers this edge, Call 'TravelerController.cs.SetCurBPM(costBPM)'.
         * This //SHOULD// pass the current BPM to the edge without any issues of it resetting to 0 or whatnot
         * 
         * 
         

        
        // If Actor Neutal collides with the Edge Collider
        if (col.gameObject.name == ("Neutral") && IsCrossingEdge == false)
        {
            // Add costBpm to actor's total currentBpm

            ActorNeutral.setCurBPM(costBPM);
            IsCrossingEdge = true;
            
            /*
            //Debug.Log("Adding " + costBPM + " To " + ActorNeutral.name + "'s Current BPM");
            Debug.Log("CurrentBPM Originally: " + ActorNeutral.currentBPM);
            ActorNeutral.currentBPM += costBPM;
            Debug.Log("New Current BPM: " + ActorNeutral.currentBPM);
            IsCrossingEdge = true;
            
        }

        else if (col.gameObject.tag == ("Actor Hiker"))
        {
            //ActorHiker.currentBPM = costBPM;
        }

        else if (col.gameObject.tag == ("Actor Body Builder"))
        {
            //ActorBodyBuilder.currentBPM = costBPM;
        }

        else if (col.gameObject.tag == ("Actor Florist"))
        {
            //ActorFlorist.currentBPM = costBPM;
        }

        else if (col.gameObject.tag == ("Actor Business Worker"))
        {
            //ActorBusinessWorker.currentBPM = costBPM;
        }
        */
        
    }

    // On Trigger Exit: Removes vertex from edge if not connected
    void OnTriggerExit(Collider other)
    {
        CheckAndRemoveVertexFromEdge(other);

        /*
        // When the actor has left the edge
        // Actor has stopped crossing the edge
        if (IsCrossingEdge == true)
        {
            // Sets crossing edge to false\
            Debug.Log("Has Left " + this.name);
            Debug.Log(ActorNeutral.name + " left Edge " + this.name + " - new CurrentBPM:" + ActorNeutral.currentBPM);
            IsCrossingEdge = false;
        }
        */
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