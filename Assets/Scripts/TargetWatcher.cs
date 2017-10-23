using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[System.Serializable]
public class TrigerWithTag : UnityEvent<GameObject> {}

public class TargetWatcher : MonoBehaviour {
	[SerializeField]
	private string triggerOnTagName;
	[Header("Trigger with Tag On Enter")]
	public TrigerWithTag methodToTriggerOnEnter;
	[Header("Trigger with Tag On Leave")]
	public TrigerWithTag methodToTriggerOnLeave;

	void OnTriggerEnter(Collider other){
		if (other.tag == triggerOnTagName)
			methodToTriggerOnEnter.Invoke (other.transform.parent.gameObject);
		
	}

	void OnTriggerExit(Collider other){
		if (other.tag == triggerOnTagName)
			methodToTriggerOnLeave.Invoke (other.transform.parent.gameObject);
		
	}
}
