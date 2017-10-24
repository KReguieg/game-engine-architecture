using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMenu : MonoBehaviour {

	bool locked = true;
	float destroyTimer = 0;
	bool destroy;

	public float DestroyAfterTime = 1;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
		if (!locked && Input.GetMouseButton (0)) {
			destroy = true;
		}
		if (locked && Input.GetMouseButtonUp (0))
			locked = false;

		if (destroy) {
			destroyTimer += Time.deltaTime;
			if (destroyTimer >= DestroyAfterTime)
				Destroy (gameObject);
		}
	}

	public void DontDestroy(){
		destroy = false;
		destroyTimer = 0;
		locked = true;
	}

}
