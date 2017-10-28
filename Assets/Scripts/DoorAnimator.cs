using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimator : MonoBehaviour {
	public Material material;
	public Vector2 speed = Vector2.one;
	// Use this for initialization
	void Start () {
		
	}
	float offsetCounter;
	// Update is called once per frame
	void Update () {
		offsetCounter += Time.deltaTime;
		offsetCounter %= 10;
		Vector2 offset = new Vector2 (offsetCounter * speed.x, offsetCounter * speed.y);
		material.SetTextureOffset ("_MainTex", offset);
	}

	void OnApplicationQuit(){
		material.SetTextureOffset ("_MainTex", Vector2.zero );
	}
}
