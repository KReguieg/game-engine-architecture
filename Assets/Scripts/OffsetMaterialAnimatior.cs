using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetMaterialAnimatior : MonoBehaviour {
	public Material material;
	float Timer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Timer += Time.deltaTime;
		Timer %= Mathf.PI * 2;

		material.mainTextureOffset = new Vector2(Mathf.Sin(Timer), Mathf.Cos(Timer));
	}

	void OnApplicationQuit(){
		material.mainTextureOffset = Vector2.zero;
	}
}
