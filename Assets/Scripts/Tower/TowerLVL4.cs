using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLVL4 : Tower {
	[Header("Level4 Spezific")]
	public float rotationSpeed;

	public GameObject RotatingGun;

	void Update(){
		if(EnemiesInRange.Count != 0)
			RotatingGun.transform.Rotate (new Vector3(rotationSpeed,0,0));
		base.Update ();

	}

	public override void Shoot (GameObject target)
	{
		base.Shoot (target);
	}

	public override void RemoveShot ()
	{
		base.RemoveShot ();
	}
}
