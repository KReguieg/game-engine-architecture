using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour {
	public GameObject Circles;
	int damageLevel = 0;
	int maxDamageLevel = 3;

	public void DestroyBase(float percent){
		return;
		if (damageLevel >= maxDamageLevel)
			return;
		
		GameObject Circle = Circles.transform.GetChild (damageLevel).gameObject;
		Transform[] children = Circle.GetComponentsInChildren<Transform> ();
		foreach (Transform t in children) {
			Rigidbody ridgidbody = t.gameObject.AddComponent<Rigidbody> ();
			ElementRotator rotator = t.gameObject.GetComponent<ElementRotator> ();

			if (rotator != null){
				ridgidbody.AddTorque(Vector3.one * 200);
				Destroy (rotator);
			}
			t.SetParent (transform);

		}
		damageLevel++;
	}

	void Update()
	{
		
	}
}
