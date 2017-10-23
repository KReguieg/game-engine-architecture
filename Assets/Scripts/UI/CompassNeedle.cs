using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassNeedle : MonoBehaviour {

	void FixedUpdate (){
		Vector3 direction = Camera.main.transform.forward;
		direction.y = 0;
		float angle = Vector3.Angle (direction, Vector3.forward);
		if (direction.x < 0)
			angle *= -1;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
