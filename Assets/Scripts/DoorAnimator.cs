using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimator : MonoBehaviour {
	Material material;
	public Vector2 speed = Vector2.one;
	// Use this for initialization
	void Start () {
		material = GetComponent <Renderer>().sharedMaterial;
	}
	float offsetCounter;
	// Update is called once per frame
	void Update () {
		offsetCounter += Time.deltaTime;
		offsetCounter %= 10;
		Vector2 offset = new Vector2 (offsetCounter * speed.x, offsetCounter * speed.y);
		material.SetTextureOffset ("_MainTex", offset);
	}
}
