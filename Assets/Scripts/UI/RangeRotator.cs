using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeRotator : MonoBehaviour {
	public float speed;
	public float yAchseOffset;

	GameObject target;
	
	// Update is called once per frame
	void Update () {
		if (target == null)
			return;
		
		transform.position = target.transform.position + Vector3.up * yAchseOffset;
		transform.Rotate (Vector3.forward * speed);
	}

	public void SetToTower(GameObject tower){
		target = tower;
		float radius = tower.GetComponent<Tower> ().RangeChecker.GetComponent<SphereCollider> ().radius;
		transform.localScale = Vector3.one * radius / 2;
	}

	public void Disable(){
		transform.localScale = Vector3.zero;
	}

	public void SetUpgrade(Tower t){
		float radius = t.GetComponent<Tower> ().RangeChecker.GetComponent<SphereCollider> ().radius;
		transform.localScale = Vector3.one * radius / 2;
	}

	public void UnsetUpgrade(){
		SetToTower (target);
	}
}
