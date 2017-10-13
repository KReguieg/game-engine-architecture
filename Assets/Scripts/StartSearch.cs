using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StartSearch : MonoBehaviour {
	public GameObject target;
	// Use this for initialization
	void Start () {
		GetComponent<NavMeshAgent> ().SetDestination(target.transform.position);
	}


}
