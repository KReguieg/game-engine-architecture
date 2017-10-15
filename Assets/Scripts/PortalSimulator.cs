using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MetalCollector))]
public class PortalSimulator : MonoBehaviour {

	public LayerMask mask;
	public float TakeMetalOverDistance = 5;
	MetalCollector collcetor;
	// Use this for initialization
	void Start () {
		collcetor = GetComponent<MetalCollector> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 1000, mask)) {
				WithMetalTransport (hit);
			}
		}
	}

	void WithMetalTransport(RaycastHit hit){
		Vector3 travelVector = (hit.point + Vector3.up) -  transform.position ;
		collcetor.metalInRange.ForEach (obj => {
			if (Vector3.Distance (obj.transform.position, transform.position) <= TakeMetalOverDistance)
				obj.transform.position += travelVector;
		});

		transform.position = hit.point + Vector3.up;
	}
}
