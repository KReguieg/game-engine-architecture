using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisbilyCheck : MonoBehaviour {

	public Renderer[] connectedRenderToSeeInSight;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(){
		foreach (Renderer render in connectedRenderToSeeInSight) {
			render.enabled = true;
		}
		//transform.parent.gameObject.GetComponent<Renderer> ().enabled = true;
	}
	void OnTriggerExit(){
		foreach (Renderer render in connectedRenderToSeeInSight) {
			render.enabled = false;
		}
		transform.parent.gameObject.GetComponent<Renderer> ().enabled = false;
	}
}
