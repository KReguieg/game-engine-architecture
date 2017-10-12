using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisbilyCheck : MonoBehaviour {

	public GameObject[] connectedObjectToSeeInSight;
	[SerializeField]
	string onEnterLayer = "Default";
	[SerializeField]
	string onLeaveLayer = "VROnly";

	int counter = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(){
		counter++;

		foreach (GameObject obj in connectedObjectToSeeInSight) {
			obj.layer = LayerMask.NameToLayer(onEnterLayer);
		}
		//transform.parent.gameObject.GetComponent<Renderer> ().enabled = true;
	}
	void OnTriggerExit(){
		counter--;

		if (counter == 0) {
			foreach (GameObject obj in connectedObjectToSeeInSight) {
				obj.layer = LayerMask.NameToLayer (onLeaveLayer);
			}
		}
		//transform.parent.gameObject.GetComponent<Renderer> ().enabled = false;
	}
}
