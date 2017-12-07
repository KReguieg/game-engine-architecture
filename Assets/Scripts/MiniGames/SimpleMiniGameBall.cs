using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMiniGameBall : MonoBehaviour {

	Vector3 StartPosition;
	// Use this for initialization
	void Start () {
		StartPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		if(collisionInfo.collider.name == "Target"){
			Destroy(transform.parent.gameObject);
			GetComponentInParent<MetalGameManager>().Win();
		}
		else
			transform.position = StartPosition;
	}
}
