using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalCollector : MonoBehaviour {

	[SerializeField] float pullStrength = 50;
	public List<GameObject> metalInRange;


	// Use this for initialization
	void Start () {
		metalInRange = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (var obj in metalInRange) {
			PullMetal(obj);
		}
	}

	void FixedUpdate (){
		
		metalInRange.RemoveAll (x => {
			//Debug.Log("Metal Name " + x.name + " Tagged With" + x.tag);
			return x.transform.GetChild(0).tag == "Untagged";
		});
	}

	void PullMetal(GameObject metal){
		Vector3 dir = metal.transform.position - transform.position;
		dir = dir.normalized * -pullStrength;
		metal.GetComponent<Rigidbody>().AddForce(dir );
	}

	public void OnMetalEnterRange(GameObject go){
		metalInRange.Add (go);
	}

	public void OnMetalLeaveRange(GameObject go){
		metalInRange.Remove(go);
	}
}
