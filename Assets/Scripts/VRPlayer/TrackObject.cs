using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObject : MonoBehaviour {

	public Transform TrackedObject;
	public Transform AlternativeTrackedObject;
	public bool lockRotationX;
	public bool lockRotationZ = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Transform currentObject = TrackedObject;
		if(AlternativeTrackedObject != null && !currentObject.gameObject.activeSelf)
			currentObject = AlternativeTrackedObject;
		transform.SetPositionAndRotation(TrackedObject.position, TrackedObject.rotation);
		if(lockRotationX)
			transform.rotation = Quaternion.Euler(0,transform.eulerAngles.y,transform.eulerAngles.z);
		if(lockRotationZ)
			transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0);
	}
}
