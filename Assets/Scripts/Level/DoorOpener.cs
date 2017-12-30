using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour {
	Animation doorAnimation;
	public bool open = false;
	// Use this for initialization
	void Start () {
		doorAnimation = GetComponent<Animation> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartOpeningDoor(){
		if(!open)
			doorAnimation.Play ();
		open = true;
	}
}
