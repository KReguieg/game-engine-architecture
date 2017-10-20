using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarColor : MonoBehaviour {
	Material material;
	public Gradient fadeColor;

	float XSize;
	// Use this for initialization
	void Start () {
		material = GetComponent<MeshRenderer> ().material;
		XSize = transform.localScale.x;
	}


	public void SetLives(float amount)
	{
		
		material.color = fadeColor.Evaluate (1 - amount);

		Vector3 scale = transform.localScale;
		scale.x = amount * XSize;
		transform.localScale = scale;
	}


}
