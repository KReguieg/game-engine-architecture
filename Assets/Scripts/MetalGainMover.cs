using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalGainMover : MonoBehaviour {
	public float speed = 1;
	public float maxY = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Vector3.up * speed;

		if (transform.position.y >= maxY) {
			Destroy (gameObject);
		}
	}
}
