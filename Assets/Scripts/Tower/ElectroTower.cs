using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroTower : Tower {
	[Header("ElectroTower Spezific")]

	public float MaxSpeed;
	public float lightningFadeOut;

	float lightningFadeOutTimer;

	public int lightningComplexityMin;
	public int lightningComplexityMax;
	LineRenderer lineRenderer;
	[ColorUsageAttribute(true,true,0f,8f,0.125f,3f)]
	public Color color;
	// Use this for initialization
	new void Start () {
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.positionCount = lightningComplexityMax;

		base.Start ();
	}
	

	void FixedUpdate(){
		float spinAmount = Mathf.Clamp01( attacktimer * attackspeed) * MaxSpeed;
		towerHead.transform.Rotate (Vector3.up * spinAmount);

		if(target != null && lightningFadeOutTimer <= lightningFadeOut)
			UpdateLightning ();
	}

	public override void LookAtTarget ()
	{
		
	}

	public override void RemoveShot ()
	{
		
	}

	public override void ShootAtTarget ()
	{
		RefreshLightning();

		target.GetComponent<EnemyBehavior>().TakeDamage(damage);
	}

	void UpdateLightning(){
		Color actualColor = color;
		actualColor.a = 1 - (lightningFadeOutTimer / lightningFadeOut);
		lineRenderer.endColor = actualColor;
		lineRenderer.startColor = actualColor;

		lightningFadeOutTimer += Time.deltaTime;
		int count = Random.Range (lightningComplexityMin, lightningComplexityMax);
		for (int i = 0; i < count; i++) {
			Vector3 pos = Vector3.Lerp ( towerHead.transform.position, target.transform.position, (i + 1f) / lineRenderer.positionCount );	
			pos += Random.insideUnitSphere;
			lineRenderer.SetPosition (i, pos);
		} 
		for (int i = count; i < lineRenderer.positionCount ; i++) {
			lineRenderer.SetPosition (i, target.transform.position);
		}
	}

	void RefreshLightning(){
		lightningFadeOutTimer = 0;
	}
}
