using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTowardsCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(RTS_Camera.Camera != null) {
			transform.forward = RTS_Camera.Camera.transform.forward;
			//transform.Rotate (Vector3.up * 180);
		}
	}
}
