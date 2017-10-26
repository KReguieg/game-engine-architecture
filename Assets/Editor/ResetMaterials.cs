using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoadAttribute]
public class ResetMaterials : Editor {
	public Material DoorMaterial;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Reset Materials");
	}

	void Awake()
	{
		
		if (!Application.isPlaying) {
			Debug.Log ("Reset Materials");
			DoorMaterial.mainTextureOffset = Vector2.zero;
		}
	}
}
