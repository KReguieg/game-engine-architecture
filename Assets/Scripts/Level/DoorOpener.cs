using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour {
	Animation doorAnimation;
	// Use this for initialization
	void Start () {
		doorAnimation = GetComponent<Animation> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartOpeningDoor(){
		doorAnimation.Play ();
	}
}
