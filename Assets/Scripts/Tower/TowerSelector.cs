using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelector : MonoBehaviour {
	Tower parentTower;
	// Use this for initialization
	void Start () {
		parentTower = GetComponentInParent<Tower> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		parentTower.Select ();
	}
}
