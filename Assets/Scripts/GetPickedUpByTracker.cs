using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPickedUpByTracker : MonoBehaviour {

		ConstantForce force;

		Rigidbody boody;

		GameObject claw;

	void OnTriggerEnter(Collider col) {
		Debug.Log(col.gameObject.name);
		if(col.name == "Claw") {
			claw = col.gameObject;
			claw.GetComponent<Rigidbody>().isKinematic = true;
			claw.AddComponent<SpringJoint>();
			
			SpringJoint springy = claw.GetComponent<SpringJoint>();
			springy.breakForce = 100.0f;
			claw.GetComponent<SpringJoint>().connectedBody = boody;
			claw.GetComponent<SpringJoint>().spring = 25;
			claw.GetComponent<SpringJoint>().anchor *= -1;
			transform.parent = col.transform;
			boody.drag = 35;
			transform.localPosition += new Vector3(0, -0.01f, 0);
		}
		if (col.name == "TargetboxFloor") {
			Debug.Log("TARGETBOX, DROP THAT SHIT!!!");
			Destroy(claw.GetComponent<SpringJoint>());
			transform.parent = null;
			GameObject.Find("Boxes").GetComponent<Animator>().enabled = true;
		}
		//col.transform.parent.GetComponent<BoxCollider>().isTrigger = false;
	}

	void OnCollision(Collision collision) {
		Debug.Log(collision.gameObject.name);
		if(collision.collider.name.Contains("Treasurebox") || collision.collider.name.Contains("Sphere") ) {
			boody.AddForce(new Vector3(0, -0.1f, 0));
		}
	}

	void Start() {
		boody = GetComponent<Rigidbody>();
		force = GetComponent<ConstantForce>();
	}
}
