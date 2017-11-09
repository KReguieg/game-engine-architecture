using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementRotator : MonoBehaviour {
	public Vector3 rotationSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (rotationSpeed, Space.Self);
	}
}
