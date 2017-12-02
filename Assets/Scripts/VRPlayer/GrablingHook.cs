using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;


public class GrablingHook : MonoBehaviour {
	VRTK_ControllerEvents events;
	public LayerMask mask;

	public GameObject translatedHand;

	public VRTK_DashTeleport linkToDash;
	PositionSeeker seeker;

	VRTK_Pointer pointer; 
	Transform target;

	[SerializeField]
	private float MaxLength = 20;

	// Use this for initialization
	void Start () {
		events = GetComponent<VRTK_ControllerEvents>();
		events.TouchpadTouchStart += TouchStart;
		events.TouchpadTouchEnd += TouchEnd;
		events.TouchpadPressed += Pressed;

		seeker = translatedHand.GetComponent<PositionSeeker>();
		target = transform;
	}

    private void Pressed(object sender, ControllerInteractionEventArgs e)
    {
		linkToDash.Teleport(CreateDestinationMarker());
		target = transform;
    }

    private void TouchEnd(object sender, ControllerInteractionEventArgs e)
    {
        rayCasting = false;
    }

    bool rayCasting;
    private void TouchStart(object sender, ControllerInteractionEventArgs e)
	{
		rayCasting = true;
    }

	DestinationMarkerEventArgs CreateDestinationMarker(){
		DestinationMarkerEventArgs e = new DestinationMarkerEventArgs();
		e.destinationPosition = seeker.transform.position;
		e.target = target;
		e.enableTeleport = true;
		return e;
	}
	


	Vector3 pos;
	Ray ray ;
	RaycastHit hit;
    // Update is called once per frame
    void Update () {
		if(rayCasting){
			// this is a child of the Controller in Runtime -> parent for Controller Position
			ray = new Ray(transform.parent.position, transform.forward); 

			if(Physics.Raycast(ray ,out hit ,MaxLength, mask)){
				seeker.TargetPosition = hit.point;
				target = hit.transform;
			}
			else
			{
				seeker.TargetPosition = ray.GetPoint(MaxLength);
				target = transform;
			}
			pos = seeker.TargetPosition ;
		}
		seeker.transform.rotation = transform.rotation ;
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawLine(ray.origin, pos );
	}
}
