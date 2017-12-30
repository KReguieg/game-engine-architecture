using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour {
	[Header("Link To own Objects")]
	public GameObject Circles;
	public GameObject head;

	[Header("Attributes")]
	public AnimationCurve headRotationSpeed;
	float damageLevel = 1;
	float maxDamageLevel = 3;

	public void DestroyBase(float lifeLeftPercent){
		
		if (damageLevel > maxDamageLevel)
			return;

			head.GetComponent<ElementRotator>().rotationSpeed = Vector3.forward * headRotationSpeed.Evaluate(1 - lifeLeftPercent);
		bool enoughLife = lifeLeftPercent >= 1 - (damageLevel / maxDamageLevel);
		if(enoughLife)
			return;

		
		GameObject Circle = Circles.transform.GetChild (0).gameObject;
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
}
