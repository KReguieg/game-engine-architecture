using UnityEngine;
using System.Collections;
 
 
public class OverridePlaneMesh : MonoBehaviour
{
    public int widthSegments = 1;
    public int lengthSegments = 1;

	float width = 10;
	float length = 10;
 
    void Start()
    {
		ModifyPlane();
    }
 
    public void ModifyPlane()
    {
        GameObject plane = this.gameObject;
		MeshFilter meshFilter = plane.GetComponent<MeshFilter>();

        Mesh m = new Mesh();

        int hCount2 = widthSegments+1;
        int vCount2 = lengthSegments+1;
        int numTriangles = widthSegments * lengthSegments * 6; // 6 = 2*3
        int numVertices = hCount2 * vCount2;

        Vector3[] vertices = new Vector3[numVertices];
        Vector2[] uvs = new Vector2[numVertices];
        int[] triangles = new int[numTriangles];

        int index = 0;
        float uvFactorX = 1.0f/widthSegments;
        float uvFactorY = 1.0f/lengthSegments;
        float scaleX = width/widthSegments;
        float scaleY = length/lengthSegments;
        for (float y = 0.0f; y < vCount2; y++)
        {
            for (float x = 0.0f; x < hCount2; x++)
            {
                vertices[index] = new Vector3(x * scaleX - width/2f, 0.0f, y*scaleY - length/2f);
                uvs[index++] = new Vector2(x*uvFactorX, y*uvFactorY);
            }
        }

        index = 0;
        for (int y = 0; y < lengthSegments; y++)
        {
            for (int x = 0; x < widthSegments; x++)
            {
                triangles[index]   = (y     * hCount2) + x;
                triangles[index+1] = ((y+1) * hCount2) + x;
                triangles[index+2] = (y     * hCount2) + x + 1;

                triangles[index+3] = ((y+1) * hCount2) + x;
                triangles[index+4] = ((y+1) * hCount2) + x + 1;
                triangles[index+5] = (y     * hCount2) + x + 1;
                index += 6;
            }
        }

        m.vertices = vertices;
        m.uv = uvs;
        m.triangles = triangles;
        m.RecalculateNormals();

        meshFilter.sharedMesh = m;
        m.RecalculateBounds();
    }
}