using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObject : MonoBehaviour {

	public Transform TrackedObject;
	public Transform AlternativeTrackedObject;
	public bool lockRotationX;
	// Use this for initialization
	void Start () {
		if(!TrackedObject.gameObject.active)
			TrackedObject = AlternativeTrackedObject;
	}
	
	// Update is called once per frame
	void Update () {
		transform.SetPositionAndRotation(TrackedObject.position, TrackedObject.rotation);
		if(lockRotationX)
			transform.rotation = Quaternion.Euler(0,transform.eulerAngles.y,transform.eulerAngles.z);
	}
}
