using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBlocker : MonoBehaviour {
	public bool RaycastBlockByUI;

	void LateUpdate(){
		RaycastBlockByUI = false;
	}
}
