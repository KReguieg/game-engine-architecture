using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCompass : MonoBehaviour {
	float angle;
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 direction = transform.parent.forward;
		direction.y = 0;
		angle = Vector3.Angle (direction, Vector3.forward);
		if (direction.x > 0)
		 	angle *= -1;
		transform.localRotation = Quaternion.AngleAxis(angle, Vector3.up);
	}

	void OnDrawGizmos()
	{
		Vector3 start = transform.position;
		Vector3 end = start += transform.parent.forward;
		Gizmos.DrawLine(start, end);
	}
}
