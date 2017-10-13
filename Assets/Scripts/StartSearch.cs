using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StartSearch : MonoBehaviour {
	public GameObject target;
	public LayerMask mask;
	public bool Controlleble = false;
	// Use this for initialization
	void Start () {
		GetComponent<NavMeshAgent> ().SetDestination(target.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		if (Controlleble && Input.GetMouseButtonDown(0)) {
			
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 1000, mask)) {
				if(hit.collider.gameObject.tag != "player")
					GetComponent<NavMeshAgent> ().SetDestination(hit.point);
			}
		}
	}


}
