using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFrustumDrawer : MonoBehaviour {
	LineRenderer line;
	int height, width;

	// Use this for initialization
	void Start () {
		line = GetComponent<LineRenderer> ();
		line.positionCount = 4;
		height = Screen.height;
		width = Screen.width;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		Ray[] ray = new Ray[]{	Camera.main.ScreenPointToRay (new Vector3(0,0,0)),
			Camera.main.ScreenPointToRay (new Vector3(0,height,0)),
			Camera.main.ScreenPointToRay (new Vector3(width,height,0)),
			Camera.main.ScreenPointToRay (new Vector3(width,0,0))};
		Plane plane = new Plane (Vector3.up, Vector3.zero);
		float dist;
		for (int i = 0; i < 4; i++) {
			plane.Raycast (ray[i],out dist);
			line.SetPosition (i, ray [i].GetPoint (dist));
		}

	}
}
