using UnityEngine;
using System.Collections;

public class GraphVisuals : MonoBehaviour
{
	public Material[] VertexMaterialsByHeight
						= new Material[5];

	public Material EdgeUnvisitedMaterial;
	public Material EdgeVisitedMaterial;
	public Material EdgeInQueueMaterial;
	public Material ShortestPathMaterial;

	public float EdgeThickness = 0.5f;

	private bool setGUIStyle = false;
	private Texture2D labelTexture;

	void Start()
	{
		labelTexture = MakeSolidTexture (2, 2, Color.black);
	}

	void OnGUI()
	{
		if (!setGUIStyle && labelTexture)
		{
			setGUIStyle = true;
			GUIStyle costLabelStyle = GUI.skin.box;
			costLabelStyle.alignment = TextAnchor.MiddleCenter;
			costLabelStyle.fontSize = 20;
			costLabelStyle.fontStyle = FontStyle.Bold;
			costLabelStyle.normal.background = labelTexture;
			costLabelStyle.normal.textColor = Color.white;
			GUI.skin.box = costLabelStyle;
		}
	}

	public Material GetVertexMaterialByHeight(float y)
	{
		float heightAsPercentage = (y - GraphConstants.MIN_Y)
			  / (GraphConstants.MAX_Y - GraphConstants.MIN_Y);
		float scaledByMaterialsCount = heightAsPercentage * VertexMaterialsByHeight.Length;
		int index = Mathf.Clamp(
			(int)scaledByMaterialsCount,
			0,
			VertexMaterialsByHeight.Length - 1);
		return VertexMaterialsByHeight[index];
	}

	public void StretchEdgeBetweenTwoVertices(VertexScript vertexA, VertexScript vertexB, EdgeScript edge)
	{
		Vector3 leftVertexPosition = vertexA.transform.position;
		Vector3 rightVertexPosition = vertexB.transform.position;
		edge.transform.position = Vector3.Lerp (leftVertexPosition, rightVertexPosition, 0.5f);
		Vector3 positionDifferences = rightVertexPosition - leftVertexPosition;
		edge.transform.localScale = new Vector3(EdgeThickness, positionDifferences.magnitude * 0.5f, EdgeThickness);
		edge.transform.rotation = Quaternion.LookRotation (positionDifferences);
		edge.transform.Rotate (new Vector3 (90, 0, 0));
	}

	private Texture2D MakeSolidTexture(int width, int height, Color color)
	{
		Color[] pixels = new Color[width * height];
		for( int i = 0; i < pixels.Length; ++i )
		{
			pixels[i] = color;
		}
		Texture2D result = new Texture2D(width, height);
		result.SetPixels(pixels);
		result.Apply();
		return result;
	}
}
