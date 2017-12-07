using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerMovement : MonoBehaviour {
	Vector3 startPos;
	// Use this for initialization
	void Start () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			MoveDirection(Vector3.up * 0.2f);
		}
		MoveBack();
	}

    private void MoveBack()
    {
        Vector3 dir = (startPos - transform.position).normalized;
		MoveDirection(dir * 0.01f);
    }

	void MoveDirection(Vector3 direction){
		transform.position += direction;
	}
}
