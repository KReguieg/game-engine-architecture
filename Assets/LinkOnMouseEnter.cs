using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class LinkOnMouseEnter : MonoBehaviour {
	public UnityEvent MouseEnter;
	public UnityEvent MouseLeave;

	void OnMouseEnter(){
		Debug.Log ("wöööwöw");
		MouseEnter.Invoke();
	}

	void OnMouseLeave(){
		MouseLeave.Invoke ();
	}
}
