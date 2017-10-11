using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[System.Serializable]
public class TrigerWithMultiTag : UnityEvent<GameObject> {}


[RequireComponent(typeof(MultiTags))]
public class TargetWatcher : MonoBehaviour {

	[Header("Trigger with Multitag On Enter")]
	public TrigerWithMultiTag methodToTriggerOnEnter;
	[Header("Trigger with Multitag On Leave")]
	public TrigerWithMultiTag methodToTriggerOnLeave;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter(Collider other){
		
		foreach (MT s in GetComponent<MultiTags>().localTagList) {
			if (s.Name == "Untagged")
				continue;

			if (other.gameObject.HasTag (s.Name)) {
				methodToTriggerOnEnter.Invoke (other.transform.parent.gameObject);
				break;
			}
		}
	}

	void OnTriggerExit(Collider other){
		foreach (MT s in GetComponent<MultiTags>().localTagList) {
			if (s.Name == "Untagged")
				continue;
			if (other.gameObject.HasTag (s.Name)) {
				methodToTriggerOnLeave.Invoke (other.transform.parent.gameObject);
				break;
			}
		}
	}
}
