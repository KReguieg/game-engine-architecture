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
 
 
    public Vector3[] vertices;
    public Material material;
 
    public int crossSegments = 3;
    private Vector3[] crossPoints;
    private int lastCrossSegments;
 
    private Vector3 lastCameraPosition1;
    private Vector3 lastCameraPosition2;
    public int movePixelsForRebuild = 6;
    public float maxRebuildTime = 0.1f;
    private float lastRebuildTime = 0.00f;

	public AnimationCurve thickness; 
 
    void Reset()
    {
        vertices = new Vector3[]{Vector3.zero, Vector3.one};
    }
    void Start()
    {
        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        mr.material = material;
    }
	Color color = Color.white;
    void LateUpdate()
    {
        if (null == vertices ||
            vertices.Length <= 1)
        {
            GetComponent<Renderer>().enabled = false;
            return;
        }
        GetComponent<Renderer>().enabled = true;
 
        //rebuild the mesh?
        bool re = false;
        float distFromMainCam;
        if (vertices.Length > 1)
        {
            Vector3 cur1 = Camera.main.WorldToScreenPoint(vertices[0]);
            distFromMainCam = lastCameraPosition1.z;
            lastCameraPosition1.z = 0;
            Vector3 cur2 = Camera.main.WorldToScreenPoint(vertices[vertices.Length - 1]);
            lastCameraPosition2.z = 0;
 
            float distance = (lastCameraPosition1 - cur1).magnitude;
            distance += (lastCameraPosition2 - cur2).magnitude;
 
            if (distance > movePixelsForRebuild || Time.time - lastRebuildTime > maxRebuildTime)
            {
                re = true;
                lastCameraPosition1 = cur1;
                lastCameraPosition2 = cur2;
            }
        }
 
        if (re)
        {
            //draw tube
 
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
 
            Vector3[] meshVertices = new Vector3[vertices.Length * crossSegments];
            Vector2[] uvs = new Vector2[vertices.Length * crossSegments];
            Color[] colors = new Color[vertices.Length * crossSegments];
            int[] tris = new int[vertices.Length * crossSegments * 6];
            int[] lastVertices = new int[crossSegments];
            int[] theseVertices = new int[crossSegments];
            Quaternion rotation = Quaternion.identity;
 
            for (int p = 0; p < vertices.Length; p++)
            {
                if (p < vertices.Length - 1) rotation = Quaternion.FromToRotation(Vector3.forward, vertices[p + 1] - vertices[p]);
 
                for (int c = 0; c < crossSegments; c++)
                {
                    int vertexIndex = p * crossSegments + c;
                    meshVertices[vertexIndex] = vertices[p] + rotation * crossPoints[c] * thickness.Evaluate((float)p /vertices.Length );
                    uvs[vertexIndex] = new Vector2((0.0f + c) / crossSegments, (0.0f + p) / vertices.Length);
                    colors[vertexIndex] = color;
 
                    //				print(c+" - vertex index "+(p*crossSegments+c) + " is " + meshVertices[p*crossSegments+c]);
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
                        //					print("Triangle: indexes("+tris[start]+", "+tris[start+1]+", "+tris[start+2]+"), ("+tris[start+3]+", "+tris[start+4]+", "+tris[start+5]+")");
                    }
                }
            }
 
            Mesh mesh = GetComponent<MeshFilter>().mesh;
            if (!mesh)
            {
                mesh = new Mesh();
            }
            mesh.vertices = meshVertices;
            mesh.triangles = tris;
            mesh.RecalculateNormals();
            mesh.uv = uvs;
        }
    }
}