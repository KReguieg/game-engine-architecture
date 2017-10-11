using UnityEngine;
using System.Collections;

public class FogOfWarPlayer : MonoBehaviour {
	
	public GameObject FogOfWarPlane;
	public LayerMask mask;

	void FixedUpdate () {

		FogOfWarPlane.GetComponent<Renderer> ().sharedMaterial.SetVector ("_Player", transform.position);
	}
}
