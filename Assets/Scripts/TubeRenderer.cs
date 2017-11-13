using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class TubeRenderer : MonoBehaviour
{
    /*
    TubeRenderer.cs
 
    This script is created by Ray Nothnagel of Last Bastion Games. It is 
    free for use and available on the Unify Wiki.
 
    For other components I've created, see:
    http://lastbastiongames.com/middleware/
 
    (C) 2008 Last Bastion Games
    */
    // see http://wiki.unity3d.com/index.php?title=TubeRenderer

    public Vector3[] vertices;
    public Material material;

    public int crossSegments = 10;

    internal void SetSize(int length)
    {
        vertices = new Vector3[length];
        meshVertices = new Vector3[vertices.Length * crossSegments];
        uvs = new Vector2[vertices.Length * crossSegments];
        colors = new Color[vertices.Length * crossSegments];
        tris = new int[vertices.Length * crossSegments * 6];
        lastVertices = new int[crossSegments];
        theseVertices = new int[crossSegments];
    }

    private Vector3[] crossPoints;
    private int lastCrossSegments;
    public AnimationCurve thickness;

    Color color = Color.white;
    
    #region MeshParts
    Mesh mesh;
    Vector3[] meshVertices;
    Vector2[] uvs;
    Color[] colors;
    int[] tris;
    int[] lastVertices;
    int[] theseVertices;
    #endregion
    void Reset()
    {
        vertices = new Vector3[] { Vector3.zero, Vector3.one };
    }
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        if (!mesh)
        {
            mesh = new Mesh();
        }
        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        mr.material = material;
    }    



    void LateUpdate()
    {
        if (null == vertices ||
            vertices.Length <= 1)
        {
            GetComponent<Renderer>().enabled = false;
            return;
        }
        GetComponent<Renderer>().enabled = true;

        if (crossSegments != lastCrossSegments)
        {
            crossPoints = new Vector3[crossSegments];
            float theta = 2.0f * Mathf.PI / crossSegments;
            for (int c = 0; c < crossSegments; c++)
            {
                crossPoints[c] = new Vector3(Mathf.Cos(theta * c), Mathf.Sin(theta * c), 0);
            }
            lastCrossSegments = crossSegments;
        }

        
        Quaternion rotation = Quaternion.identity;

        for (int p = 0; p < vertices.Length; p++)
        {
            if (p < vertices.Length - 1) 
                 rotation = Quaternion.FromToRotation(Vector3.forward, vertices[p + 1] - vertices[p]);

            for (int c = 0; c < crossSegments; c++)
            {
                int vertexIndex = p * crossSegments + c;
                meshVertices[vertexIndex] = vertices[p] + rotation * crossPoints[c] * thickness.Evaluate((float)p / vertices.Length);
                uvs[vertexIndex] = new Vector2((0.0f + c) / crossSegments, (0.0f + p) / vertices.Length);
                colors[vertexIndex] = color;

                lastVertices[c] = theseVertices[c];
                theseVertices[c] = p * crossSegments + c;
            }
            //make triangles
            if (p > 0)
            {
                for (int c = 0; c < crossSegments; c++)
                {
                    int start = (p * crossSegments + c) * 6;
                    tris[start] = lastVertices[c];
                    tris[start + 1] = lastVertices[(c + 1) % crossSegments];
                    tris[start + 2] = theseVertices[c];
                    tris[start + 3] = tris[start + 2];
                    tris[start + 4] = tris[start + 1];
                    tris[start + 5] = theseVertices[(c + 1) % crossSegments];
                }
            }
        }

        
        mesh.vertices = meshVertices;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        mesh.uv = uvs;
    }
}